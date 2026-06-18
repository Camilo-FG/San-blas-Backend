using SanblasBackend.DTOs;

namespace SanblasBackend.Services;

public interface IDashboardService
{
    Task<DashboardStatsResponse> ObtenerEstadisticasAsync();
}
