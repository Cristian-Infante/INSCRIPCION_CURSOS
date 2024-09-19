using Fabrica_de_Objetos;
using Fachada;

using System;
using System.Windows.Forms;

namespace VistaEscritorio.Forms
{
    public class AgregarCursoForm : Form
    {
        private InscripcionFachada _fachada;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblCreditos;
        private TextBox txtCreditos;
        private Button btnAgregar;

        public AgregarCursoForm(InscripcionFachada fachada)
        {
            _fachada = fachada;
            this.Text = "Agregar curso";
            this.Size = new System.Drawing.Size(300, 200);

            lblNombre = new Label() { Text = "Nombre:", Left = 10, Top = 20 };
            txtNombre = new TextBox() { Left = 120, Top = 20, Width = 150 };

            lblCreditos = new Label() { Text = "Créditos:", Left = 10, Top = 60 };
            txtCreditos = new TextBox() { Left = 120, Top = 60, Width = 150 };

            btnAgregar = new Button() { Text = "Agregar", Left = 100, Top = 100, Width = 100 };
            btnAgregar.Click += BtnAgregar_Click;

            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);
            this.Controls.Add(lblCreditos);
            this.Controls.Add(txtCreditos);
            this.Controls.Add(btnAgregar);
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Por favor ingrese un nombre válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (int.TryParse(txtCreditos.Text, out int creditos))
            {
                var curso = EntidadFactory.CrearCurso(nombre, creditos);
                _fachada.AgregarCurso(curso);
                MessageBox.Show($"Curso agregado con ID: {curso.Id}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor ingrese un número válido de créditos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
