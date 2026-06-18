namespace SanblasBackend.Utils;

public static class InscripcionCatequesisValidaciones
{
    public static readonly string[] EstadosValidos = ["Pendiente", "Aprobada", "Rechazada"];
    public static readonly string[] NivelesValidos = ["Primero", "Sétimo"];

    public const string MensajeNivelInvalido = "El nivel a inscribirse solo puede ser Primero o Sétimo.";
    public const string MensajeEstadoInvalido = "El estado solo puede ser Pendiente, Aprobada o Rechazada.";
    public const string MensajeIdInvalido = "El id debe ser mayor que 0.";
    public const string MensajeNoEncontrado = "No se encontró una inscripción con el id indicado.";
    public const string MensajeFechaNacimientoFutura = "La fecha de nacimiento no puede ser futura.";
    public const string MensajeFechaBautismoFutura = "La fecha de bautismo no puede ser futura.";

    public static bool EsIdValido(int id) => id > 0;

    public static bool EsEstadoValido(string? estado, out string? estadoNormalizado)
    {
        estadoNormalizado = EstadosValidos
            .FirstOrDefault(e => e.Equals(estado, StringComparison.OrdinalIgnoreCase));

        return estadoNormalizado is not null;
    }

    public static bool EsNivelValido(string? nivel, out string? nivelNormalizado)
    {
        nivelNormalizado = NivelesValidos
            .FirstOrDefault(n => n.Equals(nivel, StringComparison.OrdinalIgnoreCase));

        return nivelNormalizado is not null;
    }

    public static bool ValidarFechaNoFutura(DateOnly? fecha)
    {
        return !fecha.HasValue || fecha.Value <= DateOnly.FromDateTime(DateTime.Today);
    }

    public static bool ValidarFechaNoFutura(DateOnly fecha)
    {
        return fecha <= DateOnly.FromDateTime(DateTime.Today);
    }
}
