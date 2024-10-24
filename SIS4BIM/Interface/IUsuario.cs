using SIS4BIM.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Interface
{
    public interface IUsuario
    {
        Usuario Get(byte id);
        byte ValidacionUsuario(Usuario t);
        int ActualizarContrasena(Usuario t);
        string HashPassword(string contrasenia);
        string GenerarContrasena(string nombre, string primerApellido);
        string GenerarUsuario(string nombre, string primerApellido);
    }
}
