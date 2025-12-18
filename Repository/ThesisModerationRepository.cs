using BusinessObject.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository;

public class ThesisModerationRepository :IThesisModerationRepository
{
    private readonly AppDbContext _context;

    public ThesisModerationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ThesisModeration?> GetBySubmissionIdAsync(int submissionId)
    {
        return await _context.ThesisModerations
            .FirstOrDefaultAsync(x => x.Submission_Id == submissionId);
    }
}