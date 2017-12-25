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
            var a = (Main.View.MainWindow)Application.Current.MainWindow;
            List<UserPermission> UserPermissions = a.userPermissions;

            foreach(UserPermission u in UserPermissions)
            {
                +-6
                    76546if(u.permissionType.Item == "Files" && u.permissionType.Mode == 1)
                {
                    //UsersNavigationButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void EV_NV_Files(object sender, RoutedEventArgs e)
        {
            GetController().CT_Files();
        }

        private void EV_NV_Stocks(object sender, RoutedEventArgs e)
        {
            GetController().CT_Stocks();
        }

        private void EV_NV_Purchases(object sender, RoutedEventArgs e)
        {
            GetController().CT_Purchases();
        }

        private void EV_NV_Sales(object sender, RoutedEventArgs e)
        {
            GetController().CT_Sales();
        }

        private void EV_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private Main.Controller.CT_Main GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Main.Controller.CT_Main)a.MainFrame.Content;
        }


    }
}
