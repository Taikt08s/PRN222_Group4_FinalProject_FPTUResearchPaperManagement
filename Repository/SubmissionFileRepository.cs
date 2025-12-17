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
    public class SubmissionFileRepository : ISubmissionFileRepository
    {
        private readonly AppDbContext _context;

        public SubmissionFileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubmissionFile>> GetBySubmissionIdAsync(int submissionId)
        {
            return await _context.SubmissionFiles
                .Where(f => f.Submission_Id == submissionId)
                .OrderByDescending(f => f.Uploaded_At)
                .ToListAsync();
        }

        public async Task AddAsync(SubmissionFile file)
        {
            _context.SubmissionFiles.Add(file);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(SubmissionFile file)
        {
            _context.SubmissionFiles.Remove(file);
            await _context.SaveChangesAsync();
        }

        public async Task<SubmissionFile?> GetByIdAsync(int id)
        {
            return await _context.SubmissionFiles.Where(f => f.Id == id).FirstOrDefaultAsync();
        }
    }
}
