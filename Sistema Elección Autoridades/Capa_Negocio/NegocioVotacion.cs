using Capa_Datos;
using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class NegocioVotacion
    {
        private readonly DatosVotacion datosVotacion;

        public NegocioVotacion()
        {
            datosVotacion = new DatosVotacion();
        }

        public int votar(Votacion votacion)
        {
            return datosVotacion.Insert(votacion);
        }

        public string ObtenerCandidatosMasVotados()
        {
            return datosVotacion.ObtenerCandidatosMasVotados();
        }
    }
}
