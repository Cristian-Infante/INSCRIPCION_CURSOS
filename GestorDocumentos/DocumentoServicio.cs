using System;
using System.IO;
using Entidades;

namespace GestorDocumentos
{
    public class DocumentoServicio
    {
        public void GenerarCertificado(Inscripcion inscripcion, Persona persona, Curso curso)
        {
            // Ruta donde se guardará el certificado
            string rutaCertificados = "Certificados";
            if (!Directory.Exists(rutaCertificados))
            {
                Directory.CreateDirectory(rutaCertificados);
            }

            string nombreArchivo = $"Certificado_{persona.Nombre}_{curso.Nombre}_{inscripcion.Id}.txt";
            string rutaCompleta = Path.Combine(rutaCertificados, nombreArchivo);

            // Contenido del certificado
            string contenido = $"Certificado de Inscripción\n\n" +
                               $"Nombre del Estudiante: {persona.Nombre}\n" +
                               $"Curso Inscrito: {curso.Nombre}\n" +
                               $"Créditos del Curso: {curso.Creditos}\n" +
                               $"Fecha de Inscripción: {inscripcion.FechaInscripcion}\n" +
                               $"ID de Inscripción: {inscripcion.Id}\n";

            File.WriteAllText(rutaCompleta, contenido);
            Console.WriteLine($"[GestorDocumentos] Certificado generado en: {rutaCompleta}");
        }
    }
}
