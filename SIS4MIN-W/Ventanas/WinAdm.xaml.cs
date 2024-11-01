﻿using SIS4BIM.Modelo;
using SIS4MIN_W.Alerts;
using SIS4MIN_W.Views;
using SIS4MIN_W.Windows;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIS4MIN_W.Ventanas
{

    public partial class WinAdm : Window
    {
        private MessageP customMessageBox;
        Usuario t;
        public WinAdm(Usuario t)
        {
            InitializeComponent();
            this.t = t;
            txtAutor.Text = $"ADMINISTRADOR\n({t.NombreUsuario})";
            btnInit.IsChecked = true;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //msg
            customMessageBox = new MessageP("¿Estás seguro de que desea salir?", "Ya te vas? :c", true);
            customMessageBox.Owner = Window.GetWindow(this);
            customMessageBox.ShowDialog();
            if (customMessageBox.Result)
            {
                Application.Current.Shutdown();
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            string vistaSeleccionada = radioButton?.Tag.ToString();
            CambiarVista(vistaSeleccionada);
        }

        private void CambiarVista(string vista)
        {
            switch (vista)
            {
                case "Vista1":
                    ContentArea.Content = new BasicView(t.Id);
                    break;
                case "Vista2":
                    ContentArea.Content = new EmpyView("PROVEEDORES");
                    break;
                case "Vista3":
                    ContentArea.Content = new EmpyView("CATEGORIAS");
                    break;
                case "Vista4":
                    ContentArea.Content = new EmpyView("PRODUCTOS");
                    break;
                case "Vista5":
                    ContentArea.Content = new Reporte2View();
                    break;
                case "Vista6":
                    ContentArea.Content = new ChangePassword(t);
                    break;
                default:
                    break;
            }
        }

        private void btnLgin_Click(object sender, RoutedEventArgs e)
        {
            //msg
            customMessageBox = new MessageP("Esta a punto de cerrar sesión", "Cerrar sesión", true);
            customMessageBox.Owner = Window.GetWindow(this);
            customMessageBox.ShowDialog();
            if (customMessageBox.Result)
            {
                t = null;
                WININICIO winInicio = new WININICIO();
                winInicio.Show();
                this.Close();
            }
        }
    }
}
