using BusinessObject.Models;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service;

public class StudentGroupService : IStudentGroupService
{
    private IStudentGroupRepository _repo { get; set; }
    public StudentGroupService(IStudentGroupRepository repo)
    {
        _repo = repo;
    }

    public async Task<StudentGroup?> GetByTopicAsync(int topicId)
    {
        return await _repo.GetByTopicAsync(topicId);

    }

    public async Task<StudentGroup?> GetByIdAsync(int groupId)
    {
        return await _repo.GetByIdAsync(groupId);
    }
}

