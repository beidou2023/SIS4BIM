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
    public class VentaImplementacion : BaseImplementacion, IVenta
    {
        public Venta Get(byte id)
        {
            Venta t = null;
            this.query = @"SELECT id,total,fecha,estado,fechaRegistro,
                            IFNULL(fechaActualizacion,current_timestamp()) 
                            As fechaActualizacion,idUsuario, idCliente
                            FROM venta WHERE id=@id;";
            MySqlCommand comando = CreateBasicCommand(this.query);
            comando.Parameters.AddWithValue("@id", id);
            try
            {
                DataTable table = ExecuteDataTableCommand(comando);
                if (table.Rows.Count > 0)
                {
                    t = new Venta(
                        int.Parse(table.Rows[0][0].ToString()),
                        decimal.Parse(table.Rows[0][1].ToString()),
                        DateTime.Parse(table.Rows[0][2].ToString()),
                        byte.Parse(table.Rows[0][3].ToString()),
                        DateTime.Parse(table.Rows[0][4].ToString()),
                        DateTime.Parse(table.Rows[0][5].ToString()),
                        byte.Parse(table.Rows[0][6].ToString()),
                        short.Parse(table.Rows[0][7].ToString())
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
            this.query = @"SELECT v.total AS 'Total', v.fecha AS 'Fecha', 
                            v.fechaRegistro AS 'Fecha Registro', CONCAT(u.nombres,' ',u.primerApellido,' ', 
                            IFNULL(u.segundoApellido,'')) AS 'Encargado',
                            v.idCliente AS 'ID Cliente' FROM venta v 
                            JOIN usuario u ON v.idUsuario=u.id WHERE estado=1;";
            MySqlCommand command = CreateBasicCommand(this.query);
            return ExecuteDataTableCommand(command);
        }

        public int Insert(Venta t)
        {
            int n = 0;
            this.query = @"INSERT INTO venta (total,fecha,estado,fechaRegistro,idUsuario,idCliente)
                            VALUES (@total,@fecha,1,CURRENT_TIMESTAMP(),@idUsuario,@idCliente);";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@total", t.Total);
            comand.Parameters.AddWithValue("@fecha", t.Fecha);
            comand.Parameters.AddWithValue("@idUsuario", t.IdUsuario);
            comand.Parameters.AddWithValue("@idCliente", t.IdCliente);
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

        public int Update(Venta t)
        {
            int n = 0;
            this.query = @"UPDATE venta 
                            SET total=@total, fecha=@fecha,
                            fechaActualizacion=NOW(),
                            idUsuario=@idUsuario,idCliente=@idCliente
                            WHERE id=@id;";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@total", t.Total);
            comand.Parameters.AddWithValue("@fecha", t.Fecha);
            comand.Parameters.AddWithValue("@idUsuario", t.IdUsuario);
            comand.Parameters.AddWithValue("@idCliente", t.IdCliente);
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

        public int Delete(Venta t)
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
    }
}
