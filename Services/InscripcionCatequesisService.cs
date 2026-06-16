using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs;
using SanblasBackend.Models;

namespace SanblasBackend.Services;

public class InscripcionCatequesisService : IInscripcionCatequesisService
{
    private readonly GlobalContex _context;

    public InscripcionCatequesisService(GlobalContex context)
    {
        _context = context;
    }

    public async Task<CrearInscripcionCatequesisResponse> CrearInscripcionAsync(CrearInscripcionCatequesisRequest request)
    {
        var inscripcion = new InscripcionCatequesis
        {
            CentroCatequesis = request.DatosInscripcion.CentroCatequesis,
            NivelAInscribirse = request.DatosInscripcion.NivelAInscribirse,
            Estado = "Pendiente",
            FechaSolicitud = DateTime.UtcNow,
            Catequizando = new Catequizando
            {
                Nombre = request.DatosCatequizando.Nombre,
                Apellidos = request.DatosCatequizando.Apellidos,
                FechaNacimiento = request.DatosCatequizando.FechaNacimiento,
                DireccionExacta = request.DatosCatequizando.DireccionExacta
            },
            Bautismo = new BautismoCatequizando
            {
                Parroquia = request.DatosBautismo.Parroquia,
                Fecha = request.DatosBautismo.Fecha,
                Tomo = request.DatosBautismo.Tomo,
                Folio = request.DatosBautismo.Folio,
                Asiento = request.DatosBautismo.Asiento
            },
            Adecuacion = new AdecuacionCatequizando
            {
                RequiereAdecuacionCentroEducativo = request.DatosAdecuacion.RequiereAdecuacionCentroEducativo,
                DescripcionAdecuacion = request.DatosAdecuacion.DescripcionAdecuacion
            },
            CondicionSalud = new CondicionSaludCatequizando
            {
                PortadorEnfermedadCronica = request.DatosCondicionSalud.PortadorEnfermedadCronica,
                DescripcionEnfermedad = request.DatosCondicionSalud.DescripcionEnfermedad
            },
            Madre = new MadreCatequizando
            {
                Nombre = request.DatosMadre.Nombre,
                Apellidos = request.DatosMadre.Apellidos ?? string.Empty,
                DireccionExacta = request.DatosMadre.DireccionExacta,
                Ciudad = request.DatosMadre.Ciudad,
                Provincia = request.DatosMadre.Provincia,
                Telefono = request.DatosMadre.Telefono
            }
        };

        _context.InscripcionesCatequesis.Add(inscripcion);
        await _context.SaveChangesAsync();

        return new CrearInscripcionCatequesisResponse
        {
            Id = inscripcion.Id,
            Mensaje = "Inscripción a catequesis registrada correctamente",
            Estado = inscripcion.Estado,
            FechaSolicitud = inscripcion.FechaSolicitud
        };
    }

    public async Task<IEnumerable<InscripcionCatequesisResumenResponse>> ObtenerInscripcionesAsync(string? estado)
    {
        var query = _context.InscripcionesCatequesis
            .Include(i => i.Catequizando)
            .Include(i => i.Madre)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(estado))
        {
            query = query.Where(i => i.Estado == estado);
        }

        var inscripciones = await query
            .OrderByDescending(i => i.FechaSolicitud)
            .ToListAsync();

        return inscripciones.Select(i => new InscripcionCatequesisResumenResponse
        {
            Id = i.Id,
            NombreCatequizando = $"{i.Catequizando.Nombre} {i.Catequizando.Apellidos}".Trim(),
            CentroCatequesis = i.CentroCatequesis,
            NivelAInscribirse = i.NivelAInscribirse,
            Estado = i.Estado,
            FechaSolicitud = i.FechaSolicitud,
            TelefonoEncargada = i.Madre.Telefono
        });
    }

    public async Task<InscripcionCatequesisDetalleResponse?> ObtenerInscripcionPorIdAsync(int id)
    {
        var inscripcion = await _context.InscripcionesCatequesis
            .Include(i => i.Catequizando)
            .Include(i => i.Bautismo)
            .Include(i => i.Adecuacion)
            .Include(i => i.CondicionSalud)
            .Include(i => i.Madre)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (inscripcion is null)
        {
            return null;
        }

        return new InscripcionCatequesisDetalleResponse
        {
            Id = inscripcion.Id,
            CentroCatequesis = inscripcion.CentroCatequesis,
            NivelAInscribirse = inscripcion.NivelAInscribirse,
            Estado = inscripcion.Estado,
            FechaSolicitud = inscripcion.FechaSolicitud,
            Catequizando = new CatequizandoDetalleResponse
            {
                Nombre = inscripcion.Catequizando.Nombre,
                Apellidos = inscripcion.Catequizando.Apellidos,
                FechaNacimiento = inscripcion.Catequizando.FechaNacimiento,
                DireccionExacta = inscripcion.Catequizando.DireccionExacta ?? string.Empty
            },
            Bautismo = new BautismoDetalleResponse
            {
                Parroquia = inscripcion.Bautismo.Parroquia ?? string.Empty,
                Fecha = inscripcion.Bautismo.Fecha,
                Tomo = inscripcion.Bautismo.Tomo ?? string.Empty,
                Folio = inscripcion.Bautismo.Folio ?? string.Empty,
                Asiento = inscripcion.Bautismo.Asiento ?? string.Empty
            },
            Adecuacion = new AdecuacionDetalleResponse
            {
                RequiereAdecuacionCentroEducativo = inscripcion.Adecuacion.RequiereAdecuacionCentroEducativo,
                DescripcionAdecuacion = inscripcion.Adecuacion.DescripcionAdecuacion ?? string.Empty
            },
            CondicionSalud = new CondicionSaludDetalleResponse
            {
                PortadorEnfermedadCronica = inscripcion.CondicionSalud.PortadorEnfermedadCronica,
                DescripcionEnfermedad = inscripcion.CondicionSalud.DescripcionEnfermedad ?? string.Empty
            },
            Madre = new MadreDetalleResponse
            {
                Nombre = inscripcion.Madre.Nombre,
                Apellidos = inscripcion.Madre.Apellidos,
                DireccionExacta = inscripcion.Madre.DireccionExacta ?? string.Empty,
                Ciudad = inscripcion.Madre.Ciudad ?? string.Empty,
                Provincia = inscripcion.Madre.Provincia ?? string.Empty,
                Telefono = inscripcion.Madre.Telefono
            }
        };
    }
}
