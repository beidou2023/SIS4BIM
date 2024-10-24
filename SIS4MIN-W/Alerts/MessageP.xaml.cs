using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SIS4MIN_W.Alerts
{
    /// <summary>
    /// Interaction logic for MessageP.xaml
    /// </summary>
    public partial class MessageP : Window
    {
        public bool Result { get; private set; }

        // modalUsuario.Owner = Window.GetWindow(this);
        public MessageP(string message, string titulo, bool showNoButton = false)
        {
            InitializeComponent();
            txtContenido.Text = message;
            txtTitulo.Text = titulo;

            if (showNoButton)
            {
                YesButton.Visibility = Visibility.Visible;
                NoButton.Visibility = Visibility.Visible;
            }
            else
            {
                YesButton.Visibility = Visibility.Visible;
                NoButton.Visibility = Visibility.Collapsed;
            }
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Result = false; 
            this.Close();
        }

        private void btnClearPwd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
