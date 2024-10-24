using SIS4BIM.Implementacion;
using SIS4BIM.Modelo;
using SIS4MIN_W.Alerts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIS4MIN_W.Views
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : UserControl
    {
        private MessageP customMessageBox;
        Usuario t;
        byte id;
        UsuarioImplementacion usuarioImpl;
        public ChangePassword(Usuario t)
        {
            InitializeComponent();
            this.t = t;
        }

        private void btnClearPwd_Click(object sender, RoutedEventArgs e)
        {
            txbPwdActual.Clear();
            txbPwdNueva.Clear();
            txbPwdConfirmar.Clear();
            txbPwdNueva.IsEnabled = false;
            txbPwdConfirmar.IsEnabled = false;
            txbPwdActual.IsEnabled = true;
            txbPwdActual.Focus();
        }

        private void btnConfirmPwd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                usuarioImpl = new UsuarioImplementacion();
                if (usuarioImpl.HashPassword1(txbPwdActual.Password) == t.Contrasenia)
                {
                    txbPwdActual.IsEnabled = false;
                    txbPwdConfirmar.IsEnabled = true;
                    txbPwdNueva.IsEnabled = true;
                    txbPwdNueva.Focus();
                }
                else
                {
                    txbPwdActual.SelectAll();
                    txbPwdActual.Focus();
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

        public bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;
            if (!password.Any(char.IsUpper))
                return false;
            if (!password.Any(char.IsLower))
                return false;
            if (!password.Any(char.IsDigit))
                return false;
            if (!password.Any(ch => !(char.IsNumber(ch) || char.IsLetter(ch))))
                return false;
            return true;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txbPwdActual.IsEnabled)
            {
                //MessageBox.Show("Debe ingresar su contraseña actual");
                customMessageBox = new MessageP("Debe ingresar su contraseña actual", "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
                txbPwdActual.Focus();
            }
            else if (txbPwdNueva.Password != txbPwdConfirmar.Password)
            {
                //MessageBox.Show("Las Contraseñas deben ser las mismas");
                customMessageBox = new MessageP("Las Contraseñas deben ser las mismas", "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
                txbPwdNueva.Clear();
                txbPwdConfirmar.Clear();
                txbPwdNueva.Focus();
            }
            else if (!IsValidPassword(txbPwdConfirmar.Password))
            {
                //MessageBox.Show("Bajo nivel de seguridad de contraseña, aumentela!");
                customMessageBox = new MessageP("Bajo nivel de seguridad de contraseña, aumentela!", "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
                txbPwdNueva.Clear();
                txbPwdConfirmar.Clear();
                txbPwdNueva.Focus();
            }
            else
            {
                try
                {
                    usuarioImpl = new UsuarioImplementacion();
                    t = usuarioImpl.Get(id);
                    t.Contrasenia = (txbPwdNueva.Password);
                    usuarioImpl.ActualizarContrasena(t);
                    //MessageBox.Show("Se ha cambiado su contraseña con exito, vuelva a ingresar");
                    customMessageBox = new MessageP("Se ha cambiado su contraseña con exito, vuelva a ingresar", "Vuelva");
                    customMessageBox.Owner = Window.GetWindow(this);
                    customMessageBox.ShowDialog();

                    string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    Process.Start(appPath);
                    Application.Current.Shutdown();
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
}
