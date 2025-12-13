using AutoMapper;
using BusinessObject.Models;
using DataAccessLayer;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.EntityFrameworkCore;
using Service.Dtos;
using Service.Interfaces;

namespace Service
{
    public class SubmissionService : ISubmissionService
    {
        private readonly StorageClient _storage;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private const string BucketName = "fpturesearchpapermanagement.firebasestorage.app";

        public SubmissionService(AppDbContext context, IMapper mapper)
        {
            var credentialPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Credentials",
            "fpturesearchpapermanagement-firebase-adminsdk-fbsvc-651f744a16.json"
        );

            var credential = GoogleCredential.FromFile(credentialPath);
            _storage = StorageClient.Create(credential);
            _context = context;
            _mapper = mapper;
        }

        public async Task UploadFileAsync(int submissionId, string folder, Stream fileStream, string fileName, string contentType)
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

            var submissionFile = new SubmissionFile
            {
                Submission_Id = submissionId,
                File_Name = fileName,
                File_Type = folder,
                Firebase_Url = url,
                Uploaded_At = DateTime.UtcNow
            };

            _context.SubmissionFiles.Add(submissionFile);
            await _context.SaveChangesAsync();
        }


        public async Task<List<SubmissionFileDto>> GetFilesAsync(int submissionId)
        {
            var entities = await _context.SubmissionFiles
                .Where(f => f.Submission_Id == submissionId)
                .OrderByDescending(f => f.Uploaded_At)
                .ToListAsync();

            return _mapper.Map<List<SubmissionFileDto>>(entities);
        }
      
        public async Task<Submission> GetOrCreateSubmissionIdAsync(int topicId, int groupId,int semesterId)
        {
            var submission = await _context.Submissions
                .FirstOrDefaultAsync(s =>
                    s.Topic_Id == topicId &&
                    s.Group_Id == groupId &&
                    s.Semester_Id == semesterId
                );

            if (submission != null)
                return submission;

            submission = new Submission
            {
                Topic_Id = topicId,
                Group_Id = groupId,
                Semester_Id = semesterId,
                Status = "Draft",
                Submitted_At = DateTime.UtcNow,
                Reject_Reason = "",
                
            };

            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();

            return submission;
        }
    }
}
