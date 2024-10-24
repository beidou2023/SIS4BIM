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
    public class ProveedorImplementacion : BaseImplementacion, IProveedor
    {
        public Proveedor Get(byte id)
        {
            Proveedor t = null;
            this.query = @"SELECT id,denominacion,direccion,latitud,longitud,
                            estado,fechaRegistro,IFNULL(fechaActualizacion,current_timestamp()) 
                            As fechaActualizacion,idUsuario, idDepartamento
                            FROM proveedor WHERE id=@id;";
            MySqlCommand comando = CreateBasicCommand(this.query);
            comando.Parameters.AddWithValue("@id", id);
            try
            {
                DataTable table = ExecuteDataTableCommand(comando);
                if (table.Rows.Count > 0)
                {
                    t = new Proveedor(
                        short.Parse(table.Rows[0][0].ToString()),
                        table.Rows[0][1].ToString(),
                        table.Rows[0][2].ToString(),
                        float.Parse(table.Rows[0][3].ToString()),
                        float.Parse(table.Rows[0][4].ToString()),
                        byte.Parse(table.Rows[0][5].ToString()),
                        DateTime.Parse(table.Rows[0][6].ToString()),
                        DateTime.Parse(table.Rows[0][7].ToString()),
                        byte.Parse(table.Rows[0][8].ToString()),
                        byte.Parse(table.Rows[0][9].ToString())
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
            this.query = @"SELECT p.denominacion as 'Proveedor', p.direccion AS 'Direccion',
                            p.latitud AS 'Latitud', p.longitud AS 'Longitud',p.fechaRegistro AS 'Fecha Registro',
                            CONCAT(u.nombres,' ',u.primerApellido,' ',
                            IFNULL(u.segundoApellido,'')) AS 'Encargado',d.nombre AS 'Departamento'
                            FROM proveedor p JOIN usuario u ON p.idUsuario=u.id 
                            JOIN departamento d ON p.idDepartamento=d.id
                            WHERE p.estado=1;";
            MySqlCommand command = CreateBasicCommand(this.query);
            return ExecuteDataTableCommand(command);
        }

        public int Insert(Proveedor t)
        {
            int n = 0;
            this.query = @"INSERT INTO proveedor (denominacion,direccion,latitud,
                            longitud,estado,fechaRegistro,idUsuario,idDepartamento)
                            VALUES (@denominacion,@direccion,@latitud,@longitud,1,
                            CURRENT_TIMESTAMP(),@idUsuario,@idDepartamento);";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@denominacion", t.Denominacion);
            comand.Parameters.AddWithValue("@direccion", t.Direccion);
            comand.Parameters.AddWithValue("@latitud", t.Latitud);
            comand.Parameters.AddWithValue("@longitud", t.Longitud);
            comand.Parameters.AddWithValue("@idUsuario", t.IdUsuario);
            comand.Parameters.AddWithValue("@idDepartamento", t.IdDepartamento);
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

        public int Update(Proveedor t)
        {
            int n = 0;
            this.query = @"UPDATE proveedor 
                            SET denominacion=@denominacion, direccion=@direccion,
                            latitud=@latitud,longitud=@longitud,fechaActualizacion=NOW(),
                            idUsuario=@idUsuario,idDepartamento=@idDepartamento
                            WHERE id=@id;";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@denominacion", t.Denominacion);
            comand.Parameters.AddWithValue("@direccion", t.Direccion);
            comand.Parameters.AddWithValue("@latitud", t.Latitud);
            comand.Parameters.AddWithValue("@longitud", t.Longitud);
            comand.Parameters.AddWithValue("@idUsuario", t.IdUsuario);
            comand.Parameters.AddWithValue("@idDepartamento", t.IdDepartamento);
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

        public int Delete(Proveedor t)
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
