using Entidades;
using Fachada;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VistaEscritorio.Forms
{
    public class ListarPersonasForm : Form
    {
        private InscripcionFachada _fachada;
        private DataGridView dgvPersonas;

        public ListarPersonasForm(InscripcionFachada fachada)
        {
            _fachada = fachada;
            this.Text = "Listado de personas";
            this.Size = new System.Drawing.Size(600, 400);

            dgvPersonas = new DataGridView()
            {
                Left = 10,
                Top = 10,
                Width = 560,
                Height = 340,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            this.Controls.Add(dgvPersonas);
            CargarPersonas();
        }

        private void CargarPersonas()
        {
            List<Persona> personas = _fachada.ObtenerPersonas();
            dgvPersonas.DataSource = personas;
        }
    }
}
