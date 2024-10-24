using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Modelo
{
    public class BaseModelo
    {
        public byte Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActualizacion { get; set; }

        public BaseModelo()
        {

        }
        public BaseModelo(byte estado, DateTime fechaRegistro, DateTime fechaActualizacion)
        {
            Estado = estado;
            FechaRegistro = fechaRegistro;
            FechaActualizacion = fechaActualizacion;
        }
    }
}
