using BusinessObject.Models;

namespace Repository.Interfaces;

public interface ISemesterRepository : IGenericRepository<Semester>
{
    Task<List<String>> GetAllTermAsync();
    Task<Semester?> GetCurrentSemester();
}
