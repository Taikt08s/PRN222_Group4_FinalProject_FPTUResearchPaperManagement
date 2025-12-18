using BusinessObject.Models;

namespace Repository.Interfaces;

public class DashboardCacheRepository : GenericRepository<DashboardCache>, IDashboardCacheRepository
{
    public DashboardCacheRepository(DataAccessLayer.AppDbContext context) : base(context)
    {
    }
    public Task<DashboardCache> createDashboardCacheAsync(DashboardCache dashboardCache)
    {
        _context.DashboardCaches.Add(dashboardCache);
        _context.SaveChanges();
        return Task.FromResult(dashboardCache);
    } 
}