using SIS4BIM.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Interface
{
    public interface IProducto : IBase<Producto>
    {
        Producto Get(byte id);
        DataTable SelectFiltro(DateTime desde,DateTime hasta);
        DataTable SelectFiltroCategoria(string querry);
        DataTable SelectFiltroDepartamento(string querry);
    }
}
