using BusinessObject.Models;

public class DashBoardCacheService : Service.GenericService<DashboardCache>, IDashboardCacheService
{
    private readonly Repository.Interfaces.IDashboardCacheRepository _dashboardCacheRepository;

    public DashBoardCacheService(Repository.Interfaces.IDashboardCacheRepository dashboardCacheRepository) : base(dashboardCacheRepository)
    {
        _dashboardCacheRepository = dashboardCacheRepository;
    }

    public async Task<List<DashboardCache>> GetDashboardCachesAsync()
    {
        return await _dashboardCacheRepository.GetAllAsync();
    }
} 