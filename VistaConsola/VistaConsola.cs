using Fachada;
using Entidades;
using Fabrica_de_Objetos;

namespace VistaConsola;

public class VistaConsola
{
    private readonly InscripcionFachada _fachada;

    public VistaConsola()
    {
        _fachada = new InscripcionFachada();
    }

    public void MostrarMenu()
    {
        while (true)
        {
            //Console.Clear();
            Console.WriteLine("Sistema de Inscripción de Cursos");
            Console.WriteLine("1. Inscribir a un curso");
            Console.WriteLine("2. Agregar persona");
            Console.WriteLine("3. Agregar curso");
            Console.WriteLine("4. Listar personas");
            Console.WriteLine("5. Listar cursos");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    InscribirCurso();
                    break;
                case "2":
                    AgregarPersona();
                    break;
                case "3":
                    AgregarCurso();
                    break;
                case "4":
                    ListarPersonas();
                    break;
                case "5":
                    ListarCursos();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }

    private void InscribirCurso()
    {
        Console.Write("Ingrese el ID de la persona: ");
        if (!int.TryParse(Console.ReadLine(), out int personaId))
        {
            Console.WriteLine("ID de persona inválido.");
            return;
        }

        Console.Write("Ingrese el ID del curso: ");
        if (!int.TryParse(Console.ReadLine(), out int cursoId))
        {
            Console.WriteLine("ID de curso inválido.");
            return;
        }

        _fachada.InscribirCurso(personaId, cursoId);
    }

    private void AgregarPersona()
    {
        Console.Write("Ingrese el nombre de la persona: ");
        string? nombre = Console.ReadLine();

        Console.Write("Ingrese los créditos máximos: ");
        if (!int.TryParse(Console.ReadLine(), out int creditosMaximos))
        {
            Console.WriteLine("Créditos inválidos.");
            return;
        }

        var persona = EntidadFactory.CrearPersona(nombre, creditosMaximos);
        _fachada.AgregarPersona(persona);
        Console.WriteLine($"Persona agregada con ID: {persona.Id}");
    }

    private void AgregarCurso()
    {
        Console.Write("Ingrese el nombre del curso: ");
        string? nombre = Console.ReadLine();

        Console.Write("Ingrese los créditos del curso: ");
        if (!int.TryParse(Console.ReadLine(), out int creditos))
        {
            Console.WriteLine("Créditos inválidos.");
            return;
        }

        var curso = EntidadFactory.CrearCurso(nombre, creditos);
        _fachada.AgregarCurso(curso);
        Console.WriteLine($"Curso agregado con ID: {curso.Id}");
    }

    // Nueva funcionalidad: Listar Personas
    private void ListarPersonas()
    {
        List<Persona> personas = _fachada.ObtenerPersonas();
        Console.WriteLine("Listado de personas:");
        foreach (var persona in personas)
        {
            Console.WriteLine($"ID: {persona.Id}, Nombre: {persona.Nombre}, Créditos Máximos: {persona.CreditosMaximos}, Créditos Actuales: {persona.CreditosActuales}");
        }
    }

    // Nueva funcionalidad: Listar Cursos
    private void ListarCursos()
    {
        List<Curso> cursos = _fachada.ObtenerCursos();
        Console.WriteLine("Listado de cursos:");
        foreach (var curso in cursos)
        {
            Console.WriteLine($"ID: {curso.Id}, Nombre: {curso.Nombre}, Créditos: {curso.Creditos}");
        }
    }
}