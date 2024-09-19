namespace Entidades;

public class Inscripcion
{
    public int Id { get; set; }
    public int PersonaId { get; set; }
    public int CursoId { get; set; }
    public DateTime FechaInscripcion { get; set; }
}