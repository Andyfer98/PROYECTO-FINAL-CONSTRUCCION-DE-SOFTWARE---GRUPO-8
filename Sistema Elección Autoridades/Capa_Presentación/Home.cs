using Capa_Entidades;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Presentación
{
    public partial class Home : Form
    {

        NegocioCandidatos negCand = new NegocioCandidatos();
        NegocioVotacion negVot = new NegocioVotacion();
        int EstudianteId;

        public Home(int estudianteId)
        {
            InitializeComponent();
            EstudianteId = estudianteId;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            List<Candidato> listCandidatas = negCand.get();
            for (int i = 0; i < listCandidatas.Count; i++)
            {
                string binFilePath = Path.Combine(Application.StartupPath, "..", "..", "Imagenes", $"{listCandidatas[i].NombreImagen}");

                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Width = 150;
                pictureBox.Height = 100;
                pictureBox.Padding = new Padding(5);
                pictureBox.Location = new Point(pictureBox.Location.Y, 19);

                using (FileStream fs = new FileStream(binFilePath, FileMode.Open))
                {
                    byte[] imageBytes = new byte[fs.Length];
                    fs.Read(imageBytes, 0, imageBytes.Length);

                    Bitmap image = (Bitmap)Image.FromStream(new MemoryStream(imageBytes));
                    pictureBox.Image = image;
                }

                Label titleLabel = new Label();
                titleLabel.Font = new Font("Cooper Black", 8);
                titleLabel.Text = listCandidatas[i].NombresCompletos;

                Button button = new Button();
                button.Text = "VOTAR";
                button.Width = 110;
                button.Cursor = Cursors.Hand;
                button.Location = new Point(pictureBox.Location.Y, 130);
                int currentId = listCandidatas[i].Id;
                button.Click += (_sender, _e) => Button_Click(sender, e, currentId);

                GroupBox groupBox = new GroupBox();
                groupBox.Width = 150;
                groupBox.Height = 170;
                groupBox.Margin = new Padding(20);

                groupBox.Controls.Add(pictureBox);
                groupBox.Controls.Add(titleLabel);
                groupBox.Controls.Add(button);

                flowLayoutPanel1.Controls.Add(groupBox);
            }
        }

        private void Button_Click(object sender, EventArgs e, int id)
        {
            Votacion votacion = new Votacion();
            votacion.Estudiante = new Estudiante { Id = EstudianteId };
            votacion.Candidato = new Candidato { Id = id };

            int resultado = negVot.votar(votacion);
            if (resultado == 1)
            {
                MessageBox.Show("Votación exitosa", "Enhorabuena");
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error", "Intenta de nuevo");
            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = negVot.ObtenerCandidatosMasVotados();

                if (!string.IsNullOrEmpty(resultado))
                {
                    MessageBox.Show(resultado, "Candidatos Más Votados");
                }
                else
                {
                    MessageBox.Show("No hay información disponible sobre los candidatos más votados.", "Información");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener candidatos más votados: {ex.Message}", "Error");
            }
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
        }
    }
}
