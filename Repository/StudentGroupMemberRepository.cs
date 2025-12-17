using BusinessObject.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository;

public class StudentGroupMemberRepository : GenericRepository<StudentGroupMember>, IStudentGroupMemberRepository
{
    private readonly IUserRepository _userRepository;
    public StudentGroupMemberRepository(AppDbContext context, IUserRepository userRepository) : base(context)
    {
        _userRepository = userRepository;
    }

    public async Task<List<StudentGroupMember>> GetByTopicAsync(int topicId)
    {
        return await _context.StudentGroupMembers
            .Where(sgm => sgm.Group.Topic.Id == topicId)
            .ToListAsync();
    }

}

