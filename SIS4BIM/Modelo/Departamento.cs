using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Modelo
{
    public class Departamento
    {
        #region Atributos
        public byte Id { get; set; }
        public string Nombre { get; set; }
        #endregion

        #region Constructores
        public Departamento()
        {
        }

        public Departamento(byte id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        #endregion

    }
}
