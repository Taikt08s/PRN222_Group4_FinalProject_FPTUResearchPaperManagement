using BusinessObject.Models;

namespace Repository.Interfaces;

public interface IThesisModerationRepository
{
    Task<ThesisModeration?> GetBySubmissionIdAsync(int submissionId);
}