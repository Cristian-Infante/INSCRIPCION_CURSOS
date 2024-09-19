using Controlador;
using Entidades;
using LogicaNegocio;
using Persistencia;

namespace Fachada;

public class InscripcionFachada
{
    private readonly InscripcionControlador _controlador;

    public InscripcionFachada()
    {
        var servicio = new InscripcionServicio();
        _controlador = new InscripcionControlador(servicio);
    }

    public void InscribirCurso(int personaId, int cursoId)
    {
        _controlador.InscribirCurso(personaId, cursoId);
    }

    public void AgregarPersona(Persona persona)
    {
        try
        {
            var repositorio = Repositorio.Instancia();
            repositorio.AgregarPersona(persona);
            Console.WriteLine("Persona agregada exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al agregar persona: {ex.Message}");
        }
    }

    public void AgregarCurso(Curso curso)
    {
        try
        {
            var repositorio = Repositorio.Instancia();
            repositorio.AgregarCurso(curso);
            Console.WriteLine("Curso agregado exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al agregar curso: {ex.Message}");
        }
    }

    // Nuevos métodos para obtener personas y cursos
    public List<Persona> ObtenerPersonas()
    {
        try
        {
            var repositorio = Repositorio.Instancia();
            return repositorio.ObtenerTodasPersonas();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener personas: {ex.Message}");
            return new List<Persona>();
        }
    }

    public List<Curso> ObtenerCursos()
    {
        try
        {
            var repositorio = Repositorio.Instancia();
            return repositorio.ObtenerTodosCursos();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener cursos: {ex.Message}");
            return new List<Curso>();
        }
    }
}