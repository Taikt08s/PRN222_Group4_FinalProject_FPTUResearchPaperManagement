using BusinessObject.Models;

namespace Service.Interfaces;

public interface IStudentGroupMemberService : IGenericService<StudentGroupMember>
{
    Task<List<StudentGroupMember>> GetByTopicAsync(int topicId);
}

