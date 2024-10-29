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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIS4MIN_W.Views
{
    public partial class Reporte1View : UserControl
    {
        ProductoImplementacion productoImpl;
        private MessageP customMessageBox;
        Producto producto;
        DateTime desde, hasta;
        public Reporte1View()
        {
            InitializeComponent();
        }

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                desde = dtpDesde.SelectedDate.Value;
                hasta = dtpHasta.SelectedDate.Value;
                if (desde > hasta) {
                    customMessageBox = new MessageP("Valor Inicio es mayor al valor final, se voltearon las fechas", "Mensaje");
                    customMessageBox.Owner = Window.GetWindow(this);
                    customMessageBox.ShowDialog();
                    DateTime aux = desde;
                    dtpDesde.Text = hasta.ToString();
                    dtpHasta.Text=aux.ToString();
                    desde = hasta;
                    hasta= aux;
                }
                Selecte();
            }
            catch (Exception ex)
            {

                customMessageBox = new MessageP(ex.Message, "Upss");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
            }

        }


        private void Selecte()
        {
            try
            {
                productoImpl = new ProductoImplementacion();
                dgvReporte.ItemsSource = null;
                dgvReporte.ItemsSource = productoImpl.SelectFiltro(desde, hasta).DefaultView;
            }
            catch (Exception ex)
            {
                customMessageBox = new MessageP(ex.Message, "Upss");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
            }
        }

        private void btnReporte_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
