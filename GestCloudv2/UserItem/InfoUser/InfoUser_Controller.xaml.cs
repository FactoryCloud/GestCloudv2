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

        public InfoUser_Controller(User user, bool editable)
        {
            InitializeComponent();
            Information = new Dictionary<string, int>();
            Information.Add("editable", Convert.ToInt32(editable));
            Information.Add("old_editable", 0);
            Information.Add("mode", 0);
            Information.Add("old_mode", 0);
            Information.Add("permission", 0);
            Information.Add("old_permission", 0);

            userView = new UserView(user);

            this.Loaded += new RoutedEventHandler(StartUserInfo_Event);
        }

        private void StartUserInfo_Event(object sender, RoutedEventArgs e)
        {
            MainContentUser = new InfoUser_MainContent();
            ToolSideUser = new InfoUser_ToolSide();
            NavigationUser = new InfoUser_Navigation();
            UpdateComponents();
        }

        private void UpdateComponents ()
        {
            MainContent.Content = MainContentUser;
            LeftSide.Content = ToolSideUser;
            TopSide.Content = NavigationUser;
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
    }
}
