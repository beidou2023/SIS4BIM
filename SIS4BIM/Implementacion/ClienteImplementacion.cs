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
    public class ClienteImplementacion : BaseImplementacion, ICliente
    {
        public Cliente Get(byte id)
        {
            Cliente t = null;
            this.query = @"SELECT id,nit,razonSocial,estado,fechaRegistro,
                            IFNULL(fechaActualizacion,CURRENT_TIMESTAMP()) 
                            AS fechaActualizacion, idUsuario 
                            FROM categoria WHERE id=@id;";
            MySqlCommand comando = CreateBasicCommand(this.query);
            comando.Parameters.AddWithValue("@id", id);
            try
            {
                DataTable table = ExecuteDataTableCommand(comando);
                if (table.Rows.Count > 0)
                {
                    t = new Cliente(
                        short.Parse(table.Rows[0][0].ToString()),
                        table.Rows[0][1].ToString(),
                        table.Rows[0][2].ToString(),
                        byte.Parse(table.Rows[0][3].ToString()),
                        DateTime.Parse(table.Rows[0][4].ToString()),
                        DateTime.Parse(table.Rows[0][5].ToString()),
                        byte.Parse(table.Rows[0][6].ToString())
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
            this.query = @"SELECT CONCAT(u.nombres,' ',u.primerApellido,' ',
                            IFNULL(u.segundoApellido,'')) AS 'Nombre Completo'  ,
                            c.nit AS 'NIT',c.fechaRegistro AS 'Fecha Registro'
                            FROM cliente c JOIN usuario u ON c.idUsuario=u.id WHERE c.estado=1;";
            MySqlCommand command = CreateBasicCommand(this.query);
            return ExecuteDataTableCommand(command);
        }

        public int Insert(Cliente t)
        {
            int n = 0;
            this.query = @"INSERT INTO cliente (nit,razonSocial,estado,fechaRegistro,idUsuario)
                            VALUES (@nit,@razonSocial,1,CURRENT_TIMESTAMP(),@idUsuario);";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@nit", t.Nit);
            comand.Parameters.AddWithValue("@razonSocial", t.RazonSocial);
            comand.Parameters.AddWithValue("@idUsuario", t.IdUsuario);
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

        public int Update(Cliente t)
        {
            int n = 0;
            this.query = @"UPDATE cliente 
                            SET nit=@nit, razonSocial=@razonSocial, 
                            fechaActualizacion=NOW(),idUsuario=@idUsuario
                            WHERE id=@id;";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@nit", t.Nit);
            comand.Parameters.AddWithValue("@razonSocial", t.RazonSocial);
            comand.Parameters.AddWithValue("@idUsuario", t.IdUsuario);
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

        public int Delete(Cliente t)
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
