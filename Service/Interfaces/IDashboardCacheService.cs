using BusinessObject.Models;
using Service.Interfaces;

public interface IDashboardCacheService : IGenericService<DashboardCache>
{
    public Task<List<DashboardCache>> GetDashboardCachesAsync();
}