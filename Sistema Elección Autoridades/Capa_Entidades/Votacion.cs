using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class Votacion
    {
        public int Id { get; set; }

        public Estudiante Estudiante { get; set; }

        public Candidato Candidato { get; set; }
    }
}
