using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace GestCloudv2.UserItem.InfoUser.Permissions
{
    /// <summary>
    /// Interaction logic for UsersPermissionUser_MainContent.xaml
    /// </summary>
    public partial class UsersPermissionUser_MainContent : Page
    {
        public List<UserPermission> UserPermissions;

        public UsersPermissionUser_MainContent()
        {
            InitializeComponent();

            GestCloudDB db = new GestCloudDB();
            UserPermissions = new List<UserPermission>();

            UserPermissions = db.UserPermissions.Where(u => u.user == GetController().userView.user)
                .Include(u => u.user).Include(u => u.permissionType).ToList();

            this.Loaded += new RoutedEventHandler(StartUserPermissions);
        }

        private void StartUserPermissions(object sender, RoutedEventArgs e)
        {
            foreach(UserPermission permission in UserPermissions)
            {
                if(permission.permissionType.Item == "Users")
                {
                    if(permission.permissionType.Mode == 1)
                    {
                        AccessYes.IsChecked=true;
                    }

                    if (permission.permissionType.Mode == 2)
                    {
                        InformationYes.IsChecked = true;
                    }

                    if (permission.permissionType.Mode == 3)
                    {
                        BasicEditYes.IsChecked = true;
                    }
                }
            }
        }

        private InfoUser.InfoUser_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (InfoUser.InfoUser_Controller)a.MainPage.Content;
        }
    }
}
