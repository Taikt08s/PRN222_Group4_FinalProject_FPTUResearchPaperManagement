using BusinessObject.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository;

public class SemesterRepository : GenericRepository<Semester>, ISemesterRepository
{
    private readonly IUserRepository _userRepository;
    public SemesterRepository(AppDbContext context, IUserRepository userRepository) : base(context)
    {
        _userRepository = userRepository;
    }

    public async Task<List<String>> GetAllTermAsync()
    {
        return await _context.Semesters.Select(sem => sem.Term).ToListAsync();
    }

    public async Task<Semester?> GetCurrentSemester()
    {
        return await _context.Semesters
            .Where(sem => sem.Start_Date.Date >= DateTime.Now.Date)
            .Where(sem => sem.End_Date.Date <= DateTime.Now.Date)
            .FirstOrDefaultAsync();
    }

    public async Task<Semester> GetByIdAsync(int semesterId)
    {
        return await _context.Semesters
            .FirstOrDefaultAsync(s => s.Id == semesterId);
    }
}
