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
using Microsoft.EntityFrameworkCore;
using FrameworkDB.V1;

namespace GestCloudv2.Main.View
{
    /// <summary>
    /// Interaction logic for NV_Main.xaml
    /// </summary>
    public partial class NV_Main : Page
    {
        public NV_Main()
        {
            InitializeComponent();
            var a = (Main.View.MC_Main)Application.Current.MainWindow;
            List<UserPermission> UserPermissions = a.UserPermissions;

            foreach(UserPermission u in UserPermissions)
            {
                if(u.permissionType.Item == "Users" && u.permissionType.Mode == 1)
                {
                    //UsersNavigationButton.Visibility = Visibility.Visible;
                }

                if (u.permissionType.Item == "Cards" && u.permissionType.Mode == 1)
                {
                    //CardsNavigationButton.Visibility = Visibility.Visible;
                }

                if (u.permissionType.Item == "Stock" && u.permissionType.Mode == 1)
                {
                    //StockNavigationButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void EV_NV_Files(object sender, RoutedEventArgs e)
        {
            GetController().ChangeMode(1);
        }

        private void CardsNavigationEvent(object sender, RoutedEventArgs e)
        {
            GetController().ChangeMode(2);
        }

        private void StockNavigationEvent(object sender, RoutedEventArgs e)
        {
            GetController().ChangeMode(3);
        }

        private Main.Controller.CT_Main GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MC_Main)mainWindow;
            return (Main.Controller.CT_Main)a.MainPage.Content;
        }
    }
}
