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

            UpdateData();
        }

        private void Data_Search(object sender, RoutedEventArgs e)
        {
            userView.userSearch.FirstName = NameSearchBox.Text;
            userView.userSearch.LastName = NameSearchBox.Text;
            SearchData();
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

        private void MainWindow_Loaded(object sender, EventArgs e)
        {
            newUserWindow = new NewUserWindow();
            newUserWindow.UpdateDataEvent += new EventHandler(newUserWindow_MyEvent);
        }

        private void newUserWindow_MyEvent(object sender, EventArgs e)
        {
            UpdateData();
        }
    }
}
