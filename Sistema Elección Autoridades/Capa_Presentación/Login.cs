using Capa_Negocio;
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
    public partial class Login : Form
    {

        private readonly NegocioConsultas negocioConsultas;

        public Login()
        {
            InitializeComponent();
            negocioConsultas = new NegocioConsultas();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormUsuario formUsuario = new FormUsuario();    
            Hide();
            formUsuario.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!CamposIncompletos())
            {
                string username = txtUsuario.Text;
                string password = txtClave.Text;

                if (username == "admin" && password == "root")
                {
                    FormCandidato candidato = new FormCandidato();
                    Hide();
                    candidato.ShowDialog();
                    return;
                }
                int id = negocioConsultas.ValidarCredenciales(username, password);
                if (id != 0)
                {
                    Home home = new Home(id);
                    Hide();
                    home.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Credenciales invalidas", "Error");
                }

            }else
            {
                MessageBox.Show("Campos vacíos", "Error");
            }
        }

        private bool CamposIncompletos()
        {
            return string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                   string.IsNullOrWhiteSpace(txtClave.Text);
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
