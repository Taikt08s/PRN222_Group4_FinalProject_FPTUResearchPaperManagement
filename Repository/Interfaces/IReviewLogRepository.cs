using BusinessObject.Models;

namespace Repository.Interfaces
{
    public interface IReviewLogRepository
    {
        Task AddAsync(ReviewLog reviewLog);
        Task<List<ReviewLog>> GetBySubmissionIdAsync(int submissionId);
        Task<List<ReviewLog>> GetLatestBySubmissionGroupedByRoleAsync(int submissionId);
        Task<bool> IsUserReviewed(Guid currentUserId, int submissionId);
        Task<List<int>> GetSubmissionsUserReviewed(Guid currentUserId, List<int> submissionIds);
    }
}
