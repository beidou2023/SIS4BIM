using MySql.Data.MySqlClient;
using SIS4BIM.Interface;
using SIS4BIM.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIS4BIM.Implementacion
{
    public class ProductoImplementacion : BaseImplementacion, IProducto
    {
        public Producto Get(byte id)
        {
            Producto t = null;
            this.query = @"SELECT id,nombre,precioBaseVenta,unidadMedida,saldo,
                            estado,fechaRegistro,IFNULL(fechaActualizacion,current_timestamp()) As fechaActualizacion,
                            idUsuario, idCategoria,idProveedor
                            FROM producto WHERE id=@id;";
            MySqlCommand comando = CreateBasicCommand(this.query);
            comando.Parameters.AddWithValue("@id", id);
            try
            {
                DataTable table = ExecuteDataTableCommand(comando);
                if (table.Rows.Count > 0)
                {
                    t = new Producto(
                        short.Parse(table.Rows[0][0].ToString()),
                        table.Rows[0][1].ToString(),
                        decimal.Parse(table.Rows[0][2].ToString()),
                        table.Rows[0][3].ToString(),
                        short.Parse(table.Rows[0][4].ToString()),
                        byte.Parse(table.Rows[0][5].ToString()),
                        DateTime.Parse(table.Rows[0][6].ToString()),
                        DateTime.Parse(table.Rows[0][7].ToString()),
                        byte.Parse(table.Rows[0][8].ToString()),
                        byte.Parse(table.Rows[0][9].ToString()),
                        short.Parse(table.Rows[0][10].ToString())
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
            this.query = @"SELECT p.nombre AS 'Producto', p.precioBaseVenta AS 'Precio Base Venta',
                            p.unidadMedida AS 'Unidad Medida', p.saldo AS 'Saldo', p.fechaRegistro AS 'Fecha Registro',
                            CONCAT(u.nombres,' ',u.primerApellido,' ',IFNULL(u.segundoApellido,'')) AS 'Encargado', 
                            c.nombre AS 'Categoria', pp.denominacion AS 'Proveedor' FROM producto p 
                            JOIN categoria c ON p.idCategoria=c.id JOIN proveedor pp ON p.idProveedor=pp.id 
                            JOIN usuario u ON p.idUsuario=u.id WHERE p.estado=1;";
            MySqlCommand command = CreateBasicCommand(this.query);
            return ExecuteDataTableCommand(command);
        }

        public int Insert(Producto t)
        {
            int n = 0;
            this.query = @"INSERT INTO producto (nombre,precioBaseVenta,unidadMedida,
                            saldo,estado,fechaRegistro,idUsuario,idCategoria,idProveedor)
                            VALUES (@nombre,@precioBaseVenta,@unidadMedida,@saldo,1,
                            CURRENT_TIMESTAMP(),@idUsuario,@idCategoria,@idProveedor);";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@nombre", t.Nombre);
            comand.Parameters.AddWithValue("@precioBaseVenta", t.PrecioBaseVenta);
            comand.Parameters.AddWithValue("@unidadMedida", t.UnidadMedida);
            comand.Parameters.AddWithValue("@saldo", t.Saldo);
            comand.Parameters.AddWithValue("@idUsuario", t.IdUsuario);
            comand.Parameters.AddWithValue("@idCategoria", t.IdCategoria);
            comand.Parameters.AddWithValue("@idProveedor", t.IdProveedor);
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

        public int Update(Producto t)
        {
            int n = 0;
            this.query = @"UPDATE producto 
                            SET nombre=@nombre, precioBaseVenta=@precioBaseVenta,
                            unidadMedida=@unidadMedida,saldo=@saldo,fechaActualizacion=NOW(),
                            idUsuario=@idUsuario,idCategoria=@idCategoria,idProveedor=@idProveedor
                            WHERE id=@id;";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@nombre", t.Nombre);
            comand.Parameters.AddWithValue("@precioBaseVenta", t.PrecioBaseVenta);
            comand.Parameters.AddWithValue("@unidadMedida", t.UnidadMedida);
            comand.Parameters.AddWithValue("@saldo", t.Saldo);
            comand.Parameters.AddWithValue("@idUsuario", t.IdUsuario);
            comand.Parameters.AddWithValue("@idCategoria", t.IdCategoria);
            comand.Parameters.AddWithValue("@idProveedor", t.IdProveedor);
            comand.Parameters.AddWithValue("@id", t.Id);

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

        public int Delete(Producto t)
        {
            int n = 0;
            this.query = @"UPDATE usuario 
                            SET estado=0, fechaActualizacion=NOW()
                            WHERE id=@id;";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@id", t.Id);
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

        public DataTable SelectFiltro(DateTime desde, DateTime hasta) {
            this.query = @" SELECT nombre AS 'Producto',precioBaseVenta AS 'Precio unidad', saldo AS 'Saldo', fechaRegistro AS 'Fecha de Registro'
                            FROM producto WHERE fechaRegistro BETWEEN @desde AND @hasta AND estado=1 ORDER BY 4;";
            MySqlCommand command = CreateBasicCommand(this.query);
            command.Parameters.AddWithValue("@desde", desde);
            command.Parameters.AddWithValue("@hasta", hasta);
            return ExecuteDataTableCommand(command);
        }
        public DataTable SelectFiltroCategoria(string querry) {
            this.query = @" SELECT p.nombre AS 'Producto',p.precioBaseVenta AS 'Precio unidad', p.saldo AS 'Saldo', 
                            p.fechaRegistro AS 'Fecha de Registro'
                            FROM producto p JOIN categoria c ON p.idCategoria=c.id WHERE  c.nombre=@querry;";
            MySqlCommand command = CreateBasicCommand(this.query);
            command.Parameters.AddWithValue("@querry", querry);
            return ExecuteDataTableCommand(command);
        }
        public DataTable SelectFiltroDepartamento(string querry) {
            this.query = @" SELECT p.nombre AS 'Producto',p.precioBaseVenta AS 'Precio unidad', 
                            p.saldo AS 'Saldo', p.fechaRegistro AS 'Fecha de Registro'
                            FROM producto p JOIN proveedor c ON p.idProveedor=c.id JOIN departamento d ON c.idDepartamento=d.id 
                            WHERE  d.nombre=@querry;";
            MySqlCommand command = CreateBasicCommand(this.query);
            command.Parameters.AddWithValue("@querry", querry);
            return ExecuteDataTableCommand(command);
        }
    }
}
