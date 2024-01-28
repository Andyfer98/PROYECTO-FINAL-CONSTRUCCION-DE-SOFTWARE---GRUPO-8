using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Capa_Entidades;

namespace Capa_Datos
{
    public class DatosConsultas
    {
        private readonly string connectionString;

        public DatosConsultas()
        {
            connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        }

        public int Login(string usuario, string clave)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("spVerificarCredenciales", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@clave", clave);

                    resultado = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en el login: {ex.Message}");
                }
            }

            return resultado;
        }

        public List<Semestre> GetSemestres()
        {
            List<Semestre> semestres = new List<Semestre>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("spRealizarConsulta", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tipoConsulta", "consultar_semestres");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Semestre semestre = new Semestre
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nivel = reader["nivel"].ToString()
                            };

                            semestres.Add(semestre);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener ciudades: {ex.Message}");
                }
            }

            return semestres;
        }

        public int Validar(string parametro)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("spRealizarConsulta", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@parametro", parametro);
                    cmd.Parameters.AddWithValue("@tipoConsulta", "consultar_semestres");

                    resultado = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en el login: {ex.Message}");
                }
            }

            return resultado;
        }

    }
}


