using SIS4BIM.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SIS4BIM.Interface;

namespace SIS4BIM.Implementacion
{
    public class UsuarioImplementacion : BaseImplementacion, IUsuario
    {
        private Random random;
        public Usuario Get(byte id)
        {
            Usuario t = null;

            this.query = @"SELECT id, ci,nombres, primerApellido, segundoApellido,
                            fechaNacimiento,sexo,rol,nombreUsuario,contrasenia,estado,
                            fechaRegistro, IFNULL(fechaActualizacion,CURRENT_TIMESTAMP()) 
                            AS fechaActualizacion
                            FROM  usuario
                            WHERE id=@id;";
            MySqlCommand comando = CreateBasicCommand(this.query);
            comando.Parameters.AddWithValue("@id", id);
            try
            {
                DataTable table = ExecuteDataTableCommand(comando);
                if (table.Rows.Count > 0)
                {
                    t = new Usuario(
                        byte.Parse(table.Rows[0][0].ToString()),
                        table.Rows[0][1].ToString(),
                        table.Rows[0][2].ToString(),
                        table.Rows[0][3].ToString(),
                        table.Rows[0][4].ToString(),
                        DateTime.Parse(table.Rows[0][5].ToString()),
                        char.Parse(table.Rows[0][6].ToString()),
                        table.Rows[0][7].ToString(),
                        table.Rows[0][8].ToString(),
                        table.Rows[0][9].ToString(),
                        byte.Parse(table.Rows[0][10].ToString()),
                        DateTime.Parse(table.Rows[0][11].ToString()),
                        DateTime.Parse(table.Rows[0][12].ToString())
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
            this.query = @"SELECT id,ci AS 'CI', concat(nombres,' ',primerApellido,' ',
                            IFNULL(segundoApellido,''))
                            AS 'Nombre Completo',sexo AS 'Sexo',rol AS 'Rol',nombreUsuario 
                            AS 'Username',fechaRegistro AS 'Fecha Registro'
                            FROM usuario WHERE estado=1;";
            MySqlCommand command = CreateBasicCommand(this.query);
            return ExecuteDataTableCommand(command);
        }

        public int Insert(Usuario t)
        {
            int n = 0;
            this.query = @"INSERT INTO usuario (ci,nombres,primerApellido,segundoApellido,
                            fechaNacimiento,sexo,rol,nombreUsuario,
                            contrasenia,estado,fechaRegistro) 
                            VALUES (@ci,@nombres,@primerApellido,@segundoApellido,
                            @fechaNacimiento,@sexo,@rol,@nombreUsuario,
                            @contrasenia,1,CURRENT_TIME()) ";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@ci", t.Ci);
            comand.Parameters.AddWithValue("@nombres", t.Nombres.ToUpper());
            comand.Parameters.AddWithValue("@primerApellido", t.PrimerApellido.ToUpper());
            comand.Parameters.AddWithValue("@segundoApellido", t.SegundoApellido.ToUpper());
            comand.Parameters.AddWithValue("@fechaNacimiento", t.FechaNacimiento);
            comand.Parameters.AddWithValue("@sexo", t.Sexo);
            comand.Parameters.AddWithValue("@rol", t.Rol);
            comand.Parameters.AddWithValue("@nombreUsuario", t.NombreUsuario);
            comand.Parameters.AddWithValue("@contrasenia", HashPassword1(t.Contrasenia));
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

        public int ActualizarContrasena(Usuario t)
        {
            int n = 0;
            this.query = @"UPDATE usuario 
                            SET contrasenia=@contrasenia
                            WHERE id=@id;";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@id", t.Id);
            comand.Parameters.AddWithValue("@contrasenia", HashPassword1(t.Contrasenia));
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

        public int Update(Usuario t)
        {
            int n = 0;
            this.query = @"UPDATE usuario 
                            SET ci=@ci,nombres=@nombres,primerApellido=@primerApellido,
                            segundoApellido=@segundoApellido,fechaNacimiento=@fechaNacimiento,
                            sexo=@sexo,rol=@rol,fechaActualizacion=NOW()
                            WHERE id=@id;";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@ci", t.Ci);
            comand.Parameters.AddWithValue("@nombres", t.Nombres.ToUpper());
            comand.Parameters.AddWithValue("@primerApellido", t.PrimerApellido.ToUpper());
            comand.Parameters.AddWithValue("@segundoApellido", t.SegundoApellido.ToUpper());
            comand.Parameters.AddWithValue("@fechaNacimiento", t.FechaNacimiento);
            comand.Parameters.AddWithValue("@sexo", t.Sexo);
            comand.Parameters.AddWithValue("@rol", t.Rol);
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

        public int Delete(Usuario t)
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

        public byte ValidacionUsuario(Usuario t)
        {
            byte veri = 0;
            this.query = @"SELECT id,nombreUsuario,contrasenia 
                            FROM usuario
                            WHERE nombreUsuario=@nombreUsuario
                            AND contrasenia=@contrasenia;";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@nombreUsuario", t.NombreUsuario);
            comand.Parameters.AddWithValue("@contrasenia", HashPassword1(t.Contrasenia));
            try
            {
                DataTable table = ExecuteDataTableCommand(comand);
                if (table.Rows.Count > 0)
                {
                    veri = byte.Parse(table.Rows[0][0].ToString());
                }
                return veri;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string HashPassword1(string contrasenia)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(contrasenia);


                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }


        public string GenerarContrasena(string nombre, string primerApellido)
        {
            random = new Random();
            string numerosAleatorios = "";
            for (int i = 0; i < 6; i++)
            {
                numerosAleatorios += random.Next(0, 10);
            }
            string primeraLetraNombre = nombre[0].ToString().ToUpper();
            string primeraLetraApellido = primerApellido[0].ToString().ToUpper();
            return (primeraLetraNombre.ToString().ToUpper() + primeraLetraApellido.ToString().ToUpper() + numerosAleatorios);
        }

        public string GenerarUsuario(string nombre, string primerApellido)
        {
            string usuario = "";
            usuario += nombre[0].ToString().ToUpper();
            usuario += primerApellido.ToString().ToUpper();
            usuario += "-";

            string aux = "";
            string numerosAleatorios = "";
            random = new Random();
            do
            {
                numerosAleatorios = "";
                aux = usuario;
                for (int i = 0; i < 6; i++)
                    numerosAleatorios += random.Next(0, 10);
                aux += numerosAleatorios;
            } 
            while (VerificarUser(aux));

            return usuario+numerosAleatorios;
        }

        private bool VerificarUser(string username) {
            this.query = @"SELECT nombreUsuario FROM usuario WHERE nombreUsuario=@username";
            MySqlCommand comand = CreateBasicCommand(this.query);
            comand.Parameters.AddWithValue("@username", username);
            try
            {
                DataTable table = ExecuteDataTableCommand(comand);
                if (table.Rows.Count > 0)
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

    }
}
