using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Modelo
{
    public class Proveedor : BaseModelo
    {
        #region Atributos
        public short Id { get; set; }
        public string Denominacion { get; set; }
        public string Direccion { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }
        public byte IdUsuario { get; set; }
        public byte IdDepartamento { get; set; }
        #endregion

        #region Constructores
        public Proveedor()
        {
        }

        public Proveedor(short id, string denominacion, string direccion, float latitud, float longitud, byte estado, DateTime fechaRegistro, DateTime fechaActualizacion, byte idUsuario, byte idDepartamento)
            : base(estado, fechaRegistro, fechaActualizacion)
        {
            Id = id;
            Denominacion = denominacion;
            Direccion = direccion;
            Latitud = latitud;
            Longitud = longitud;
            IdUsuario = idUsuario;
            IdDepartamento = idDepartamento;
        }
        #endregion

    }
}
