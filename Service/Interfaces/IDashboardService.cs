using Service.Dtos;

namespace Service.Interfaces;

public interface IDashboardService
{
    Task<DashboardStatisticsDto> GetStatisticsAsync();
}
