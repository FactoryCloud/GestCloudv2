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
        public UserList_MainContent()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(StartEvent);
            userView = new UsersView();
            NameSearchBox.KeyUp += new KeyEventHandler(Data_Search);
            CodeSearchBox.KeyUp += new KeyEventHandler(Data_SearchCod);
            UserSeachBox.KeyUp += new KeyEventHandler(Data_SearchUserName);
            UsersTable.MouseDoubleClick += new MouseButtonEventHandler(UserInfo_Event);
            UsersTable.MouseLeftButtonUp += new MouseButtonEventHandler(UserSelected_Event);
            UpdateData();
        }

        private void StartEvent(object sender, RoutedEventArgs e)
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

        public void SelectedUserUpdate()
        {
            int user = UsersTable.SelectedIndex;
            if (user >= 0)
            {
                DataGridRow row = (DataGridRow)UsersTable.ItemContainerGenerator.ContainerFromIndex(user);
                DataRowView dr = row.Item as DataRowView;
                //GetController().UpdateUserSelected(Int32.Parse(dr.Row.ItemArray[0].ToString()));
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

        private Main.Controller.CT_Main GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Main.Controller.CT_Main)a.MainFrame.Content;
        }
    }
}
