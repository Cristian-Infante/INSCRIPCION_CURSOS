using LogicaNegocio;

namespace Controlador;

public class InscripcionControlador
{
    private readonly IInscripcionServicio _inscripcionServicio;

    public InscripcionControlador(IInscripcionServicio inscripcionServicio)
    {
        _inscripcionServicio = inscripcionServicio;
    }

    public void InscribirCurso(int personaId, int cursoId)
    {
        try
        {
            _inscripcionServicio.InscribirCurso(personaId, cursoId);
            Console.WriteLine("Inscripción exitosa.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al inscribir: {ex.Message}");
        }
    }
}
