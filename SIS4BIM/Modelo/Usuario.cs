﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Modelo
{
    public class Usuario :BaseModelo
    {
        #region Atributos
        public byte Id { get; set; }
        public string Ci { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public char Sexo { get; set; }
        public string Rol { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        #endregion

        #region Constructores
        public Usuario()
        {
        }

        /// <summary>
        /// Verificar Inicio
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="contrasenia"></param>
        public Usuario(string nombreUsuario, string contrasenia)
        {
            NombreUsuario = nombreUsuario;
            Contrasenia = contrasenia;
        }


        /// <summary>
        /// GET
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ci"></param>
        /// <param name="nombres"></param>
        /// <param name="primerApellido"></param>
        /// <param name="segundoApellido"></param>
        /// <param name="fechaNacimiento"></param>
        /// <param name="sexo"></param>
        /// <param name="rol"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="contrasenia"></param>
        /// <param name="estado"></param>
        /// <param name="fechaRegistro"></param>
        /// <param name="fechaActualizacion"></param>
        public Usuario(byte id, string ci, string nombres, string primerApellido, string segundoApellido, DateTime fechaNacimiento, char sexo, string rol, string nombreUsuario, string contrasenia, byte estado, DateTime fechaRegistro, DateTime fechaActualizacion)
            : base(estado, fechaRegistro, fechaActualizacion)
        {
            Id = id;
            Ci = ci;
            Nombres = nombres;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
            FechaNacimiento = fechaNacimiento;
            Sexo = sexo;
            Rol = rol;
            NombreUsuario = nombreUsuario;
            Contrasenia = contrasenia;
        }

        /// <summary>
        /// INSERT
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="nombres"></param>
        /// <param name="primerApellido"></param>
        /// <param name="segundoApellido"></param>
        /// <param name="fechaNacimiento"></param>
        /// <param name="sexo"></param>
        /// <param name="rol"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="contrasenia"></param>
        public Usuario(string ci, string nombres, string primerApellido, string segundoApellido, DateTime fechaNacimiento, char sexo, string rol, string nombreUsuario, string contrasenia)
        {
            Ci = ci;
            Nombres = nombres;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
            FechaNacimiento = fechaNacimiento;
            Sexo = sexo;
            Rol = rol;
            NombreUsuario = nombreUsuario;
            Contrasenia = contrasenia;
        }
        #endregion

    }
}
