using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Modelo
{
    public class Venta : BaseModelo
    {
        #region Atributos
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public byte IdUsuario { get; set; }
        public short IdCliente { get; set; }
        #endregion

        #region Constructores
        public Venta()
        {
        }

        public Venta(int id, decimal total, DateTime fecha, byte estado, DateTime fechaRegistro, DateTime fechaActualizacion, byte idUsuario, short idCliente)
            : base(estado, fechaRegistro, fechaActualizacion)
        {
            Id = id;
            Total = total;
            Fecha = fecha;
            IdUsuario = idUsuario;
            IdCliente = idCliente;
        }
        #endregion

    }
}
