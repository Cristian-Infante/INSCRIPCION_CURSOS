using Entidades;
using Microsoft.Data.Sqlite;

namespace Persistencia;

public class Repositorio
{
    private readonly string _cadenaConexion;
    private static Repositorio? _instancia;
    private static readonly object _lock = new object();

    private Repositorio(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }

    // Implementación Singleton
    public static Repositorio Instancia()
    {
        string directorioActual = AppDomain.CurrentDomain.BaseDirectory;
        string directorioProyecto = Path.GetFullPath(Path.Combine(directorioActual, @"..\..\.."));
        string rutaDb = Path.Combine(directorioProyecto, "InscripcionCursos.db");

        Console.WriteLine("Obteniendo instancia...");
        if (_instancia == null)
        {
            Console.WriteLine("Instanciando Repositorio...");
            lock (_lock)
            {
                Console.WriteLine("Dentro del lock...");
                if (_instancia == null)
                {
                    Console.WriteLine("Creando instancia...");
                    _instancia = new Repositorio($"Data Source={rutaDb};Default Timeout=30;");
                    _instancia.EnsureCreated();
                }
            }
        }
        return _instancia;
    }

    private void EnsureCreated()
    {
        Console.WriteLine("Creando tablas...");
        using (var conexion = new SqliteConnection(_cadenaConexion))
        {
            conexion.Open();
            // Crear tabla Personas
            var cmdCrearPersonas = new SqliteCommand(@"
                CREATE TABLE IF NOT EXISTS Personas (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    CreditosMaximos INTEGER NOT NULL,
                    CreditosActuales INTEGER NOT NULL DEFAULT 0
                );", conexion);
            cmdCrearPersonas.ExecuteNonQuery();

            // Crear tabla Cursos
            var cmdCrearCursos = new SqliteCommand(@"
                CREATE TABLE IF NOT EXISTS Cursos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Creditos INTEGER NOT NULL
                );", conexion);
            cmdCrearCursos.ExecuteNonQuery();

            // Crear tabla Inscripciones
            var cmdCrearInscripciones = new SqliteCommand(@"
                CREATE TABLE IF NOT EXISTS Inscripciones (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    PersonaId INTEGER,
                    CursoId INTEGER,
                    FechaInscripcion TEXT,
                    FOREIGN KEY (PersonaId) REFERENCES Personas(Id),
                    FOREIGN KEY (CursoId) REFERENCES Cursos(Id)
                );", conexion);
            cmdCrearInscripciones.ExecuteNonQuery();
        }
    }
    
    public void EjecutarTransaccion(Action<SqliteConnection, SqliteTransaction> accion)
    {
        using (var conexion = new SqliteConnection(_cadenaConexion))
        {
            conexion.Open();
            using (var transaccion = conexion.BeginTransaction())
            {
                try
                {
                    accion(conexion, transaccion);
                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    Console.WriteLine($"Error durante la transacción: {ex.Message}");
                    throw;
                }
            }
        }
    }

    // Métodos para CRUD de Personas, Cursos e Inscripciones
    public void AgregarPersona(Persona persona)
    {
        EjecutarTransaccion((conexion, transaccion) =>
        {
            var cmd = new SqliteCommand("INSERT INTO Personas (Nombre, CreditosMaximos, CreditosActuales) VALUES (@Nombre, @CreditosMaximos, @CreditosActuales)", conexion, transaccion);
            cmd.Parameters.AddWithValue("@Nombre", persona.Nombre);
            cmd.Parameters.AddWithValue("@CreditosMaximos", persona.CreditosMaximos);
            cmd.Parameters.AddWithValue("@CreditosActuales", persona.CreditosActuales);
            cmd.ExecuteNonQuery();
            persona.Id = Convert.ToInt32((long)new SqliteCommand("SELECT last_insert_rowid()", conexion, transaccion).ExecuteScalar());
        });
    }

    public Persona? ObtenerPersonaPorId(int id, SqliteConnection conexion, SqliteTransaction transaccion)
    {
        Console.WriteLine($"Buscando persona con ID: {id}");
        var cmd = new SqliteCommand("SELECT * FROM Personas WHERE Id = @Id", conexion, transaccion);
        cmd.Parameters.AddWithValue("@Id", id);

        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                return new Persona
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    CreditosMaximos = reader.GetInt32(reader.GetOrdinal("CreditosMaximos")),
                    CreditosActuales = reader.GetInt32(reader.GetOrdinal("CreditosActuales"))
                };
            }
        }
        return null;
    }

    public void AgregarCurso(Curso curso)
    {
        EjecutarTransaccion((conexion, transaccion) =>
        {
            var cmd = new SqliteCommand("INSERT INTO Cursos (Nombre, Creditos) VALUES (@Nombre, @Creditos)", conexion, transaccion);
            cmd.Parameters.AddWithValue("@Nombre", curso.Nombre);
            cmd.Parameters.AddWithValue("@Creditos", curso.Creditos);
            cmd.ExecuteNonQuery();
            curso.Id = Convert.ToInt32((long)new SqliteCommand("SELECT last_insert_rowid()", conexion, transaccion).ExecuteScalar());
        });
    }

    public Curso? ObtenerCursoPorId(int id, SqliteConnection conexion, SqliteTransaction transaccion)
    {
        var cmd = new SqliteCommand("SELECT * FROM Cursos WHERE Id = @Id", conexion, transaccion);
        cmd.Parameters.AddWithValue("@Id", id);
        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                return new Curso
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Creditos = reader.GetInt32(reader.GetOrdinal("Creditos"))
                };
            }
        }
        return null;
    }

    public void AgregarInscripcion(Inscripcion inscripcion, SqliteConnection conexion, SqliteTransaction transaccion)
    {
        var cmd = new SqliteCommand("INSERT INTO Inscripciones (PersonaId, CursoId, FechaInscripcion) VALUES (@PersonaId, @CursoId, @FechaInscripcion)", conexion, transaccion);
        cmd.Parameters.AddWithValue("@PersonaId", inscripcion.PersonaId);
        cmd.Parameters.AddWithValue("@CursoId", inscripcion.CursoId);
        cmd.Parameters.AddWithValue("@FechaInscripcion", inscripcion.FechaInscripcion.ToString("o")); // Formato ISO 8601
        cmd.ExecuteNonQuery();
        inscripcion.Id = Convert.ToInt32((long)new SqliteCommand("SELECT last_insert_rowid()", conexion, transaccion).ExecuteScalar());
    }

    // Métodos para listar Personas y Cursos
    public List<Persona> ObtenerTodasPersonas()
    {
        var personas = new List<Persona>();
        using (var conexion = new SqliteConnection(_cadenaConexion))
        {
            conexion.Open();
            using (var transaccion = conexion.BeginTransaction())
            {
                var cmd = new SqliteCommand("SELECT * FROM Personas", conexion, transaccion);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        personas.Add(new Persona
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            CreditosMaximos = reader.GetInt32(reader.GetOrdinal("CreditosMaximos")),
                            CreditosActuales = reader.GetInt32(reader.GetOrdinal("CreditosActuales"))
                        });
                    }
                }
            }
        }
        return personas;
    }

    public List<Curso> ObtenerTodosCursos()
    {
        var cursos = new List<Curso>();
        using (var conexion = new SqliteConnection(_cadenaConexion))
        {
            conexion.Open();
            using (var transaccion = conexion.BeginTransaction())
            {
                var cmd = new SqliteCommand("SELECT * FROM Cursos", conexion, transaccion);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cursos.Add(new Curso
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Creditos = reader.GetInt32(reader.GetOrdinal("Creditos"))
                        });
                    }
                }
                
            }
        }
        return cursos;
    }
}
