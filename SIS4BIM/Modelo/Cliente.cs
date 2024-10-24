using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Modelo
{
    public class Cliente : BaseModelo
    {
        #region Atributos
        public short Id { get; set; }
        public string Nit { get; set; }
        public string RazonSocial { get; set; }
        public byte IdUsuario { get; set; }
        #endregion

        #region Constructores
        public Cliente()
        {
        }

        public Cliente(short id, string nit, string razonSocial, byte estado, DateTime fechaRegistro, DateTime fechaActualizacion, byte idUsuario)
            : base(estado, fechaRegistro, fechaActualizacion)
        {
            Id = id;
            Nit = nit;
            RazonSocial = razonSocial;
            IdUsuario = idUsuario;
        }
        #endregion

    }
}
