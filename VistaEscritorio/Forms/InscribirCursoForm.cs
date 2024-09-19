using Fachada;

using System;
using System.Windows.Forms;

namespace VistaEscritorio.Forms
{
    public class InscribirCursoForm : Form
    {
        private InscripcionFachada _fachada;
        private Label lblPersonaId;
        private TextBox txtPersonaId;
        private Label lblCursoId;
        private TextBox txtCursoId;
        private Button btnInscribir;

        public InscribirCursoForm(InscripcionFachada fachada)
        {
            _fachada = fachada;
            this.Text = "Inscribir a un curso";
            this.Size = new System.Drawing.Size(300, 200);

            lblPersonaId = new Label() { Text = "ID de la persona:", Left = 10, Top = 20 };
            txtPersonaId = new TextBox() { Left = 120, Top = 20, Width = 150 };

            lblCursoId = new Label() { Text = "ID del curso:", Left = 10, Top = 60 };
            txtCursoId = new TextBox() { Left = 120, Top = 60, Width = 150 };

            btnInscribir = new Button() { Text = "Inscribir", Left = 100, Top = 100, Width = 100 };
            btnInscribir.Click += BtnInscribir_Click;

            this.Controls.Add(lblPersonaId);
            this.Controls.Add(txtPersonaId);
            this.Controls.Add(lblCursoId);
            this.Controls.Add(txtCursoId);
            this.Controls.Add(btnInscribir);
        }

        private void BtnInscribir_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtPersonaId.Text, out int personaId) && int.TryParse(txtCursoId.Text, out int cursoId))
            {
                try
                {
                    _fachada.InscribirCurso(personaId, cursoId);
                    MessageBox.Show("Inscripción exitosa.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al inscribir: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor ingrese IDs válidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
