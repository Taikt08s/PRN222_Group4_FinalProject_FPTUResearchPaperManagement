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
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly AppDbContext _context;

        public SubmissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Submission?> GetByIdAsync(int id)
        {
            return await _context.Submissions
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Submission?> GetByTopicGroupSemesterAsync(
            int topicId,
            int groupId,
            int semesterId)
        {
            return await _context.Submissions
                .FirstOrDefaultAsync(s =>
                    s.Topic_Id == topicId &&
                    s.Group_Id == groupId &&
                    s.Semester_Id == semesterId
                );
        }

        public async Task AddAsync(Submission submission)
        {
            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Submission submission)
        {
            _context.Submissions.Update(submission);
            await _context.SaveChangesAsync();
        }
    }
}
