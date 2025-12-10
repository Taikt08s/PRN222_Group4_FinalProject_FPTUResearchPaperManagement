using BusinessObject.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .ToListAsync();
        }
    }
}
