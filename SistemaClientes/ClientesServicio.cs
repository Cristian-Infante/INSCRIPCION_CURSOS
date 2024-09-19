using System;
using Entidades;

namespace SistemaClientes
{
    public class ClienteServicio
    {
        public void NotificarInscripcion(Persona persona, Curso curso)
        {
            // Implementar lógica de notificación. Por simplicidad, usaremos una salida en consola.
            Console.WriteLine($"[Notificación] Hola {persona.Nombre}, has sido inscrito exitosamente en el curso '{curso.Nombre}' el {DateTime.Now}.");
        }
    }
}