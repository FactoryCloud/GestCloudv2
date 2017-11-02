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
using System.Data;
using FrameworkView.V1;

namespace GestCloudv2.Files.Nodes.Users.UserMenu.View
{
    /// <summary>
    /// Interaction logic for UserList_MainContent.xaml
    /// </summary>
    public partial class MC_USR_Menu : Page
    {
        public MC_USR_Menu()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);
            NameSearchBox.KeyUp += new KeyEventHandler(Data_Search);
            CodeSearchBox.KeyUp += new KeyEventHandler(Data_SearchCod);
            UserSeachBox.KeyUp += new KeyEventHandler(Data_SearchUserName);
            UsersTable.MouseDoubleClick += new MouseButtonEventHandler(UserInfo_Event);
            UsersTable.MouseLeftButtonUp += new MouseButtonEventHandler(UserSelected_Event);
            UpdateData();
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {

        }

        private void UserSelected_Event(object sender, MouseButtonEventArgs e)
        {
            SelectedUserUpdate();
        }

        private void UserInfo_Event(object sender, MouseButtonEventArgs e)
        {
            SelectedUserUpdate();
            UserInfoLoad();
        }

        private void Data_Search(object sender, RoutedEventArgs e)
        {
            GetController().UsersView.userSearch.FirstName = NameSearchBox.Text;
            GetController().UsersView.userSearch.LastName = NameSearchBox.Text;
            SearchData();
        }

        private void Data_SearchUserName(object sender, RoutedEventArgs e)
        {
            GetController().UsersView.userSearch.Username = UserSeachBox.Text;
            SearchDataUserName();
        }

        private void Data_SearchCod(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CodeSearchBox.Text))
            {
                UpdateData();
            }
            else
            {
                GetController().UsersView.userSearch.UserID = int.Parse(CodeSearchBox.Text);
                SearchDataCod();
            }
        }

        public void UpdateData()
        {
            UsersTable.ItemsSource = null;
            UsersTable.ItemsSource = GetController().UsersView.GetTable();
        }

        public void SearchData()
        {
            UsersTable.ItemsSource = null;
            UsersTable.ItemsSource = GetController().UsersView.GetTableFiltered();
        }

        public void SearchDataUserName()
        {
            UsersTable.ItemsSource = null;
            UsersTable.ItemsSource = GetController().UsersView.GetTableFilteredUserName();
        }

        public void SearchDataCod()
        {
            UsersTable.ItemsSource = null;
            UsersTable.ItemsSource = GetController().UsersView.GetTableFilteredCod();
        }

        public void SelectedUserUpdate()
        {
            int user = UsersTable.SelectedIndex;
            if (user >= 0)
            {
                DataGridRow row = (DataGridRow)UsersTable.ItemContainerGenerator.ContainerFromIndex(user);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetUser(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        public void UserInfoLoad()
        {
            int user = UsersTable.SelectedIndex;
            if (user >= 0)
            {
                //GetController().StartViewUser();
            }
        }

        private Files.Nodes.Users.UserMenu.Controller.CT_UserMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Files.Nodes.Users.UserMenu.Controller.CT_UserMenu)a.MainFrame.Content;
        }
    }
}
