using MySql.Data.MySqlClient;
using SIS4BIM.Interface;
using SIS4BIM.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Implementacion
{
    public class DetalleImplementacion : BaseImplementacion, IDetalle
    {
        public Detalle Get(byte id)
        {
            Detalle t = null;
            this.query = @"SELECT idVenta,idProducto,precioUnitario,cantidad
                            FROM detalle WHERE idVenta=@id;";
            MySqlCommand comando = CreateBasicCommand(this.query);
            comando.Parameters.AddWithValue("@id", id);
            try
            {
                DataTable table = ExecuteDataTableCommand(comando);
                if (table.Rows.Count > 0)
                {
                    t = new Detalle(
                        int.Parse(table.Rows[0][0].ToString()),
                        short.Parse(table.Rows[0][1].ToString()),
                        decimal.Parse(table.Rows[0][2].ToString()),
                        short.Parse(table.Rows[0][3].ToString())
                    );
                }
                return t;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable Select()
        {
            this.query = @"SELECT d.idVenta AS 'ID VENTA', p.nombre AS 'Producto', 
                            d.precioUnitario AS 'Precio Unitario', d.cantidad AS Cantidad
                            FROM detalle d JOIN producto p ON d.idProducto=p.id;";
            MySqlCommand command = CreateBasicCommand(this.query);
            return ExecuteDataTableCommand(command);
        }

        public int Insert(Detalle t)
        {
            int n = 0;
            this.query = @"INSERT INTO detalle (idVenta,idProducto,precioUnitario,cantidad)
                            VALUES (@idVenta,@idProducto,@precioUnitario,@cantidad);";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@idVenta", t.IdVenta);
            comand.Parameters.AddWithValue("@idProducto", t.IdProducto);
            comand.Parameters.AddWithValue("@precioUnitario", t.PrecioUnitario);
            comand.Parameters.AddWithValue("@cantidad", t.Cantidad);
            try
            {
                n = ExecuteBasic(comand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return n;
        }

        public int Update(Detalle t)
        {
            int n = 0;
            this.query = @"UPDATE detalle 
                            SET idVenta=@idVenta, idProducto=@idProducto,
                            precioUnitario=@precioUnitario,cantidad=@cantidad
                            WHERE idVenta=@idVenta;";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@idVenta", t.IdVenta);
            comand.Parameters.AddWithValue("@idProducto", t.IdProducto);
            comand.Parameters.AddWithValue("@precioUnitario", t.PrecioUnitario);
            comand.Parameters.AddWithValue("@cantidad", t.Cantidad);
            comand.Parameters.AddWithValue("@idVenta", t.IdVenta);

            try
            {
                n = ExecuteBasic(comand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return n;
        }

        public int Delete(Detalle t)
        {
            int n = 0;
            this.query = @"";
            MySqlCommand comand = CreateBasicCommand(this.query);
            //comand.Parameters.AddWithValue("@id", t.Id);
            try
            {
                n = ExecuteBasic(comand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return n;
        }
    }
}
