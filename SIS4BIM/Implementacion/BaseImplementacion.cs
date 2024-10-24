using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SIS4BIM.Implementacion
{
    public class BaseImplementacion
    {
        string connectionString = "Server=localhost;Database=bdincos2023;Uid=root;Pwd=;";
        public string query;

        public MySqlCommand CreateBasicCommand()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand comand = new MySqlCommand();
            comand.Connection = connection;
            return comand;
        }

        public MySqlCommand CreateBasicCommand(string query)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand comand = new MySqlCommand(query, connection);
            return comand;
        }

        /// <summary>
        /// INSERT, UPDATE or DELETE
        /// </summary>
        /// <param name="comand">Comando asiciado a una conexion y con su SQL query</param>
        /// <returns></returns>
        public int ExecuteBasic(MySqlCommand comand)
        {
            int n = 0;
            try
            {
                comand.Connection.Open();
                n = comand.ExecuteNonQuery();
                return n;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                comand.Connection.Close();
            }
        }

        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="comand">Comando asociado a una conexion con SQL query</param>
        /// <returns></returns>
        public DataTable ExecuteDataTableCommand(MySqlCommand comand)
        {
            DataTable table = new DataTable();
            try
            {
                comand.Connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(comand);
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                comand.Connection.Close();
            }
            //return table;
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
