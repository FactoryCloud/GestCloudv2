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

namespace GestCloudv2.UserItem.InfoUser
{
    /// <summary>
    /// Interaction logic for PermissionUser_ToolSide.xaml
    /// </summary>
    public partial class PermissionUser_ToolSide : Page
    {
        public PermissionUser_ToolSide()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(StartEvent);
        }

        private void StartEvent(object sender, RoutedEventArgs e)
        {
            if(GetController().Information["editable"] == 1)
            {
                EditModeButton.Visibility = Visibility.Hidden;
                SaveChangesButton.Visibility = Visibility.Visible;
            }
        }

        private void BasicPermissionEvent(object sender, RoutedEventArgs e)
        {
            GetController().ChangePermission(0);
        }

        private void UsersPermissionEvent(object sender, RoutedEventArgs e)
        {
            GetController().ChangePermission(1);
        }

        private void CardsPermissionEvent(object sender, RoutedEventArgs e)
        {
            GetController().ChangePermission(2);
        }

        private void SaveChangesEvent(object sender, RoutedEventArgs e)
        {
            GetController().SaveChanges();
        }

        private void EditModeEvent(object sender, RoutedEventArgs e)
        {
            ChangeToEdit();
        }

        public void ChangeToEdit()
        {
            EditModeButton.Visibility = Visibility.Hidden;
            SaveChangesButton.Visibility = Visibility.Visible;
            GetController().ChangeEditable(1);
        }

        private InfoUser.InfoUser_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MC_Main)mainWindow;
            return (InfoUser.InfoUser_Controller)a.MainPage.Content;
        }
    }
}
