using Capa_Entidades;
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
    public partial class FormUsuario : Form
    {

        private readonly NegocioEstudiantes negocioEstudiantes;
        private readonly NegocioConsultas negocioConsultas;

        public FormUsuario()
        {
            InitializeComponent();
            negocioEstudiantes = new NegocioEstudiantes();
            negocioConsultas = new NegocioConsultas();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (validarEntradas())
            {
                Estudiante estudiante = recogerDatos();

                int resultado = negocioEstudiantes.post(estudiante);
                
                if (resultado == 1)
                {
                    MessageBox.Show("Ingresado con éxito", "Enorabuena");
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

        private bool validarEntradas()
        {
            return !CamposIncompletos() && SemestreSeleccionada();
        }

        private bool CamposIncompletos()
        {
            return  string.IsNullOrWhiteSpace(txtNombres.Text) ||
                    string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                    string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                    string.IsNullOrWhiteSpace(txtClave.Text);
        }

        private bool SemestreSeleccionada()
        {
            return cbSemestre.SelectedIndex != -1;
        }

        private Estudiante recogerDatos()
        {
            Estudiante estudiante = new Estudiante();
            estudiante.NombresCompletos = txtNombres.Text;
            estudiante.CorreoInstitucional = txtCorreo.Text;
            estudiante.Usuario = txtUsuario.Text;
            estudiante.Clave = txtClave.Text;
            if (cbSemestre.SelectedItem != null && cbSemestre.SelectedItem is Semestre)
            {
                estudiante.Semestre = (Semestre)cbSemestre.SelectedItem;
            }
            return estudiante;
        }


        private void FormUsuario_Load(object sender, EventArgs e)
        {
            LlenarCombo();
        }

        private void LlenarCombo()
        {
            List<Semestre> semestres = negocioConsultas.get();
            cbSemestre.DataSource = semestres;
            cbSemestre.DisplayMember = "Nivel";
            cbSemestre.ValueMember = "Id";
            cbSemestre.SelectedIndex = -1;
            cbSemestre.Text = "Seleccione un semestre";
        }

        private void FormUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
        }
    }
}
