using BusinessObject.Models;

namespace Repository.Interfaces
{
    public interface IReviewLogRepository
    {
        Task AddAsync(ReviewLog reviewLog);
        Task<List<ReviewLog>> GetBySubmissionIdAsync(int submissionId);
        Task<List<ReviewLog>> GetLatestBySubmissionGroupedByRoleAsync(int submissionId);
    }
}