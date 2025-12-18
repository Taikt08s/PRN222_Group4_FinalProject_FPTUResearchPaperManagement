using BusinessObject.Models;

namespace Repository.Interfaces;

public interface ISemesterRepository : IGenericRepository<Semester>
{
    Task<List<String>> GetAllTermAsync();
    Task<Semester?> GetCurrentSemester();
    Task<bool> CheckMatchSemesterTime(DateTime startTime, DateTime endTime);
    Task<Semester> GetByIdAsync(int semesterId);
    Task<bool> CreateSemesterAsync(Semester semester);
}
