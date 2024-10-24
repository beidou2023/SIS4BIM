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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIS4MIN_W.Modal;
using System.Data;
using SIS4MIN_W.Alerts;

namespace SIS4MIN_W.Views
{
    public partial class BasicView : UserControl
    {
        private MessageP customMessageBox;
        UsuarioImplementacion usuarioImp;
        byte id;
        byte myid;
        Usuario t;
        public BasicView(byte myid)
        {
            InitializeComponent();
            this.myid = myid;
        }

        public void Selecte()
        {
            try
            {
                usuarioImp = new UsuarioImplementacion();
                dgvUsuarios.ItemsSource = null;
                dgvUsuarios.ItemsSource = usuarioImp.Select().DefaultView;
                this.dgvUsuarios.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                customMessageBox = new MessageP(ex.Message, "Upss");
                customMessageBox.Owner = Window.GetWindow(this);
                customMessageBox.ShowDialog();
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            NewUModal modalUsuario = new NewUModal(0,myid);
            modalUsuario.Owner = Window.GetWindow(this);
            modalUsuario.ShowDialog();
            if (modalUsuario.DialogResult == true)
            {
                Selecte();
            }


        }

        private void btnMod_Click(object sender, RoutedEventArgs e)
        {
            if (dgvUsuarios.SelectedItem != null && dgvUsuarios.Items.Count > 0)
            {
                NewUModal modalUsuario = new NewUModal(id,myid);
                modalUsuario.Owner = Window.GetWindow(this);
                modalUsuario.ShowDialog();

                if (modalUsuario.DialogResult == true)
                {
                    Selecte();
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgvUsuarios.SelectedItem != null && dgvUsuarios.Items.Count > 0 && t != null)
            {
                if (t.Id == myid)
                {
                    //MessageBox.Show("No puedes eliminarte a ti mismo");
                    customMessageBox = new MessageP("No puedes eliminarte a ti mismo", "Upss");
                    customMessageBox.Owner = Window.GetWindow(this);
                    customMessageBox.ShowDialog();
                }
                //else if (MessageBox.Show("Esta seguro de eliminar el registro", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                else
                {
                    customMessageBox = new MessageP("Esta seguro de eliminar el registro?", "Eliminar",true);
                    customMessageBox.Owner = Window.GetWindow(this);
                    customMessageBox.ShowDialog();
                    if (customMessageBox.Result)
                    {
                        try
                        {
                            usuarioImp = new UsuarioImplementacion();
                            if (usuarioImp.Delete(t) > 0)
                            {
                                //MessageBox.Show("Registro eliminado");
                                customMessageBox = new MessageP("Registro eliminado", "Bye bye");
                                customMessageBox.Owner = Window.GetWindow(this);
                                customMessageBox.ShowDialog();
                                Selecte();
                            }
                            else
                            {
                                //MessageBox.Show("No se eliminaron registros");
                                customMessageBox = new MessageP("No se eliminaron registros", "Upss");
                                customMessageBox.Owner = Window.GetWindow(this);
                                customMessageBox.ShowDialog();
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
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Selecte();
        }

        private void dgvUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvUsuarios.SelectedItem != null && dgvUsuarios.Items.Count > 0)
            {
                DataRowView d = (DataRowView)dgvUsuarios.SelectedItem;
                id = byte.Parse(d.Row.ItemArray[0].ToString());
                t = null;
                try
                {
                    usuarioImp = new UsuarioImplementacion();
                    t = usuarioImp.Get(id);
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
