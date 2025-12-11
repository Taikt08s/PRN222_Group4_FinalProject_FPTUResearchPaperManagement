using Service.Dtos;

namespace Service.Interfaces
{
    public interface ITopicService
    {
        Task<List<TopicResponseModel>> GetTopicsForStudentAsync(string major);
        
        Task<TopicResponseModel?> GetTopicByIdAsync(int id);
    }
}
