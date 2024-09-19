using Fabrica_de_Objetos;
using Fachada;

using System;
using System.Windows.Forms;

namespace VistaEscritorio.Forms
{
    public class AgregarPersonaForm : Form
    {
        private InscripcionFachada _fachada;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblCreditosMaximos;
        private TextBox txtCreditosMaximos;
        private Button btnAgregar;

        public AgregarPersonaForm(InscripcionFachada fachada)
        {
            _fachada = fachada;
            this.Text = "Agregar persona";
            this.Size = new System.Drawing.Size(300, 200);

            lblNombre = new Label() { Text = "Nombre:", Left = 10, Top = 20 };
            txtNombre = new TextBox() { Left = 120, Top = 20, Width = 150 };

            lblCreditosMaximos = new Label() { Text = "Créditos máximos:", Left = 10, Top = 60 };
            txtCreditosMaximos = new TextBox() { Left = 120, Top = 60, Width = 150 };

            btnAgregar = new Button() { Text = "Agregar", Left = 100, Top = 100, Width = 100 };
            btnAgregar.Click += BtnAgregar_Click;

            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);
            this.Controls.Add(lblCreditosMaximos);
            this.Controls.Add(txtCreditosMaximos);
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

            if (int.TryParse(txtCreditosMaximos.Text, out int creditosMaximos))
            {
                var persona = EntidadFactory.CrearPersona(nombre, creditosMaximos);
                _fachada.AgregarPersona(persona);
                MessageBox.Show($"Persona agregada con ID: {persona.Id}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor ingrese un número válido de créditos máximos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
