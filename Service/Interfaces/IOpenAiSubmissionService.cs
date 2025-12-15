using BusinessObject.Models;

namespace Service.Interfaces;

public interface IOpenAiSubmissionService
{
    Task<bool> ValidateAndModerateSubmissionAsync(Submission submission);
}