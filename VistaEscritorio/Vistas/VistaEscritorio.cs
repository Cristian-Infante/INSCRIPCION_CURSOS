using Fachada;
using System;
using System.Windows.Forms;
using VistaEscritorio.Forms;

namespace VistaEscritorio.Vistas
{
    public class VistaEscritorio : Form
    {
        private Button btnInscribirCurso;
        private Button btnAgregarPersona;
        private Button btnAgregarCurso;
        private Button btnListarPersonas;
        private Button btnListarCursos;

        private InscripcionFachada _fachada;

        public VistaEscritorio()
        {
            _fachada = new InscripcionFachada();

            // Configuración del formulario
            this.Text = "Sistema de Inscripción de Cursos - Escritorio";
            this.Size = new System.Drawing.Size(400, 300);

            // Inicialización de botones
            btnInscribirCurso = new Button() { Text = "Inscribir a un curso", Left = 50, Top = 30, Width = 300 };
            btnAgregarPersona = new Button() { Text = "Agregar persona", Left = 50, Top = 70, Width = 300 };
            btnAgregarCurso = new Button() { Text = "Agregar curso", Left = 50, Top = 110, Width = 300 };
            btnListarPersonas = new Button() { Text = "Listar personas", Left = 50, Top = 150, Width = 300 };
            btnListarCursos = new Button() { Text = "Listar cursos", Left = 50, Top = 190, Width = 300 };

            // Eventos
            btnInscribirCurso.Click += BtnInscribirCurso_Click;
            btnAgregarPersona.Click += BtnAgregarPersona_Click;
            btnAgregarCurso.Click += BtnAgregarCurso_Click;
            btnListarPersonas.Click += BtnListarPersonas_Click;
            btnListarCursos.Click += BtnListarCursos_Click;

            // Agregar controles al formulario
            this.Controls.Add(btnInscribirCurso);
            this.Controls.Add(btnAgregarPersona);
            this.Controls.Add(btnAgregarCurso);
            this.Controls.Add(btnListarPersonas);
            this.Controls.Add(btnListarCursos);
        }

        private void BtnInscribirCurso_Click(object sender, EventArgs e)
        {
            var form = new InscribirCursoForm(_fachada);
            form.ShowDialog();
        }

        private void BtnAgregarPersona_Click(object sender, EventArgs e)
        {
            var form = new AgregarPersonaForm(_fachada);
            form.ShowDialog();
        }

        private void BtnAgregarCurso_Click(object sender, EventArgs e)
        {
            var form = new AgregarCursoForm(_fachada);
            form.ShowDialog();
        }

        private void BtnListarPersonas_Click(object sender, EventArgs e)
        {
            var form = new ListarPersonasForm(_fachada);
            form.ShowDialog();
        }

        private void BtnListarCursos_Click(object sender, EventArgs e)
        {
            var form = new ListarCursosForm(_fachada);
            form.ShowDialog();
        }
    }
}
