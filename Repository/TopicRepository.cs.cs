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
    }
}
