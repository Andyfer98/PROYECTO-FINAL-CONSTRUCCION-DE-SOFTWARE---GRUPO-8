using Capa_Entidades;
using Capa_Negocio;
using Capa_Presentación.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Presentación
{
    public partial class FormCandidato : Form
    {

        SubirImagen subirImagen = new SubirImagen();
        NegocioCandidatos negocioCandidatos;
        private string nameFile = "";

        public FormCandidato()
        {
            InitializeComponent();
            negocioCandidatos = new NegocioCandidatos();
        }

        private void btnFotos_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Imagenes|*.jpg; *.jpeg; *png";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFile.Title = "Seleccionar Imagen";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                nameFile = openFile.FileName;
            }
        }

        private bool validarEntradas()
        {
            return string.IsNullOrWhiteSpace(txtNombres.Text) ||
                    string.IsNullOrWhiteSpace(nameFile) ||
                    string.IsNullOrWhiteSpace(txtTitulo.Text) ||
                    string.IsNullOrWhiteSpace(txtPropuesta.Text) ||
                    string.IsNullOrWhiteSpace(txtAcercaDeMi.Text);
        }

        private Candidato recogerDatos()
        {
            Candidato candidato = new Candidato();
            candidato.NombresCompletos = txtNombres.Text;
            candidato.Titulo = txtTitulo.Text;
            candidato.AcercaDeMi = txtAcercaDeMi.Text;
            candidato.Propuesta = txtPropuesta.Text;
            candidato.NombreImagen = subirImagen.SaveImageToFile(nameFile, txtNombres.Text);
            return candidato;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!validarEntradas())
            {
                Candidato candidato = recogerDatos();

                int resultado = negocioCandidatos.post(candidato);

                if (resultado == 1)
                {
                    MessageBox.Show("Candidato registrado con éxito", "Enorabuena");
                    Login login = new Login();
                    Hide();
                    login.Show();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error", "Lo sentimos");
                }
            }
            else
            {
                MessageBox.Show("Campos vacíos", "Error");
            }
        }

        private void FormCandidato_FormClosing(object sender, FormClosingEventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
        }
    }
}
