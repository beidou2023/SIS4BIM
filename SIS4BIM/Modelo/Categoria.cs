using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Modelo
{
    public class Categoria : BaseModelo
    {
        #region Atributos
        public byte Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte IdUsuario { get; set; }
        #endregion

        #region Constructores
        public Categoria()
        {
        }

        public Categoria(byte id, string nombre, string descripcion, byte estado, DateTime fechaRegistro, DateTime fechaActualizacion, byte idUsuario)
            : base(estado, fechaRegistro, fechaActualizacion)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            IdUsuario = idUsuario;
        }
        #endregion

    }
}
