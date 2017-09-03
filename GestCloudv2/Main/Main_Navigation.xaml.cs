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

namespace GestCloudv2
{
    /// <summary>
    /// Interaction logic for Main_Navigation.xaml
    /// </summary>
    public partial class Main_Navigation : Page
    {
        public Main_Navigation()
        {
            InitializeComponent();
            var a = (MainWindow)Application.Current.MainWindow;
            List<UserPermission> UserPermissions = a.UserPermissions;

            foreach(UserPermission u in UserPermissions)
            {
                if(u.permissionType.Item == "Users" && u.permissionType.Subitem == "General" && u.permissionType.Mode == 1)
                {
                    UsersNavigationButton.Visibility = Visibility.Visible;
                }

                if (u.permissionType.Item == "Cards" && u.permissionType.Subitem == "General" && u.permissionType.Mode == 1)
                {
                    CardsNavigationButton.Visibility = Visibility.Hidden;
                }
            }
        }

        private void UsersNavigationEvent(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;

            a.changeLeftSide(new UserList_ToolSide());
            a.changeMainContent(new UserList_MainContent());
        }

        private void CardsNavigationEvent(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;

            a.changeLeftSide(new CardList.CardList_ToolSide());
            a.changeMainContent(new CardList.CardList_MainContent());
        }
    }
}
