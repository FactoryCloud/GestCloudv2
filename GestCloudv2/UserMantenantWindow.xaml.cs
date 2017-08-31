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
        ModifyUserWindow modifyUserWindow;
        public UserMantenantWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
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
            newuserwindow.UpdateDataEvent += new EventHandler(newuserwindow_MyEvent);
        }

        private void newuserwindow_MyEvent(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            int user = UsersTable.SelectedIndex;
            DataGridRow row = (DataGridRow)UsersTable.ItemContainerGenerator.ContainerFromIndex(user);
            DataRowView dr = row.Item as DataRowView;

            modifyUserWindow = new ModifyUserWindow(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            modifyUserWindow.UpdateDataEvent += new EventHandler(newuserwindow_MyEvent);
            modifyUserWindow.Show();
        }
    }
    
}