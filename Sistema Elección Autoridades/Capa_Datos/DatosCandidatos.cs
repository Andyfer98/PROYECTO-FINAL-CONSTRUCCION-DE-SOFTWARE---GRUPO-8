using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class DatosCandidatos
    {
        private readonly string connectionString;

        public DatosCandidatos()
        {
            connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        }

        public int Insert(Candidato candidato)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("spInsertarCandidato", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombresCompletos", candidato.NombresCompletos);
                    cmd.Parameters.AddWithValue("@nombreImagen", candidato.NombreImagen);
                    cmd.Parameters.AddWithValue("@Titulo", candidato.Titulo);
                    cmd.Parameters.AddWithValue("@AcercaDeMi", candidato.AcercaDeMi);
                    cmd.Parameters.AddWithValue("@Propuesta", candidato.Propuesta);

                    SqlParameter resultadoParam = new SqlParameter("@resultado", SqlDbType.Int);
                    resultadoParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultadoParam);

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(resultadoParam.Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al registrar: {ex.Message}");
                }
            }

            return resultado;
        }

        public List<Candidato> GetCandidatas()
        {
            List<Candidato> candidatos = new List<Candidato>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("spRealizarConsulta", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tipoConsulta", "consultar_candidatas");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Candidato candidato = new Candidato
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                NombresCompletos = reader["nombresCompletos"].ToString(),
                                NombreImagen = reader["nombreImagen"].ToString(),
                                Titulo = reader["titulo"].ToString(),
                                AcercaDeMi = reader["acercaDeMi"].ToString(),
                                Propuesta = reader["propuesta"].ToString()
                            };

                            candidatos.Add(candidato);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener ciudades: {ex.Message}");
                }
            }

            return candidatos;
        }
    }
}
