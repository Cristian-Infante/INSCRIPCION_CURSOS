using Entidades;
using Fachada;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VistaEscritorio.Forms
{
    public class ListarCursosForm : Form
    {
        private InscripcionFachada _fachada;
        private DataGridView dgvCursos;

        public ListarCursosForm(InscripcionFachada fachada)
        {
            _fachada = fachada;
            this.Text = "Listado de cursos";
            this.Size = new System.Drawing.Size(600, 400);

            dgvCursos = new DataGridView()
            {
                Left = 10,
                Top = 10,
                Width = 560,
                Height = 340,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            this.Controls.Add(dgvCursos);
            CargarCursos();
        }

        private void CargarCursos()
        {
            List<Curso> cursos = _fachada.ObtenerCursos();
            dgvCursos.DataSource = cursos;
        }
    }
}
