using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs;
using SanblasBackend.Models;

namespace SanblasBackend.Services;

public class LandingContentService : ILandingContentService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
    };

    private readonly GlobalContex _context;

    public LandingContentService(GlobalContex context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<LandingSectionDto>> GetAllSectionsAsync()
    {
        await EnsureSeededAsync();

        var sections = await _context.LandingContents
            .OrderBy(section => section.SectionKey)
            .ToListAsync();

        return sections.Select(MapToDto).ToList();
    }

    public async Task<LandingSectionDto?> GetSectionAsync(string sectionKey)
    {
        await EnsureSeededAsync();

        var normalizedKey = NormalizeKey(sectionKey);
        var section = await _context.LandingContents
            .FirstOrDefaultAsync(item => item.SectionKey == normalizedKey);

        return section is null ? null : MapToDto(section);
    }

    public async Task<LandingSectionDto?> UpdateSectionAsync(string sectionKey, object data)
    {
        await EnsureSeededAsync();

        var normalizedKey = NormalizeKey(sectionKey);
        var section = await _context.LandingContents
            .FirstOrDefaultAsync(item => item.SectionKey == normalizedKey);

        if (section is null)
            return null;

        section.JsonData = JsonSerializer.Serialize(data, JsonOptions);
        section.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToDto(section);
    }

    private async Task EnsureSeededAsync()
    {
        var defaults = GetDefaultSections();

        foreach (var (key, json) in defaults)
        {
            var exists = await _context.LandingContents.AnyAsync(section => section.SectionKey == key);
            if (exists)
                continue;

            _context.LandingContents.Add(new LandingContent
            {
                SectionKey = key,
                JsonData = json,
                UpdatedAt = DateTime.UtcNow,
            });
        }

        if (_context.ChangeTracker.HasChanges())
            await _context.SaveChangesAsync();
    }

    private static LandingSectionDto MapToDto(LandingContent section)
    {
        object data;
        try
        {
            data = JsonSerializer.Deserialize<object>(section.JsonData, JsonOptions) ?? new();
        }
        catch
        {
            data = new { raw = section.JsonData };
        }

        return new LandingSectionDto
        {
            SectionKey = section.SectionKey,
            Data = data,
            UpdatedAt = section.UpdatedAt,
        };
    }

    private static string NormalizeKey(string sectionKey) =>
        sectionKey.Trim().ToLowerInvariant();

    private static Dictionary<string, string> GetDefaultSections() => new()
    {
        ["hero"] = JsonSerializer.Serialize(new
        {
            subtitle = "Desde 1544",
            title = "Firme en la",
            titleHighlight = "Fe y Tradición",
            description = "Ubicada en el corazón de Nicoya, la Parroquia San Blas es testimonio vivo de nuestra historia y esperanza cristiana.",
        }, JsonOptions),
        ["sobre-nosotros"] = JsonSerializer.Serialize(new
        {
            eyebrow = "Sobre Nosotros",
            title = "Una parroquia que guarda la fe, la historia y la cercanía de Nicoya",
            lead = "La Parroquia San Blas de Nicoya es un referente espiritual y cultural de Costa Rica. Su historia, su misión pastoral y su vocación de servicio siguen acompañando a una comunidad viva, hospitalaria y profundamente creyente.",
            cards = new[]
            {
                new { icono = "01", titulo = "Raíz histórica", texto = "La Parroquia San Blas ha acompañado la vida espiritual de Nicoya desde sus orígenes, siendo parte esencial de la memoria religiosa y cultural de Costa Rica." },
                new { icono = "02", titulo = "Identidad cultural", texto = "Su presencia ha contribuido a preservar tradiciones, celebraciones y expresiones de fe que fortalecen el sentido de pertenencia de la comunidad nicoyana." },
                new { icono = "03", titulo = "Misión espiritual", texto = "Nuestra misión es anunciar el Evangelio, celebrar los sacramentos y sostener la fe del pueblo con una pastoral cercana, serena y comprometida." },
                new { icono = "04", titulo = "Servicio a la comunidad", texto = "Acompañamos a niños, jóvenes, adultos mayores y familias con catequesis, formación y espacios de servicio que buscan unir fe y vida diaria." },
            },
        }, JsonOptions),
        ["contacto"] = JsonSerializer.Serialize(new
        {
            title = "Contacto",
            intro = "Póngase en contacto con la Parroquia San Blas para consultas, dudas o información adicional.",
            telefono = "+506 0000-0000",
            correo = "parroquiasanblas@gmail.com",
            ubicacion = "Nicoya, Guanacaste, Costa Rica",
            horariosAtencion = new[]
            {
                "Lunes a Viernes: 8:00 a.m. a 12:00 m.d. y 1:30 p.m. a 5:00 p.m.",
                "Sábados: 8:00 a.m. a 12:00 m.d.",
            },
            mapaUrl = "https://maps.google.com/maps?q=Parroquia%20San%20Blas,%20Nicoya,%20Guanacaste&t=&z=16&ie=UTF8&iwloc=&output=embed",
        }, JsonOptions),
        ["horarios"] = JsonSerializer.Serialize(new
        {
            title = "Horarios Parroquiales",
            intro = "Consulte nuestros horarios de misas, confesiones y atención en la Oficina Parroquial San Blas.",
            bloques = new[]
            {
                new { titulo = "Horario de Misas (Entre Semana)", items = new[] { "Lunes a Jueves: 6:00 p.m.", "Viernes: 6:00 p.m." } },
                new { titulo = "Horario de Misas (Fines de Semana)", items = new[] { "Sábados: 4:00 p.m. y 6:00 p.m.", "Domingos: 7:00 a.m. - 9:00 a.m. - 4:00 p.m. - 6:00 p.m." } },
                new { titulo = "Horarios de Confesiones", items = new[] { "Jueves: Durante la Hora Santa (Después de la misa de 6:00 p.m.).", "Viernes: 4:00 p.m. a 5:30 p.m." } },
                new { titulo = "Atención en Oficina Parroquial", items = new[] { "Lunes a Viernes: 8:00 a.m. a 12:00 m.d. y 1:30 p.m. a 5:00 p.m.", "Sábados: 8:00 a.m. a 12:00 m.d. (Cerrado por la tarde)." } },
            },
        }, JsonOptions),
        ["bautizos"] = JsonSerializer.Serialize(new
        {
            title = "Información de Bautizos",
            intro = "El bautismo es el primer sacramento de la iniciación cristiana. Para bautizar en la Parroquia San Blas, por favor tome en cuenta la siguiente información.",
            requisitos = new[]
            {
                "Copia de la cédula de los padres o pasaporte si son extranjeros.",
                "Copia de la cédula de los padrinos.",
                "Comprobante de las charlas de preparación al bautismo.",
                "Certificado de nacimiento del niño/a.",
            },
            charlas = "Las charlas se imparten los segundos martes de cada mes de forma presencial en el salón parroquial. Es indispensable presentar el comprobante para la programación del sacramento.",
            solicitud = "Para solicitar o programar un bautizo, puede acercarse a la oficina parroquial con los documentos requeridos o enviarlos a través de nuestro apartado de Solicitudes de Sacramentos.",
        }, JsonOptions),
    };
}
