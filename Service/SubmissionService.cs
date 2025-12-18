using AutoMapper;
using BusinessObject.Enums;
using BusinessObject.Filters;
using BusinessObject.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Repository.Interfaces;
using Service.Dtos;
using Service.Interfaces;


namespace Service
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ISubmissionRepository _submissionRepo;
        private readonly ISubmissionFileRepository _fileRepo;
        private readonly IOpenAiSubmissionService _openAiSubmissionService;
        private readonly ISuspensionService _suspensionService;
        private readonly IReviewLogService _reviewLogService;
        private readonly IThesisModerationRepository _moderationRepo;
        private readonly StorageClient _storage;
        private readonly IMapper _mapper;

        private const string BucketName =
            "fpturesearchpapermanagement.firebasestorage.app";

        public SubmissionService(
            ISubmissionRepository submissionRepo,
            ISubmissionFileRepository fileRepo,
            IOpenAiSubmissionService openAiSubmissionService,
            ISuspensionService suspensionService,
            IThesisModerationRepository moderationRepo,
            IReviewLogService reviewLogService,
            IMapper mapper)
        {
            _submissionRepo = submissionRepo;
            _fileRepo = fileRepo;
            _openAiSubmissionService = openAiSubmissionService;
            _suspensionService = suspensionService;
            _reviewLogService = reviewLogService;
            _moderationRepo = moderationRepo;
            _mapper = mapper;

            var credentialPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Credentials",
                "fpturesearchpapermanagement-3465b1d3b88e.json"
            );

            var credential = GoogleCredential.FromFile(credentialPath);
            _storage = StorageClient.Create(credential);
        }

        // ================= SUBMISSION =================

        public async Task<SubmissionDto> GetOrCreateSubmissionAsync(
            int topicId,
            int groupId,
            int semesterId)
        {
            var submission = await _submissionRepo
                .GetByTopicGroupSemesterAsync(topicId, groupId, semesterId);

            if (submission == null)
            {
                submission = new Submission
                {
                    Topic_Id = topicId,
                    Group_Id = groupId,
                    Semester_Id = semesterId,
                    Status = SubmissionStatus.Draft.ToString(),
                    Submitted_At = DateTime.UtcNow,
                    Reviewed_At = null,
                    Plagiarism_Score = 0,
                    Plagiarism_Flag = false,
                    Reject_Reason = ""
                };

                await _submissionRepo.AddAsync(submission);
            }

            return _mapper.Map<SubmissionDto>(submission);
        }

        public async Task SubmitAsync(int submissionId)
        {
            var submission = await _submissionRepo.GetByIdAsync(submissionId);

            if (submission == null)
                throw new Exception("Submission not found");

            if (submission.Status == SubmissionStatus.Submitted.ToString())
                return;

            submission.Status = SubmissionStatus.Submitted.ToString();
            submission.Submitted_At = DateTime.UtcNow;

            await _openAiSubmissionService
                .ValidateAndModerateSubmissionAsync(submission);

            await _suspensionService.SuspendGroupAsync(submission);

            await _submissionRepo.UpdateAsync(submission);
        }

        public async Task<SubmissionDto?> GetSubmissionAsync(int topicId, int groupId, int semesterId)
        {
            var submission = await _submissionRepo
                .GetByTopicGroupSemesterAsync(topicId, groupId, semesterId);

            if (submission == null)
                return null;

            return _mapper.Map<SubmissionDto>(submission);
        }

        public async Task<SubmissionDto?> GetByIdAsync(int submissionId)
        {
            var submission = await _submissionRepo.GetByIdAsync(submissionId);
            if (submission == null) return null;
            return _mapper.Map<SubmissionDto>(submission);
        }

        // ================= FILES =================

        public async Task UploadFileAsync(
            int submissionId,
            string folder,
            Stream fileStream,
            string fileName,
            string contentType)
        {
            var uniqueName = $"{Guid.NewGuid()}_{fileName}";
            var objectName = $"submissions/{submissionId}/{folder}/{uniqueName}";

            await _storage.UploadObjectAsync(
                BucketName,
                objectName,
                contentType,
                fileStream
            );

            var url = $"https://storage.googleapis.com/{BucketName}/{objectName}";

            var file = new SubmissionFile
            {
                Submission_Id = submissionId,
                File_Name = fileName,
                File_Type = folder,
                Firebase_Url = url,
                Uploaded_At = DateTime.UtcNow
            };

            await _fileRepo.AddAsync(file);
        }

        public async Task<List<SubmissionFileDto>> GetFilesAsync(int submissionId)
        {
            var files = await _fileRepo.GetBySubmissionIdAsync(submissionId);
            return _mapper.Map<List<SubmissionFileDto>>(files);
        }

        public async Task RemoveFileAsync(int fileId)
        {
            var files = await _fileRepo.GetByIdAsync(fileId);
            if (files == null)
                throw new Exception("File not found");
            await _fileRepo.RemoveAsync(files);
        }

        // ================= REVIEW / VOTE =================
        public async Task ReviewSubmissionAsync(int submissionId, Guid reviewerId, string newStatus, string comment)
        {
            var submissionEntity = await _submissionRepo.GetByIdAsync(submissionId);
            if (submissionEntity == null)
                throw new Exception("Submission not found");

            // Approve action is only allowed when submission is Submitted or Reviewing
            if (string.Equals(newStatus, "Approve", StringComparison.OrdinalIgnoreCase))
            {
                var allowed = string.Equals(submissionEntity.Status, SubmissionStatus.Submitted.ToString(), StringComparison.OrdinalIgnoreCase)
                              || string.Equals(submissionEntity.Status, SubmissionStatus.Reviewing.ToString(), StringComparison.OrdinalIgnoreCase);

                if (!allowed)
                    throw new InvalidOperationException("Cannot approve a submission that has not been submitted or is not under review.");
            }

            // load existing logs to check duplicate vote by same reviewer
            var existingLogs = await _reviewLogService.GetBySubmissionIdAsync(submissionId);

            // Prevent the same reviewer approving the same submission more than once
            if (string.Equals(newStatus, "Approve", StringComparison.OrdinalIgnoreCase))
            {
                var alreadyApprovedByThisReviewer = existingLogs.Any(l =>
                    l.Reviewer_Id == reviewerId &&
                    string.Equals(l.New_Status, "Approve", StringComparison.OrdinalIgnoreCase));

                if (alreadyApprovedByThisReviewer)
                    throw new InvalidOperationException("You have already approved this submission.");
            }

            // Persist review via review-log service
            await _reviewLogService.CreateAsync(
                submissionId,
                reviewerId,
                submissionEntity.Status,
                newStatus,
                comment ?? string.Empty
            );

            // Recompute using up-to-date logs
            var logs = await _reviewLogService.GetBySubmissionIdAsync(submissionId);

            // If any reject exists -> Rejected
            var anyReject = logs.Any(l => string.Equals(l.New_Status, "Reject", StringComparison.OrdinalIgnoreCase));
            if (anyReject)
            {
                submissionEntity.Status = SubmissionStatus.Rejected.ToString();
                var rejectLog = logs.Where(l => string.Equals(l.New_Status, "Reject", StringComparison.OrdinalIgnoreCase))
                                    .OrderByDescending(l => l.Created_At)
                                    .FirstOrDefault();
                if (rejectLog != null)
                {
                    submissionEntity.Reject_Reason = rejectLog.Comment;
                }

                await _submissionRepo.UpdateAsync(submissionEntity);
                return;
            }

            bool instructorSuspend = logs.Any(l =>
                string.Equals(l.New_Status, "Suspend", StringComparison.OrdinalIgnoreCase)
                && l.Reviewer != null
                && string.Equals(l.Reviewer.Role, "Instructor", StringComparison.OrdinalIgnoreCase));

            bool gpecSuspend = logs.Any(l =>
                string.Equals(l.New_Status, "Suspend", StringComparison.OrdinalIgnoreCase)
                && l.Reviewer != null
                && string.Equals(l.Reviewer.Role, "GraduationProjectEvaluationCommitteeMember", StringComparison.OrdinalIgnoreCase));

            if (instructorSuspend && gpecSuspend)
            {
                submissionEntity.Status = SubmissionStatus.Suspended.ToString();
                await _submissionRepo.UpdateAsync(submissionEntity);
                return;
            }

            // Determine whether there is at least one Approve from Instructor and at least one from GPEC
            bool instructorApproved = logs.Any(l =>
                string.Equals(l.New_Status, "Approve", StringComparison.OrdinalIgnoreCase)
                && l.Reviewer != null
                && string.Equals(l.Reviewer.Role, "Instructor", StringComparison.OrdinalIgnoreCase));

            bool gpecApproved = logs.Any(l =>
                string.Equals(l.New_Status, "Approve", StringComparison.OrdinalIgnoreCase)
                && l.Reviewer != null
                && string.Equals(l.Reviewer.Role, "GraduationProjectEvaluationCommitteeMember", StringComparison.OrdinalIgnoreCase));

            // If both roles have at least one Approve -> Approved
            if (instructorApproved && gpecApproved)
            {
                submissionEntity.Status = SubmissionStatus.Approved.ToString();
                await _submissionRepo.UpdateAsync(submissionEntity);
                return;
            }

            // If only one of the two roles approved -> set Reviewing
            if (instructorApproved || gpecApproved)
            {
                submissionEntity.Status = SubmissionStatus.Reviewing.ToString();
                await _submissionRepo.UpdateAsync(submissionEntity);
                return;
            }

            // Otherwise mark as Reviewing (in-progress)
            submissionEntity.Status = SubmissionStatus.Reviewing.ToString();
            await _submissionRepo.UpdateAsync(submissionEntity);
        }

        public async Task<int> CountFilteredAsync(SubmissionFilter? filter)
        {
            return await _submissionRepo.CountFilteredAsync(filter);
        }

        public async Task<List<Submission>> GetPaginationAsync(SubmissionFilter? filter, int page, int size)
        {
            return await _submissionRepo.GetPaginationAsync(filter, page, size);
        }
        
        public async Task<ThesisAiResult?> GetThesisAiResultAsync(int submissionId)
        {
            var moderation = await _moderationRepo.GetBySubmissionIdAsync(submissionId);
            if (moderation == null) return null;

            return _mapper.Map<ThesisAiResult>(moderation);
        }
    }
}
