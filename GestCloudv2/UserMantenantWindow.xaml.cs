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
using System.Windows.Shapes;
using FrameworkDB.V1;
using System.Data;
using FrameworkView.V1;

namespace GestCloudv2
{
    /// <summary>
    /// Interaction logic for UserMantenantWindow.xaml
    /// </summary>
    public partial class UserMantenantWindow : Window
    {
        UsersView userView;
        NewUserWindow newuserwindow;
        public UserMantenantWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            userView = new UsersView();

            UpdateData();
        }

        private void NewUser(object sender, RoutedEventArgs e)
        {
            newuserwindow.Show();
        }

        public void UpdateData()
        {
            UsersTable.ItemsSource = null;
            UsersTable.ItemsSource = userView.GetTable();
        }

        private void MainWindow_Loaded(object sender, EventArgs e)
        {
            newuserwindow = new NewUserWindow();
            newuserwindow.MyEvent += new EventHandler(newuserwindow_MyEvent);
        }
        private void newuserwindow_MyEvent(object sender, EventArgs e)
        {
            UpdateData();
        }
    }
    
}