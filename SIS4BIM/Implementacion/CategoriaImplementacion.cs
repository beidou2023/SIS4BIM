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


    public class CategoriaImplementacion : BaseImplementacion, ICategoria
    {
        public Categoria Get(byte id)
        {
            Categoria t = null;
            this.query = @" SELECT id,nombre,descripcion,estado,fechaRegistro,
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
                    t = new Categoria(
                        byte.Parse(table.Rows[0][0].ToString()),
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
            this.query = @"SELECT nombre AS 'Producto',descripcion AS 'Descripcion',
                            fechaRegistro AS 'Fecha Registro'
                            FROM categoria WHERE estado=1 ORDER BY 2;";
            MySqlCommand command = CreateBasicCommand(this.query);
            return ExecuteDataTableCommand(command);
        }

        public int Insert(Categoria t)
        {
            int n = 0;
            this.query = @"INSERT INTO categoria (nombre,descripcion,estado,fechaRegistro,idUsuario)
                            VALUES (@nombre,@descripcion,1,CURRENT_TIMESTAMP(),@idUsuario);";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@nombre", t.Nombre);
            comand.Parameters.AddWithValue("@nombre", t.Descripcion);
            comand.Parameters.AddWithValue("@nombre", t.IdUsuario);
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

        public int Update(Categoria t)
        {
            int n = 0;
            this.query = @"UPDATE categoria 
                            SET nombre=@nombre, descripcion=@descripcion, 
                            fechaActualizacion=NOW(),idUsuario=@idUsuario
                            WHERE id=@id;";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@nombre", t.Nombre);
            comand.Parameters.AddWithValue("@descripcion", t.Descripcion);
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

        public int Delete(Categoria t)
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
