using BusinessObject.Filters;
using Service.Dtos;

namespace Service.Interfaces
{
    public interface ITopicService
    {
        Task<List<TopicResponseModel>> GetTopicsForStudentAsync(string major);

        Task UpdateTopicStatus(int id, string status);

        Task<TopicResponseModel?> GetTopicByIdAsync(int id);

        Task<(bool Success, string Error)> TopicRegistrationAsync(TopicRegistrationRequest req);

        Task<bool> StudentHasTopicAsync(Guid studentId);

        Task<TopicResponseModel?> GetRegisteredTopicForStudentAsync(Guid studentId);

        Task<int> CountAsync(TopicFilter? filter);
        Task<List<TopicResponseModel>> GetPaginationAsync(TopicFilter? filter, int page, int size);
        Task<TopicResponseModel> CreateTopicAsync(CreateTopicRequest req);
        Task<TopicResponseModel> UpdateTopicAsync(UpdateTopicRequest req);
    }
}
