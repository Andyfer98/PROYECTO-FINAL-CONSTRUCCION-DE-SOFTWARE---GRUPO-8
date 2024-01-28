using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;

namespace Capa_Datos
{
    public class DatosEstudiantes
    {

        private readonly string connectionString;

        public DatosEstudiantes()
        {
            connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        }

        public int Insert(Estudiante estudiante)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("spInsertarEstudiante", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombresCompletos", estudiante.NombresCompletos);
                    cmd.Parameters.AddWithValue("@semestreId", estudiante.Semestre.Id);
                    cmd.Parameters.AddWithValue("@correoInstitucional", estudiante.CorreoInstitucional);
                    cmd.Parameters.AddWithValue("@usuario", estudiante.Usuario);
                    cmd.Parameters.AddWithValue("@clave", estudiante.Clave);

                    SqlParameter resultadoParam = new SqlParameter("@resultado", SqlDbType.Int);
                    resultadoParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultadoParam);

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(resultadoParam.Value);
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
