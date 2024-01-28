using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Capa_Datos
{
    public class DatosVotacion
    {
        private readonly string connectionString;

        public DatosVotacion()
        {
            connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        }

        public int Insert(Votacion votacion)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("spInsertarVotacion", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@estudianteId", votacion.Estudiante.Id);
                    cmd.Parameters.AddWithValue("@candidatoId", votacion.Candidato.Id);

                    SqlParameter resultadoParam = new SqlParameter("@resultado", SqlDbType.Int);
                    resultadoParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultadoParam);

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(resultadoParam.Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al votar: {ex.Message}");
                }
            }

            return resultado;
        }

        public string ObtenerCandidatosMasVotados()
        {
            string resultadoString = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("spObtenerCandidatosMasVotados", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            resultadoString = reader["Resultado"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener candidatos más votados: {ex.Message}");
                }
            }

            return resultadoString;
        }
    }
}
