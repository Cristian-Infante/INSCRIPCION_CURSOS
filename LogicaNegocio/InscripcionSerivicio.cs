using Entidades;
using GestorDocumentos;
using Microsoft.Data.Sqlite;
using Persistencia;
using SistemaClientes;

namespace LogicaNegocio;

public class InscripcionServicio : IInscripcionServicio
{
    private readonly Repositorio _repositorio;
    private readonly ClienteServicio _clienteServicio;
    private readonly DocumentoServicio _documentoServicio;

    public InscripcionServicio()
    {
        _repositorio = Repositorio.Instancia();
        _clienteServicio = new ClienteServicio();
        _documentoServicio = new DocumentoServicio();
    }

    public void InscribirCurso(int personaId, int cursoId)
    {
        _repositorio.EjecutarTransaccion((conexion, transaccion) =>
        {
            // Obtener la persona
            var persona = _repositorio.ObtenerPersonaPorId(personaId);
            if (persona == null) throw new Exception("Persona no encontrada");

            // Obtener el curso
            var curso = _repositorio.ObtenerCursoPorId(cursoId);
            if (curso == null) throw new Exception("Curso no encontrado");

            // Verificar si se pueden inscribir los créditos
            if (persona.CreditosActuales + curso.Creditos > persona.CreditosMaximos)
                throw new Exception("Créditos máximos excedidos");

            // Actualizar créditos de la persona
            var cmdActualizar = new SqliteCommand("UPDATE Personas SET CreditosActuales = CreditosActuales + @Creditos WHERE Id = @Id", conexion, transaccion);
            cmdActualizar.Parameters.AddWithValue("@Creditos", curso.Creditos);
            cmdActualizar.Parameters.AddWithValue("@Id", personaId);
            cmdActualizar.ExecuteNonQuery();

            // Insertar inscripción
            var inscripcion = new Inscripcion
            {
                PersonaId = personaId,
                CursoId = cursoId,
                FechaInscripcion = DateTime.Now
            };
            _repositorio.AgregarInscripcion(inscripcion);

            // Notificar al cliente
            _clienteServicio.NotificarInscripcion(persona, curso);

            // Generar certificado
            _documentoServicio.GenerarCertificado(inscripcion, persona, curso);
        });
    }
}
