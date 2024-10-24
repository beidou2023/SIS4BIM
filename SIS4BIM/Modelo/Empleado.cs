using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Modelo
{
    public class Empleado : BaseModelo
    {
        #region Atributos
        public byte Id { get; set; }
        public string Ci { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public char Sexo { get; set; }
        public byte IdUsuario { get; set; }
        #endregion

        #region Constructores
        public Empleado()
        {
        }

        public Empleado(byte id, string ci, string nombres, string primerApellido, string segundoApellido, DateTime fechaNacimiento, char sexo, byte estado, DateTime fechaRegistro, DateTime fechaActualizacion, byte idUsuario)
            : base(estado, fechaRegistro, fechaActualizacion)
        {
            Id = id;
            Ci = ci;
            Nombres = nombres;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
            FechaNacimiento = fechaNacimiento;
            Sexo = sexo;
            IdUsuario = idUsuario;
        }
        #endregion

    }
}
