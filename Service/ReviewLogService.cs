using BusinessObject.Models;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service
{
    public class ReviewLogService : IReviewLogService
    {
        private readonly IReviewLogRepository _repo;

        public ReviewLogService(IReviewLogRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(int submissionId, Guid reviewerId, string originalStatus, string newStatus, string comment)
        {
            var reviewLog = new ReviewLog
            {
                Submission_Id = submissionId,
                Reviewer_Id = reviewerId,
                Original_Status = originalStatus ?? string.Empty,
                New_Status = newStatus ?? string.Empty,
                Comment = comment ?? string.Empty,
                Created_At = DateTime.UtcNow
            };

            await _repo.AddAsync(reviewLog);
        }

        public async Task<List<ReviewLog>> GetBySubmissionIdAsync(int submissionId)
        {
            return await _repo.GetBySubmissionIdAsync(submissionId);
        }

        public async Task<List<ReviewLog>> GetLatestBySubmissionGroupedByRoleAsync(int submissionId)
        {
            return await _repo.GetLatestBySubmissionGroupedByRoleAsync(submissionId);
        }
    }
}
