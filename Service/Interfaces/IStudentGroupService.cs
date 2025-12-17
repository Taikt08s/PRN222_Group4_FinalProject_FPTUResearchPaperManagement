using BusinessObject.Models;

namespace Service.Interfaces;

public interface IStudentGroupService
{
    Task<StudentGroup?> GetByTopicAsync(int topicId);
    Task<StudentGroup?> GetByIdAsync(int groupId);
}

