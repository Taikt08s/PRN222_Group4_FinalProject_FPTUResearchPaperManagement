using BusinessObject.Models;

namespace Repository.Interfaces
{
    public interface IStudentGroupRepository
    {
        Task<List<Guid>> GetStudentIdsByGroupAsync(int groupId);
        Task<StudentGroup?> GetByTopicAsync(int topicId);
        Task<StudentGroup?> GetByIdAsync(int groupId);
    }
}
