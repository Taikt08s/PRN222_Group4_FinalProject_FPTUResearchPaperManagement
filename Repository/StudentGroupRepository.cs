using BusinessObject.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository
{
    public class StudentGroupRepository : IStudentGroupRepository
    {
        private readonly AppDbContext _context;

        public StudentGroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> GetStudentIdsByGroupAsync(int groupId)
        {
            return await _context.StudentGroupMembers
                .Where(m => m.Group_Id == groupId)
                .Select(m => m.Student_Id)
                .ToListAsync();
        }

    }
}
