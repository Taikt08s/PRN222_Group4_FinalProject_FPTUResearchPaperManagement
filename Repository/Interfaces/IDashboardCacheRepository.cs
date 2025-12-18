using BusinessObject.Models;

namespace Repository.Interfaces;

public interface IDashboardCacheRepository : IGenericRepository<DashboardCache>
{
    public Task<DashboardCache> createDashboardCacheAsync(DashboardCache dashboardCache);
}