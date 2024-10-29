using SIS4BIM.Implementacion;
using SIS4BIM.Modelo;
using SIS4MIN_W.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
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
    /// Interaction logic for Reporte2View.xaml
    /// </summary>
    public partial class Reporte2View : UserControl
    {
        ProductoImplementacion productoImpl;
        Producto productol;
        string opcion1, opcion2;
        private MessageP customMessageBox;
        public Reporte2View()
        {
            InitializeComponent();
        }

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            opcion1 = ((ComboBoxItem)itemPicker.SelectedItem)?.Content.ToString();

            if (opcion1 == null)
            {
                customMessageBox = new MessageP("Debe seleccionar una opción para filtrar", "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
                return;
            }

            if (opcion1 == "CATEGORIA")
            {
                opcion2 = ((ComboBoxItem)itemPicker2.SelectedItem)?.Content.ToString();
            }
            else if (opcion1 == "DEPARTAMENTO")
            {
                opcion2 = ((ComboBoxItem)itemPicker3.SelectedItem)?.Content.ToString();
            }

            if (opcion2 == null)
            {
                customMessageBox = new MessageP("Debe seleccionar una opción en el segundo filtro", "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
                return;
            }
            string querry = "";
            if (opcion2 == "Todos")
                querry = "%";
            else
                querry = opcion2;

            try
            {
                productoImpl = new ProductoImplementacion();
                dgvReporte.ItemsSource = null;

                if (opcion1 == "CATEGORIA")
                {
                    dgvReporte.ItemsSource = productoImpl.SelectFiltroCategoria(querry).DefaultView;
                }
                else if (opcion1 == "DEPARTAMENTO")
                {
                    dgvReporte.ItemsSource = productoImpl.SelectFiltroDepartamento(querry).DefaultView;
                }
            }
            catch (Exception ex)
            {
                customMessageBox = new MessageP(ex.Message, "Mensaje");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
            }
        }


        private void itemPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            opcion1 = ((ComboBoxItem)itemPicker.SelectedItem)?.Content.ToString();
            switch (opcion1)
            {
                case "CATEGORIA":
                    stpEjemplo.Visibility = Visibility.Collapsed;
                    stpCategorias.Visibility = Visibility.Visible;
                    stpDepartamento.Visibility = Visibility.Collapsed;
                    stpDepartamento.IsEnabled = true;
                    break;
                case "DEPARTAMENTO":
                    stpEjemplo.Visibility = Visibility.Collapsed;
                    stpDepartamento.Visibility = Visibility.Visible;
                    stpCategorias.Visibility = Visibility.Collapsed;
                    stpCategorias.IsEnabled = true;
                    break;
                default:
                    stpEjemplo.Visibility = Visibility.Visible;
                    stpCategorias.Visibility = Visibility.Collapsed;
                    stpDepartamento.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
