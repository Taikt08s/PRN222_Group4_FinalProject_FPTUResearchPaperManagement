using BusinessObject.Models;
using Service.Dtos;

namespace Service.Interfaces
{
    public interface ISubmissionService
    {
        Task UploadFileAsync(
        int submissionId,
        string folder,
        Stream fileStream,
        string fileName,
        string contentType
    );

        Task<List<SubmissionFileDto>> GetFilesAsync(int submissionId);

        Task<Submission> GetOrCreateSubmissionIdAsync(
        int topicId,
        int groupId,
        int semesterId
        );
    }
}
