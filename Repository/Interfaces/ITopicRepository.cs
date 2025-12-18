using BusinessObject.Filters;
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

        Task<StudentGroupMember> GetStudentGroupMemberAsync(Guid studentId);

        Task<int> CountAsync(TopicFilter? filter);

        Task<List<Topic>> GetPaginationAsync(TopicFilter? filter, int page, int size);
        Task<Topic?> GetTopicByGroupAsync(int groupId);
        Task<StudentGroup?> GetGroupByTopicAsync(int topic);
        Task<Topic> CreateTopicAsync(Topic topic);
        Task<Topic> UpdateTopicAsync(Topic topic);
    }
}
