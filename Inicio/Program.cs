using VistaEscritorio;
using VistaConsola;

namespace INSCRIPCION_CURSOS
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Lanzar la VistaEscritorio en un hilo separado
            Thread threadVistaEscritorio = new Thread(() =>
            {
                var vistaEscritorio = new VistaEscritorio.Vistas.VistaEscritorio();
                vistaEscritorio.Show();  // Usa Show() para que no sea modal
                System.Windows.Forms.Application.Run(); // Necesario para manejar el ciclo de vida de Windows Forms
            });

            threadVistaEscritorio.SetApartmentState(ApartmentState.STA); // Requerido para aplicaciones Windows Forms
            threadVistaEscritorio.Start();

            // Ejecutar la VistaConsola en el hilo principal
            var vistaConsola = new VistaConsola.VistaConsola();
            vistaConsola.MostrarMenu();
        }
    }
}
