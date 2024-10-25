using SIS4BIM.Implementacion;
using SIS4BIM.Modelo;
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

using SIS4MIN_W.Modal;
using SIS4MIN_W.Ventanas;
using SIS4MIN_W.Views;
using SIS4MIN_W.Alerts;

namespace SIS4MIN_W.Windows
{
    public partial class WININICIO : Window
    {
        private MessageP customMessageBox;
        public Usuario t;
        UsuarioImplementacion usuarioImpl;
        public WININICIO()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                usuarioImpl = new UsuarioImplementacion();
                if (txtUser.Text != "" && txtPass.Password != "")
                {
                    t = new Usuario(txtUser.Text, txtPass.Password);
                    if (usuarioImpl.ValidacionUsuario(t) > 0)
                    {
                        byte id = usuarioImpl.ValidacionUsuario(t);
                        t = new Usuario();
                        t = usuarioImpl.Get(id);
                        if (t.Rol == "Cajero")
                        {
                            WinCajero winCajero = new WinCajero(t);
                            winCajero.Show();
                            this.Close();
                        }
                        else
                        {
                            WinAdm winAdmin = new WinAdm(t);
                            winAdmin.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Usuario/Contraseña incorrecto");
                        customMessageBox = new MessageP("Usuario/Contraseña incorrecto", "Mensaje");
                        customMessageBox.Owner = Window.GetWindow(this);
                        customMessageBox.ShowDialog();
                    }
                }
                else
                {
                    //MessageBox.Show("Debe ingresar un usuario y contraseña");
                    customMessageBox = new MessageP("Debe ingresar un usuario y contraseña", "Mensaje");
                    customMessageBox.Owner = Window.GetWindow(this);
                    customMessageBox.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //ex
                customMessageBox = new MessageP(ex.Message, "Upss");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
            }
        }
        private void btnClose_Click_1(object sender, RoutedEventArgs e)
        {
            customMessageBox = new MessageP("¿Estás seguro de que desea salir?","Ya te vas? :c",true);
            customMessageBox.Owner = Window.GetWindow(this);
            customMessageBox.ShowDialog();
            if (customMessageBox.Result)
            {
                this.Close();
            }
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                btnLogin_Click(this, new RoutedEventArgs());
            }
        }
    }
}
