using AutoMapper;
using BusinessObject.Enums;
using BusinessObject.Models;
using DataAccessLayer;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Service.Dtos;
using Service.Interfaces;

namespace Service
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ISubmissionRepository _submissionRepo;
        private readonly ISubmissionFileRepository _fileRepo;
        private readonly StorageClient _storage;
        private readonly IMapper _mapper;

        private const string BucketName =
            "fpturesearchpapermanagement.firebasestorage.app";

        public SubmissionService(
            ISubmissionRepository submissionRepo,
            ISubmissionFileRepository fileRepo,
            IMapper mapper)
        {
            _submissionRepo = submissionRepo;
            _fileRepo = fileRepo;
            _mapper = mapper;

            var credentialPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Credentials",
                "fpturesearchpapermanagement-firebase-adminsdk-fbsvc-651f744a16.json"
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
                    Submitted_At = null,
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

            await _submissionRepo.UpdateAsync(submission);
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
    }
}
