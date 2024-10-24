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

    public class DepartamentoImplementacion : BaseImplementacion, IDepartamento
    {
        public Departamento Get(byte id)
        {
            Departamento t = null;
            this.query = @"SELECT id,nombre 
                            FROM categoria WHERE id=@id;";
            MySqlCommand comando = CreateBasicCommand(this.query);
            comando.Parameters.AddWithValue("@id", id);
            try
            {
                DataTable table = ExecuteDataTableCommand(comando);
                if (table.Rows.Count > 0)
                {
                    t = new Departamento(
                        byte.Parse(table.Rows[0][0].ToString()),
                        table.Rows[0][1].ToString()
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
            this.query = @"SELECT nombre FROM departamento;";
            MySqlCommand command = CreateBasicCommand(this.query);
            return ExecuteDataTableCommand(command);
        }

        public int Insert(Departamento t)
        {
            int n = 0;
            this.query = @"";
            MySqlCommand comand = CreateBasicCommand(this.query);
            //comand.Parameters.AddWithValue("@ci", t.Ci);
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

        public int Update(Departamento t)
        {
            int n = 0;
            this.query = @"";
            MySqlCommand comand = CreateBasicCommand(this.query);
            //comand.Parameters.AddWithValue("@ci", t.Ci);
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

        public int Delete(Departamento t)
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
