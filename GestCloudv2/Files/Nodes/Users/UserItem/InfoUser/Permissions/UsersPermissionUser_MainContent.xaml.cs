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
        public UsersPermissionUser_MainContent()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(StartUserPermissions);
        }

        private void StartUserPermissions(object sender, RoutedEventArgs e)
        {
            foreach(UserPermission permission in GetController().GetPermissions())
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

                    if (permission.permissionType.Mode == 4)
                    {
                        AdvancedEditYes.IsChecked = true;
                    }

                    if (permission.permissionType.Mode == 5)
                    {
                        DeleteYes.IsChecked = true;
                    }

                    if (permission.permissionType.Mode == 6)
                    {
                        PermissionsYes.IsChecked = true;
                    }
                }
            }

            AccessYes.Checked += new RoutedEventHandler(UpdateInfoDB);
            AccessNo.Checked += new RoutedEventHandler(UpdateInfoDB);
            InformationYes.Checked += new RoutedEventHandler(UpdateInfoDB);
            InformationNo.Checked += new RoutedEventHandler(UpdateInfoDB);
            BasicEditYes.Checked += new RoutedEventHandler(UpdateInfoDB);
            BasicEditNo.Checked += new RoutedEventHandler(UpdateInfoDB);
            AdvancedEditYes.Checked += new RoutedEventHandler(UpdateInfoDB);
            AdvancedEditNo.Checked += new RoutedEventHandler(UpdateInfoDB);
            DeleteYes.Checked += new RoutedEventHandler(UpdateInfoDB);
            DeleteNo.Checked += new RoutedEventHandler(UpdateInfoDB);
            PermissionsYes.Checked += new RoutedEventHandler(UpdateInfoDB);
            PermissionsNo.Checked += new RoutedEventHandler(UpdateInfoDB);

            if(GetController().Information["editable"]==1)
            {
                AccessYes.IsEnabled = true;
                AccessNo.IsEnabled = true;
                InformationYes.IsEnabled = true;
                InformationNo.IsEnabled = true;
                BasicEditYes.IsEnabled = true;
                BasicEditNo.IsEnabled = true;
                AdvancedEditYes.IsEnabled = true;
                AdvancedEditNo.IsEnabled = true;
                DeleteYes.IsEnabled = true;
                DeleteNo.IsEnabled = true;
                PermissionsYes.IsEnabled = true;
                PermissionsNo.IsEnabled = true;
            }
        }

        private void UpdateInfoDB(object sender, RoutedEventArgs e)
        {
            int[] db = new int[6] { 0, 0, 0, 0, 0, 0 };
            int[] visual = new int[6] { 0, 0, 0, 0, 0, 0 };

            if (Convert.ToBoolean(AccessYes.IsChecked))
            {
                visual[0] = 1;
            }

            if (Convert.ToBoolean(InformationYes.IsChecked))
            {
                visual[1] = 1;
            }

            if (Convert.ToBoolean(BasicEditYes.IsChecked))
            {
                visual[2] = 1;
            }

            if (Convert.ToBoolean(AdvancedEditYes.IsChecked))
            {
                visual[3] = 1;
            }

            if (Convert.ToBoolean(DeleteYes.IsChecked))
            {
                visual[4] = 1;
            }

            if (Convert.ToBoolean(PermissionsYes.IsChecked))
            {
                visual[5] = 1;
            }

            foreach (UserPermission permission in GetController().GetPermissions())
            {
                if (permission.permissionType.Item == "Users")
                {
                    if (permission.permissionType.Mode == 1)
                    {
                        db[0] = 1;
                    }

                    if (permission.permissionType.Mode == 2)
                    {
                        db[1] = 1;
                    }

                    if (permission.permissionType.Mode == 3)
                    {
                        db[2] = 1;
                    }

                    if (permission.permissionType.Mode == 4)
                    {
                        db[3] = 1;
                    }

                    if (permission.permissionType.Mode == 5)
                    {
                        db[4] = 1;
                    }

                    if (permission.permissionType.Mode == 6)
                    {
                        db[5] = 1;
                    }
                }
            }

            MessageBox.Show($"{db[0]}, {db[1]}, {db[2]}, {db[3]}, {db[4]}, {db[5]}\n"
                + $"{visual[0]}, {visual[1]}, {visual[2]}, {visual[3]}, {visual[4]}, {visual[5]}");
            for (int i=0; i<6; i++)
            {
                if(db[i] == 1 && visual[i] == 0)
                {
                    MessageBox.Show($"Pierdes permiso {i + 1}");
                    GetController().DeletePermission("Users", i + 1);
                }

                else if (db[i] == 0 && visual[i] == 1)
                {
                    MessageBox.Show($"Ganas permiso {i + 1}");
                    GetController().CreatePermission("Users", i + 1);
                }
            }
        }

        private InfoUser.InfoUser_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MC_Main)mainWindow;
            return (InfoUser.InfoUser_Controller)a.MainPage.Content;
        }
    }
}
