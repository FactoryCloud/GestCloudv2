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

namespace GestCloudv2
{
    /// <summary>
    /// Interaction logic for UserList_MainContent.xaml
    /// </summary>
    public partial class UserList_MainContent : Page
    {
        UsersView userView;
        NewUserWindow newUserWindow;
        public UserList_MainContent()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            userView = new UsersView();
            NameSearchBox.KeyUp += new KeyEventHandler(Data_Search);
            CodeSearchBox.KeyUp += new KeyEventHandler(Data_SearchCod);
            UserSeachBox.KeyUp += new KeyEventHandler(Data_SearchUserName);
            UsersTable.MouseDoubleClick += new MouseButtonEventHandler(UserInfo_Event);
            UsersTable.MouseLeftButtonUp += new MouseButtonEventHandler(UserSelected_Event);
            UpdateData();
        }

        private void UserSelected_Event(object sender, MouseButtonEventArgs e)
        {
            SelectedUserUpdate();
        }

        private void UserInfo_Event(object sender, MouseButtonEventArgs e)
        {
            SelectedUserUpdate();
            UserInfoLoad();
            //MessageBox.Show(userView.SelectedUser.UserID.ToString());
        }

        private void Data_Search(object sender, RoutedEventArgs e)
        {
            userView.userSearch.FirstName = NameSearchBox.Text;
            userView.userSearch.LastName = NameSearchBox.Text;
            SearchData();
        }

        private void Data_SearchUserName(object sender, RoutedEventArgs e)
        {
            userView.userSearch.Username = UserSeachBox.Text;
            SearchDataUserName();
        }

        private void Data_SearchCod(object sender, RoutedEventArgs e)
        {
            //string userID = userView.userSearch.UserID.ToString();
            if (string.IsNullOrWhiteSpace(CodeSearchBox.Text))
            {
                UpdateData();
            }
            else
            {
                userView.userSearch.UserID = int.Parse(CodeSearchBox.Text);
                SearchDataCod();
            }
        }

        public void UpdateData()
        {
            UsersTable.ItemsSource = null;
            UsersTable.ItemsSource = userView.GetTable();
        }

        public void SearchData()
        {
            UsersTable.ItemsSource = null;
            UsersTable.ItemsSource = userView.GetTableFiltered();
        }

        public void SearchDataUserName()
        {
            UsersTable.ItemsSource = null;
            UsersTable.ItemsSource = userView.GetTableFilteredUserName();
        }

        public void SearchDataCod()
        {
            UsersTable.ItemsSource = null;
            UsersTable.ItemsSource = userView.GetTableFilteredCod();
        }

        private void MainWindow_Loaded(object sender, EventArgs e)
        {
            newUserWindow = new NewUserWindow();
            newUserWindow.UpdateDataEvent += new EventHandler(NewUserWindow_MyEvent);
        }

        private void NewUserWindow_MyEvent(object sender, EventArgs e)
        {
            UpdateData();
        }

        public void SelectedUserUpdate()
        {
            int user = UsersTable.SelectedIndex;
            if (user >= 0)
            {
                DataGridRow row = (DataGridRow)UsersTable.ItemContainerGenerator.ContainerFromIndex(user);
                DataRowView dr = row.Item as DataRowView;
                userView.UpdateUserSelected(Int32.Parse(dr.Row.ItemArray[0].ToString()));
                Window mainWindow = Application.Current.MainWindow;
                var a = (MainWindow)mainWindow;
                var b = (UserList_ToolSide)a.LeftSide.Content;
                b.EditUserButton.IsEnabled = true;
                b.DeleteUserButton.IsEnabled = true;
            }
        }

        public void UserInfoLoad()
        {
            int user = UsersTable.SelectedIndex;
            if (user >= 0)
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = (MainWindow)mainWindow;
                a.changeMainContent(new UserItem.InfoUser_MainContent(userView.SelectedUser, false));
                a.changeLeftSide(new UserItem.InfoUser_ToolSide(false));
                a.changeTopSide(new UserItem.InfoUser_Navigation());
            }
        }

        public void UserEditLoad()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            a.ChangeContent(new UserItem.InfoUser_MainContent(userView.SelectedUser, true), new UserItem.InfoUser_Navigation(), new UserItem.InfoUser_ToolSide(true));
        }
    }
}
