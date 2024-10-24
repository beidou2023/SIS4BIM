using SIS4BIM.Implementacion;
using SIS4BIM.Modelo;
using SIS4MIN_W.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIS4MIN_W.Modal
{
    public partial class NewUModal : Window
    {
        private MessageP customMessageBox;
        Usuario t;
        byte id,myid;
        string ci, nombres, primerApellido, segundoApellido, rol;
        char sexo;
        UsuarioImplementacion usuarioImpl;
        public NewUModal(byte id,byte myid)
        {
            InitializeComponent();
            this.id = id;
            this.myid = myid;
            if (this.id != 0)
            {
                ModUser();
            }
        }

        private void txbPrimerApellido_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string primerApellido = txbPrimerApellido.Text;

            if (!string.IsNullOrWhiteSpace(primerApellido))
            {
                txbSegundoApellido.IsEnabled = true;
            }
            else
            {
                txbSegundoApellido.IsEnabled = false;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            ci = txbCI.Text;
            nombres = txbNombres.Text;
            primerApellido = txbPrimerApellido.Text;
            if (!string.IsNullOrWhiteSpace(primerApellido))
            {
                txbSegundoApellido.IsEnabled = true;
            }
            segundoApellido = txbSegundoApellido.Text;

            DateTime? fechaNacimiento = dtpFechaNacimiento.SelectedDate;

            sexo = 'N';
            if (rbMasculino.IsChecked == true)
                sexo = 'M';
            else if (rbFemenino.IsChecked == true)
                sexo = 'F';

            rol = "";
            if (rbCajero.IsChecked == true)
                rol = "Cajero";
            else if (rbAdministrador.IsChecked == true)
                rol = "Administrador";


            if (string.IsNullOrWhiteSpace(ci))
            {
                //MessageBox.Show("Debe ingresar el CI.");
                customMessageBox = new MessageP("Debe ingresar el CI.", "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
                txbCI.Focus();
            }
            else if (string.IsNullOrWhiteSpace(nombres))
            {
                //MessageBox.Show("Debe ingresar los nombres.");
                customMessageBox = new MessageP("Debe ingresar los nombres.", "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
                txbNombres.Focus();
            }
            else if (string.IsNullOrWhiteSpace(nombres) || !nombres.All(char.IsLetter))
            {
                //MessageBox.Show("Debe ingresar un nombre válido, solo letras.");
                customMessageBox = new MessageP("Debe ingresar un nombre válido, solo letras.", "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
                txbNombres.SelectAll();
                txbNombres.Focus();
            }
            else if (string.IsNullOrWhiteSpace(primerApellido))
            {
                //MessageBox.Show("Debe ingresar el primer apellido.");
                customMessageBox = new MessageP("Debe ingresar el primer apellido.", "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
                txbPrimerApellido.Focus();
            }
            else if (string.IsNullOrWhiteSpace(primerApellido) || !primerApellido.All(char.IsLetter))
            {
                //MessageBox.Show("Debe ingresar un apellido válido, solo letras.");
                customMessageBox = new MessageP("Debe ingresar un apellido válido, solo letras.", "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
                txbPrimerApellido.SelectAll();
                txbPrimerApellido.Focus();
            }
            else if (!segundoApellido.All(char.IsLetter))
            {
                //MessageBox.Show("Debe ingresar un apellido válido, solo letras.");
                customMessageBox = new MessageP("Debe ingresar un apellido válido, solo letras.", "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
                txbSegundoApellido.SelectAll();
                txbSegundoApellido.Focus();
            }

            else if (!fechaNacimiento.HasValue || fechaNacimiento.Value > DateTime.Today)
            {
                //MessageBox.Show("Debe ingresar una fecha de nacimiento válida.");
                customMessageBox = new MessageP("Debe ingresar una fecha de nacimiento válida.", "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
                dtpFechaNacimiento.Focus();
            }
            else if (rbFemenino.IsChecked == false && rbMasculino.IsChecked == false)
            {
                //MessageBox.Show("Debe ingresar el sexo de la persona.");
                customMessageBox = new MessageP("Debe ingresar el sexo de la persona.", "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
            }
            else if (rbCajero.IsChecked == false && rbAdministrador.IsChecked == false)
            {
                //MessageBox.Show("Debe ingresar el rol de la persona.");
                customMessageBox = new MessageP("Debe ingresar el rol de la persona.", "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
            }
            else
            {

                if (this.id == 0)
                {
                    try
                    {
                        usuarioImpl = new UsuarioImplementacion();
                        string username = usuarioImpl.GenerarUsuario(nombres,primerApellido);
                        string password = usuarioImpl.GenerarContrasena(nombres,primerApellido);
                        t = new Usuario(
                            ci,
                            nombres,
                            primerApellido,
                            segundoApellido,
                            fechaNacimiento.Value,
                            sexo,
                            rol,
                            username,
                            usuarioImpl.HashPassword1(password)
                        );
                        if (usuarioImpl.Insert(t) > 0){
                            //MessageBox.Show($"Registro con Exito\nRecuerde su Usuario: {username}\nContraseña: {password}");
                            customMessageBox = new MessageP($"Registro con Exito\nRecuerde su Usuario: {username}\nContraseña: {password}", "Mensaje");
                            customMessageBox.Owner = Window.GetWindow(this);
                            customMessageBox.ShowDialog();
                        }
                        else{
                            //MessageBox.Show("No se insertaron registros");
                            customMessageBox = new MessageP("No se insertaron registros", "Mensaje");
                            customMessageBox.Owner = Window.GetWindow(this);
                            customMessageBox.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message);
                        customMessageBox = new MessageP(ex.Message, "Upss");
                        customMessageBox.Owner = Window.GetWindow(this);
                        customMessageBox.ShowDialog();
                    }
                }
                else
                {
                    try
                    {
                        if (t.Ci == ci.Trim() && t.Nombres == nombres.ToUpper() && t.PrimerApellido == primerApellido.ToUpper() && t.SegundoApellido == segundoApellido.ToUpper() && t.FechaNacimiento == fechaNacimiento.Value && t.Sexo == sexo && t.Rol == rol){
                            //MessageBox.Show("No se actualizo nada");
                            customMessageBox = new MessageP("No se actualizo nada", "Mensaje");
                            customMessageBox.Owner = Window.GetWindow(this);
                            customMessageBox.ShowDialog();
                        }
                        else
                        {
                            t.Ci = ci;
                            t.Nombres = nombres;
                            t.PrimerApellido = primerApellido;
                            t.SegundoApellido = segundoApellido;
                            t.FechaNacimiento = fechaNacimiento.Value;
                            t.Sexo = sexo;
                            t.Rol = rol;
                            if (usuarioImpl.Update(t) > 0){
                                //MessageBox.Show("Registro Actualizado con Exito");
                                customMessageBox = new MessageP("Registro Actualizado con Exito", "Mensaje");
                                customMessageBox.Owner = Window.GetWindow(this);
                                customMessageBox.ShowDialog();
                            }
                            else{
                                //MessageBox.Show("No se insertaron registros");
                                customMessageBox = new MessageP("No se insertaron registros", "Mensaje");
                                customMessageBox.Owner = Window.GetWindow(this);
                                customMessageBox.ShowDialog();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        customMessageBox = new MessageP(ex.Message, "Upss");
                        customMessageBox.Owner = Window.GetWindow(this);
                        customMessageBox.ShowDialog();
                    }
                }
                this.DialogResult = true;
                this.Close();
            }
        }

        private void txbPrimerApellido_TextChanged(object sender, TextChangedEventArgs e)
        {
            string primerApellido = txbPrimerApellido.Text;

            if (!string.IsNullOrWhiteSpace(primerApellido))
            {
                txbSegundoApellido.IsEnabled = true;
            }
            else
            {
                txbSegundoApellido.IsEnabled = false;
            }
        }

        public void ModUser()
        {
            try
            {
                usuarioImpl = new UsuarioImplementacion();
                t = usuarioImpl.Get(id);
                txbCI.Text = t.Ci;
                txbNombres.Text = t.Nombres;
                txbPrimerApellido.Text = t.PrimerApellido;
                txbSegundoApellido.Text = t.SegundoApellido;
                dtpFechaNacimiento.Text = t.FechaNacimiento.ToString();
                if (t.Sexo == 'M')
                    rbMasculino.IsChecked = true;
                else
                    rbFemenino.IsChecked = true;

                if (t.Rol == "Cajero")
                    rbCajero.IsChecked = true;
                else
                    rbAdministrador.IsChecked = true;
                if (myid == t.Id) {
                    rbCajero.IsEnabled = false;
                    rbAdministrador.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                customMessageBox = new MessageP(ex.Message, "Upss");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
            }
        }
    }
}
