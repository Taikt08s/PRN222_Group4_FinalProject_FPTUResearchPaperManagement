using BusinessObject.Models;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service;

public class StudentGroupMemberService : GenericService<StudentGroupMember>, IStudentGroupMemberService
{
    private IStudentGroupMemberRepository _repo { get; set; }
    public StudentGroupMemberService(IStudentGroupMemberRepository repo) : base(repo)
    {
        _repo = repo;
    }

    public async Task<List<StudentGroupMember>> GetByTopicAsync(int topicId)
    {
        return await _repo.GetByTopicAsync(topicId);
    }

}

