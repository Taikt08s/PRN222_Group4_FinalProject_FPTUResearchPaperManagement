using BusinessObject.Models;

namespace Service.Interfaces
{
    public interface IReviewLogService
    {
        Task CreateAsync(int submissionId, Guid reviewerId, string originalStatus, string newStatus, string comment);
        Task<List<ReviewLog>> GetBySubmissionIdAsync(int submissionId);
        Task<List<ReviewLog>> GetLatestBySubmissionGroupedByRoleAsync(int submissionId);
    }
}