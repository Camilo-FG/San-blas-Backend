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
            FeBautismoArchivo = request.DatosInscripcion.FeBautismoArchivo,
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
            },
            PersonaInscribe = new PersonaInscribeCatequesis
            {
                Nombre = request.DatosPersonaInscribe.Nombre,
                Apellidos = request.DatosPersonaInscribe.Apellidos,
                Parentesco = request.DatosPersonaInscribe.Parentesco
            },
            Pago = new PagoInscripcionCatequesis
            {
                MetodoPago = request.DatosPago.MetodoPago,
                NumeroComprobanteSinpe = request.DatosPago.NumeroComprobanteSinpe,
                ComprobanteArchivo = request.DatosPago.ComprobanteArchivo,
                Monto = request.DatosPago.Monto
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
            .Include(i => i.Pago)
            .Include(i => i.PersonaInscribe)
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
            FeBautismoArchivo = inscripcion.FeBautismoArchivo,
            ObservacionAdministrativa = inscripcion.ObservacionAdministrativa,
            Catequizando = new CatequizandoDetalleResponse
            {
                Nombre = inscripcion.Catequizando?.Nombre ?? string.Empty,
                Apellidos = inscripcion.Catequizando?.Apellidos ?? string.Empty,
                FechaNacimiento = inscripcion.Catequizando?.FechaNacimiento ?? default,
                DireccionExacta = inscripcion.Catequizando?.DireccionExacta ?? string.Empty
            },
            Bautismo = new BautismoDetalleResponse
            {
                Parroquia = inscripcion.Bautismo?.Parroquia ?? string.Empty,
                Fecha = inscripcion.Bautismo?.Fecha,
                Tomo = inscripcion.Bautismo?.Tomo ?? string.Empty,
                Folio = inscripcion.Bautismo?.Folio ?? string.Empty,
                Asiento = inscripcion.Bautismo?.Asiento ?? string.Empty
            },
            Adecuacion = new AdecuacionDetalleResponse
            {
                RequiereAdecuacionCentroEducativo = inscripcion.Adecuacion?.RequiereAdecuacionCentroEducativo,
                DescripcionAdecuacion = inscripcion.Adecuacion?.DescripcionAdecuacion ?? string.Empty
            },
            CondicionSalud = new CondicionSaludDetalleResponse
            {
                PortadorEnfermedadCronica = inscripcion.CondicionSalud?.PortadorEnfermedadCronica,
                DescripcionEnfermedad = inscripcion.CondicionSalud?.DescripcionEnfermedad ?? string.Empty
            },
            Madre = new MadreDetalleResponse
            {
                Nombre = inscripcion.Madre?.Nombre ?? string.Empty,
                Apellidos = inscripcion.Madre?.Apellidos ?? string.Empty,
                DireccionExacta = inscripcion.Madre?.DireccionExacta ?? string.Empty,
                Ciudad = inscripcion.Madre?.Ciudad ?? string.Empty,
                Provincia = inscripcion.Madre?.Provincia ?? string.Empty,
                Telefono = inscripcion.Madre?.Telefono ?? string.Empty
            },
            PersonaInscribe = new PersonaInscribeDetalleResponse
            {
                Nombre = inscripcion.PersonaInscribe?.Nombre ?? string.Empty,
                Apellidos = inscripcion.PersonaInscribe?.Apellidos ?? string.Empty,
                Parentesco = inscripcion.PersonaInscribe?.Parentesco ?? string.Empty
            },
            Pago = new PagoDetalleResponse
            {
                MetodoPago = inscripcion.Pago?.MetodoPago ?? string.Empty,
                NumeroComprobanteSinpe = inscripcion.Pago?.NumeroComprobanteSinpe ?? string.Empty,
                ComprobanteArchivo = inscripcion.Pago?.ComprobanteArchivo ?? string.Empty,
                Monto = inscripcion.Pago?.Monto ?? 0
            }
        };
    }

    public async Task<ActualizarEstadoInscripcionCatequesisResponse?> ActualizarEstadoAsync(
        int id,
        ActualizarEstadoInscripcionCatequesisRequest request)
    {
        var inscripcion = await _context.InscripcionesCatequesis.FindAsync(id);

        if (inscripcion is null)
        {
            return null;
        }

        inscripcion.Estado = request.Estado;
        inscripcion.ObservacionAdministrativa = request.Observacion;
        inscripcion.FechaActualizacionEstado = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new ActualizarEstadoInscripcionCatequesisResponse
        {
            Id = inscripcion.Id,
            Mensaje = "Estado de inscripción actualizado correctamente",
            Estado = inscripcion.Estado,
            ObservacionAdministrativa = inscripcion.ObservacionAdministrativa,
            FechaActualizacionEstado = inscripcion.FechaActualizacionEstado!.Value
        };
    }
}
