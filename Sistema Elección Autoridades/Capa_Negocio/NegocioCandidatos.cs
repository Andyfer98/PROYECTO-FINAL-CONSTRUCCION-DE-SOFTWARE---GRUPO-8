using Capa_Datos;
using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class NegocioCandidatos
    { 

        private readonly DatosCandidatos datosCandidatos;

        public NegocioCandidatos()
        {
            datosCandidatos = new DatosCandidatos();
        }

        public int post(Candidato candidato)
        {
            return datosCandidatos.Insert(candidato);
        }

        public List<Candidato> get()
        {
            return datosCandidatos.GetCandidatas();
        }

    }
}
