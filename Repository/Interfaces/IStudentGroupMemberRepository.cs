using BusinessObject.Models;

namespace Repository.Interfaces;

public interface IStudentGroupMemberRepository : IGenericRepository<StudentGroupMember>
{
    Task<List<StudentGroupMember>> GetByTopicAsync(int topicId);
}
