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
    /// <summary>
    /// Interaction logic for EmpyView.xaml
    /// </summary>
    public partial class EmpyView : UserControl
    {
        private string title;
        public EmpyView(string title)
        {
            InitializeComponent();
            this.title = title;
            txtTitle.Text = this.title;
        }
    }
}
