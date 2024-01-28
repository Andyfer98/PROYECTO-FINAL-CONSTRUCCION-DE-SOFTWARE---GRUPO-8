using Capa_Datos;
using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class NegocioConsultas
    {

        private readonly DatosConsultas datosConsultas;

        public NegocioConsultas()
        {
            datosConsultas = new DatosConsultas();
        }

        public int ValidarCredenciales(string usuario, string clave)
        {
            return datosConsultas.Login(usuario, clave);
        }

        public List<Semestre> get()
        {
            return datosConsultas.GetSemestres();
        }

        public int ValidarAtributo(string parametro)
        {
            return datosConsultas.Validar(parametro);
        }

    }
}
