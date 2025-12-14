using BusinessObject.Models;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service;

public class SemesterService : GenericService<Semester>, ISemesterService
{
    private ISemesterRepository _repo { get; set; }
    public SemesterService(ISemesterRepository repo) : base(repo)
    {
        _repo = repo;
    }

    public async Task<List<String>> GetAllTermAsync()
    {
        return await _repo.GetAllTermAsync();
    }

    public async Task<Semester?> GetCurrentSemester()
    {
        return await _repo.GetCurrentSemester();
    }
}
