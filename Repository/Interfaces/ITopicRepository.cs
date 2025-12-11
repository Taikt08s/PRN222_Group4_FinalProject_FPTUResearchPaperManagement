using BusinessObject.Models;

namespace Repository.Interfaces
{
    public interface ITopicRepository
    {
        Task<List<Topic>> GetTopicsByMajorAsync(string major);

        Task<Topic?> GetTopicByIdAsync(int id);

        Task CreateGroupAsync(StudentGroup group);

        Task<Topic> UpdateAsync(Topic topic);

        Task<bool> StudentHasTopicAsync(Guid studentId);
    }
}
