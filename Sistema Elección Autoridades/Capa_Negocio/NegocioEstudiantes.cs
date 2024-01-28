using Capa_Datos;
using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class NegocioEstudiantes
    {

        private readonly DatosEstudiantes datosEstudiantes;

        public NegocioEstudiantes()
        {
            datosEstudiantes = new DatosEstudiantes();
        }

        public int post(Estudiante estudiante)
        {
            return datosEstudiantes.Insert(estudiante);
        }

    }
}
