using Entidades;

namespace Fabrica_de_Objetos;

public static class EntidadFactory
{
    public static Persona CrearPersona(string? nombre, int creditosMaximos)
    {
        if (string.IsNullOrEmpty(nombre))
        {
            throw new ArgumentNullException(nameof(nombre), "El nombre no puede ser nulo o vacío.");
        }
        
        return new Persona
        {
            Nombre = nombre,
            CreditosMaximos = creditosMaximos,
            CreditosActuales = 0
        };
    }

    public static Curso CrearCurso(string? nombre, int creditos)
    {
        if (string.IsNullOrEmpty(nombre))
        {
            throw new ArgumentNullException(nameof(nombre), "El nombre no puede ser nulo o vacío.");
        }
        
        return new Curso
        {
            Nombre = nombre,
            Creditos = creditos
        };
    }
}