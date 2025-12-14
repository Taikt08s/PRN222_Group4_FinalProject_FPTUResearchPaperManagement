using BusinessObject.Models;

namespace Service.Interfaces;

public interface ISemesterService : IGenericService<Semester>
{
    Task<List<String>> GetAllTermAsync();
    Task<Semester?> GetCurrentSemester();
}
