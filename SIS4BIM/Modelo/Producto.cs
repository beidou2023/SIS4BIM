using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Modelo
{
    public class Producto : BaseModelo
    {
        #region Atributos
        public short Id { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioBaseVenta { get; set; }
        public string UnidadMedida { get; set; }
        public short Saldo { get; set; }
        public byte IdUsuario { get; set; }
        public byte IdCategoria { get; set; }
        public short IdProveedor { get; set; }
        #endregion

        #region Constructores
        public Producto()
        {
        }

        public Producto(short id, string nombre, decimal precioBaseVenta, string unidadMedida, short saldo, byte estado, DateTime fechaRegistro, DateTime fechaActualizacion, byte idUsuario, byte idCategoria, short idProveedor)
            : base(estado, fechaRegistro, fechaActualizacion)
        {
            Id = id;
            Nombre = nombre;
            PrecioBaseVenta = precioBaseVenta;
            UnidadMedida = unidadMedida;
            Saldo = saldo;
            IdUsuario = idUsuario;
            IdCategoria = idCategoria;
            IdProveedor = idProveedor;
        }
        #endregion

    }
}
