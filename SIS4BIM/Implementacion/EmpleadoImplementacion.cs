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
    public class EmpleadoImplementacion : BaseImplementacion, IEmpleado
    {
        public Empleado Get(byte id)
        {
            Empleado t = null;
            this.query = @"SELECT id,ci,nombres,primerApellido,segundoApellido,fechaNacimiento,
                            sexo,estado,fechaRegistro,IFNULL(fechaActualizacion,current_timestamp()) As fechaActualizacion,idUsuario
                            FROM empleado WHERE id=@id;";
            MySqlCommand comando = CreateBasicCommand(this.query);
            comando.Parameters.AddWithValue("@id", id);
            try
            {
                DataTable table = ExecuteDataTableCommand(comando);
                if (table.Rows.Count > 0)
                {
                    t = new Empleado(
                        byte.Parse(table.Rows[0][0].ToString()),
                        table.Rows[0][1].ToString(),
                        table.Rows[0][2].ToString(),
                        table.Rows[0][3].ToString(),
                        table.Rows[0][4].ToString(),
                        DateTime.Parse(table.Rows[0][5].ToString()),
                        char.Parse(table.Rows[0][6].ToString()),
                        byte.Parse(table.Rows[0][7].ToString()),
                        DateTime.Parse(table.Rows[0][8].ToString()),
                        DateTime.Parse(table.Rows[0][9].ToString()),
                        byte.Parse(table.Rows[0][10].ToString())
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
            this.query = @"SELECT e.ci AS 'CI', concat(e.nombres,' ',e.primerApellido,' ',IFNULL(e.segundoApellido,'')) 
                            AS 'Nombre Completo',e.sexo AS 'Sexo',e.fechaRegistro AS 'Fecha Registro' 
                            FROM empleado e WHERE estado=1;";
            MySqlCommand command = CreateBasicCommand(this.query);
            return ExecuteDataTableCommand(command);
        }

        public int Insert(Empleado t)
        {
            int n = 0;
            this.query = @"INSERT INTO empelado (ci,nombres,primerApellido,segundoApellido,
                            fechaNacimiento,sexo,estado,fechaRegistro,idUsuario)
                            VALUES (@ci,@nombres,@primerApellido,@segundoApellido,@fechaNacimiento,
                            @sexo,1,CURRENT_TIMESTAMP(),@idUsuario);";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@ci", t.Ci);
            comand.Parameters.AddWithValue("@nombres", t.Nombres);
            comand.Parameters.AddWithValue("@primerApellido", t.PrimerApellido);
            comand.Parameters.AddWithValue("@segundoApellido", t.SegundoApellido);
            comand.Parameters.AddWithValue("@fechaNacimiento", t.FechaNacimiento);
            comand.Parameters.AddWithValue("@sexo", t.Sexo);
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

        public int Update(Empleado t)
        {
            int n = 0;
            this.query = @"UPDATE empleado 
                            SET ci=@ci, nombres=@nombres,
                            primerApellido=@primerApellido,segundoApellido=@segundoApellido,
                            fechaNacimiento=@fechaNacimiento,sexo=@sexo,fechaActualizacion=NOW(),
                            idUsuario=@idUsuario
                            WHERE id=@id;";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@ci", t.Ci);
            comand.Parameters.AddWithValue("@nombres", t.Nombres);
            comand.Parameters.AddWithValue("@primerApellido", t.PrimerApellido);
            comand.Parameters.AddWithValue("@segundoApellido", t.SegundoApellido);
            comand.Parameters.AddWithValue("@fechaNacimiento", t.FechaNacimiento);
            comand.Parameters.AddWithValue("@sexo", t.Sexo);
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

        public int Delete(Empleado t)
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
