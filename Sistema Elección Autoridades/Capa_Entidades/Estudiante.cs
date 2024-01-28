using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class Estudiante
    {
        public int Id { get; set; }

        public string NombresCompletos { get; set; }

        public Semestre Semestre { get; set; }

        public string CorreoInstitucional { get; set; }

        public string Usuario { get; set; }

        public string Clave { get; set; }

    }
}
