using BusinessObject.Filters;
using BusinessObject.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository
{
    public class TopicRepository : ITopicRepository
    {
        private readonly AppDbContext _context;

        public TopicRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Topic>> GetTopicsByMajorAsync(string major)
        {
            return await _context.Topics
                .Where(t => t.Major == major)
                .Include(t => t.Semester)
                .Include(t => t.Creator)
                .ToListAsync();
        }

        public async Task<Topic?> GetTopicByIdAsync(int id)
        {
            return await _context.Topics
                .Include(t => t.Semester)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Topic> UpdateAsync(Topic topic)
        {
            _context.Topics.Update(topic);
            await _context.SaveChangesAsync();
            return topic;
        }

        public async Task CreateGroupAsync(StudentGroup group)
        {
            await _context.StudentGroups.AddAsync(group);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> StudentHasTopicAsync(Guid studentId)
        {
            return await _context.StudentGroupMembers
                .Include(x => x.Group)
                .AnyAsync(x => x.Student_Id == studentId);
        }

        public async Task<StudentGroupMember> GetStudentGroupMemberAsync(Guid studentId)
        {
            return await _context.StudentGroupMembers
                .Include(x => x.Group)
                    .ThenInclude(g => g.Members)
                        .ThenInclude(m => m.Student)
                .FirstOrDefaultAsync(x => x.Student_Id == studentId);
        }

        private IQueryable<Topic> GetFiltered(TopicFilter? filter)
        {
            if (filter == null)
                return _context.Topics.AsQueryable();
            return _context.Topics
            .Where(t => filter.SemesterTermFilter == null ? true : filter.SemesterTermFilter.Contains(t.Semester.Term))
            .Where(t => String.IsNullOrEmpty(filter.GroupStatusFilter) ?
                    true :
                    filter.GroupStatusFilter == "Solo" ? t.Is_Group_Required == true : t.Is_Group_Required == false)
            .Where(t => filter.TopicStatusFilter == null ? true : filter.TopicStatusFilter.Contains(t.Status))
            .Where(t => filter.ByIntructors == null ? true : filter.ByIntructors.Contains(t.Created_By))
            .AsQueryable();
        }

        public async Task<int> CountAsync(TopicFilter? filter)
        {
            return await GetFiltered(filter).CountAsync();
        }

        public async Task<List<Topic>> GetPaginationAsync(TopicFilter? filter, int page, int size)
        {
            return await GetFiltered(filter)
            .Include(t => t.Creator) // Include the user who created the topic
            .Include(t => t.Semester) // Include the associated semester
            .Include(x => x.Groups)
                .ThenInclude(g => g.Members)
                    .ThenInclude(m => m.Student)
            .OrderBy(t => t.Title)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
        }
    }

}
