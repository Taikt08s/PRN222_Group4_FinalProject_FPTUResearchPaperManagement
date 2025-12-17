using BusinessObject.Filters;
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

        Task RemoveFileAsync(int fileId);
        Task<List<SubmissionFileDto>> GetFilesAsync(int submissionId);

        Task SubmitAsync(int submissionId);

        Task<SubmissionDto> GetOrCreateSubmissionAsync(
            int topicId,
            int groupId,
            int semesterId
        );

        Task<SubmissionDto?> GetSubmissionAsync(int topicId, int groupId, int semesterId);
        Task<SubmissionDto?> GetByIdAsync(int submissionId);
        Task ReviewSubmissionAsync(int submissionId, Guid reviewerId, string newStatus, string comment);
        Task<int> CountFilteredAsync(SubmissionFilter? filter);
        Task<List<Submission>> GetPaginationAsync(SubmissionFilter? filter, int page, int size);
    }
}
