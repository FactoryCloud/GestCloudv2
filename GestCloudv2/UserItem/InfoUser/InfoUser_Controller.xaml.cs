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
using FrameworkView.V1;

namespace GestCloudv2.UserItem.InfoUser
{
    /// <summary>
    /// Interaction logic for InfoUser_Controller.xaml
    /// </summary>
    public partial class InfoUser_Controller : Page
    {
        public UserView userView;
        public Dictionary<string, int> Information;
        private Page MainContentUser;
        private Page ToolSideUser;
        private Page NavigationUser;
        public GestCloudDB db;
        List<UserPermission> userPermissions;
        private User userlist;

        public InfoUser_Controller(User user, int editable)
        {
            InitializeComponent();
            Information = new Dictionary<string, int>();
            Information.Add("editable", editable);
            Information.Add("old_editable", 0);
            Information.Add("mode", 0);
            Information.Add("old_mode", 0);
            Information.Add("permission", 0);
            Information.Add("old_permission", 0);
            Information.Add("controller", 0);
            Information.Add("changes", 0);

            userView = new UserView(user);
            db = new GestCloudDB();
            userPermissions = db.UserPermissions.Where(u => (u.UserID == userView.user.UserID)).Include(u => u.permissionType).ToList();

            this.Loaded += new RoutedEventHandler(StartUserInfo_Event);
        }

        private void StartUserInfo_Event(object sender, RoutedEventArgs e)
        {
            MainContentUser = new InfoUser_MainContent();
            ToolSideUser = new InfoUser_ToolSide();
            NavigationUser = new InfoUser_Navigation();
            UpdateComponents();
        }

        public List<UserPermission> GetPermissions()
        {
            //List<UserPermission> userPermissions = db.UserPermissions.Where(u => (u.UserID == userView.user.UserID)).Include(u => u.permissionType).ToList();
            return userPermissions;
        }

        public UserPermission GetPermission (string type, int number)
        {
            PermissionType permissionType = db.PermissionTypes.First(u => (u.Item == type && u.Mode == number));
            UserPermission userPermission = userPermissions.First(u => (u.UserID == userView.user.UserID && u.PermissionTypeID == permissionType.PermissionTypeID));

            return userPermission;
        }

        public void CreatePermission (string type, int number)
        {
            Information["changes"] ++;
            PermissionType permissionType = db.PermissionTypes.First(u => (u.Item == type && u.Mode == number));
            UserPermission userPermission = new UserPermission
            {
                UserID = userView.user.UserID,
                PermissionTypeID = permissionType.PermissionTypeID,
                permissionType = permissionType,
            };
            userPermissions.Add(userPermission);
            MessageBox.Show($"Permisos para usuario {userView.user.UserID} = {userPermissions.Where(u => (u.UserID == userView.user.UserID)).ToList().Count.ToString()}");
        }

        public void DeletePermission (string type, int number)
        {
            Information["changes"]++;
            PermissionType permissionType = db.PermissionTypes.First(u => (u.Item == type && u.Mode == number));
            UserPermission userPermission = userPermissions.First(u => (u.UserID == userView.user.UserID && u.PermissionTypeID == permissionType.PermissionTypeID));
            userPermissions.Remove(userPermission);
            MessageBox.Show($"Permisos para usuario {userView.user.UserID} = {userPermissions.Where(u => (u.UserID == userView.user.UserID)).ToList().Count.ToString()}");
        }

        public void BackToMain()
        {
            Information["controller"] = 0;
            ChangeComponents();
        }

        private void UpdateComponents ()
        {
            MainContent.Content = MainContentUser;
            LeftSide.Content = ToolSideUser;
            TopSide.Content = NavigationUser;
        }

        public void DisableUserEvent()
        {
            GestCloudDB db = new GestCloudDB();
            
            MessageBoxResult result = MessageBox.Show("Si usted desactiva el usuario no podre acceder a él,¿Desea desactivarlo?", "Desactivar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                userView.user.Enabled = 0;
                //Information["changes"]++;
                db.Users.Update(userView.user);
                db.SaveChanges();
                MessageBox.Show("Usuario desactivado");
            }           
        }

        public void ChangeEditable (int i)
        {
            Information["old_editable"] = Information["editable"];
            Information["editable"] = i;
            ChangeEnviroment();
        }

        public void ChangeMode(int i)
        {
            Information["mode"] = i;
            ChangeEnviroment();
        }

        public void ChangePermission(int i)
        {
            Information["old_permission"] = Information["permission"];
            Information["permission"] = i;
            ChangeEnviroment();
        }

        public void SaveChanges()
        {
            List<UserPermission> temp = db.UserPermissions.Where(u => (u.UserID == userView.user.UserID)).Include(u => u.permissionType).ToList();
            foreach (UserPermission up in userPermissions)
            {
                if(temp.Where(u=> (u.UserID == up.UserID && u.PermissionTypeID == up.PermissionTypeID)).ToList().Count == 0)
                {
                    db.Add(up);
                }
            }

            foreach (UserPermission up in temp)
            {
                if (userPermissions.Where(u => (u.UserID == up.UserID && u.PermissionTypeID == up.PermissionTypeID)).ToList().Count == 0)
                {
                    db.Remove(up);
                }
            }
            db.Users.Update(userView.user);
            db.SaveChanges();
            Information["changes"] = 0;
            MessageBox.Show("Se han guardado los cambios");
        }

        public void ChangeEnviroment()
        {
            switch(Information["mode"])
            {
                case 0:
                    MainContentUser = new InfoUser_MainContent();
                    ToolSideUser = new InfoUser_ToolSide();
                    NavigationUser = new InfoUser_Navigation();
                    UpdateComponents();
                    break;

                case 1:
                    MainContentUser = new UserItem.InfoUser.AccessUser_MainContent();
                    ToolSideUser = new UserItem.InfoUser.AccessUser_ToolSide();
                    NavigationUser = new InfoUser_Navigation();
                    UpdateComponents();
                    break;

                case 2:
                    ToolSideUser = new UserItem.InfoUser.PermissionUser_ToolSide();
                    NavigationUser = new InfoUser_Navigation();
                    switch(Information["permission"])
                    {
                        case 0:
                            MainContentUser = new UserItem.InfoUser.PermissionUser_MainContent();
                            break;

                        case 1:
                            MainContentUser = new UserItem.InfoUser.Permissions.UsersPermissionUser_MainContent();
                            break;

                        case 2:
                            MainContentUser = new UserItem.InfoUser.Permissions.CardsPermissionUser_MainContent();
                            break;
                    }
                    UpdateComponents();
                    break;
            }
        }

        private void ChangeComponents()
        {
            switch (Information["controller"])
            {
                case 0:
                    if (Information["changes"] > 0)
                    {
                        MessageBoxResult result = MessageBox.Show("Usted ha realizado cambios, ¿Esta seguro que desea salir?", "Volver", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }
                    }

                    MainWindow a = (MainWindow)Application.Current.MainWindow;
                    a.MainPage.Content = new Main.Main_Controller();
                    break;
            }
        }
    }
}
