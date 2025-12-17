using BusinessObject.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository
{
    public class ReviewLogRepository : GenericRepository<ReviewLog>, IReviewLogRepository
    {
        private readonly AppDbContext _context;

        public ReviewLogRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(ReviewLog reviewLog)
        {
            _context.ReviewLogs.Add(reviewLog);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReviewLog>> GetBySubmissionIdAsync(int submissionId)
        {
            return await _context.ReviewLogs
                .AsNoTracking()
                .Where(r => r.Submission_Id == submissionId)
                .Where(r => r.Created_At >= r.Submission.Submitted_At)
                .OrderBy(r => r.Created_At)
                .Include(r => r.Reviewer)
                .ToListAsync();
        }

        /// <summary>
        /// Returns the latest review log per reviewer role for a submission.
        /// Useful to determine the most recent vote by each role (Instructor, GPEC).
        /// </summary>
        public async Task<List<ReviewLog>> GetLatestBySubmissionGroupedByRoleAsync(int submissionId)
        {
            var logs = await _context.ReviewLogs
                .AsNoTracking()
                .Where(r => r.Submission_Id == submissionId)
                .Where(r => r.Created_At >= r.Submission.Submitted_At)
                .Include(r => r.Reviewer)
                .ToListAsync();

            var latestByRole = logs
                .Where(l => l.Reviewer != null)
                .Where(r => r.Created_At >= r.Submission.Submitted_At)
                .GroupBy(l => l.Reviewer.Role)
                .Select(g => g.OrderByDescending(x => x.Created_At).First())
                .ToList();

            return latestByRole;
        }
    }
}
