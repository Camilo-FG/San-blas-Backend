using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs;

namespace SanblasBackend.Services;

public class DashboardService : IDashboardService
{
    private readonly GlobalContex _context;

    public DashboardService(GlobalContex context)
    {
        _context = context;
    }

    public async Task<DashboardStatsResponse> ObtenerEstadisticasAsync()
    {
        var solicitudesCatequesis = await _context.InscripcionesCatequesis.CountAsync();
        var solicitudesConstancias = await _context.FormSacras.CountAsync();
        var bautismos = await _context.Bautismos.CountAsync();
        var comuniones = await _context.Comuniones.CountAsync();
        var confirmaciones = await _context.Confirmaciones.CountAsync();
        var matrimonios = await _context.Matrimonios.CountAsync();
        var donaciones = await _context.Donaciones.CountAsync();
        var eventos = await _context.Eventos.CountAsync();
        var usuarios = await _context.Users.CountAsync();

        return new DashboardStatsResponse
        {
            SolicitudesCatequesis = solicitudesCatequesis,
            SolicitudesConstancias = solicitudesConstancias,
            RegistrosSacramentos = bautismos + comuniones + confirmaciones + matrimonios,
            Donaciones = donaciones,
            Eventos = eventos,
            Usuarios = usuarios
        };
    }
}
