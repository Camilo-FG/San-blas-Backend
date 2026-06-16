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
}
