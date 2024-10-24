using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Modelo
{
    public class Detalle
    {
        #region Atributos
        public int IdVenta { get; set; }
        public short IdProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public short Cantidad { get; set; }
        #endregion

        #region Constructores
        public Detalle()
        {
        }

        public Detalle(int idVenta, short idProducto, decimal precioUnitario, short cantidad)
        {
            IdVenta = idVenta;
            IdProducto = idProducto;
            PrecioUnitario = precioUnitario;
            Cantidad = cantidad;
        }
        #endregion

    }
}
