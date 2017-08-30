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

namespace GestCloudv2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewUserEvent(object sender, RoutedEventArgs e)
        {
            NewUserWindow newUserWindow = new NewUserWindow();
            newUserWindow.Show();
            this.Close();
        }

        private void EditUserEvent(object sender, RoutedEventArgs e)
        {
            //ModifyUserWindow modifyUserWindow = new ModifyUserWindow();
            //modifyUserWindow.Show();
            this.Close();
        }

        private void DeleteUserEvent(object sender, RoutedEventArgs e)
        {
            DeleteUserWindow deleteUserWindows = new DeleteUserWindow();
            deleteUserWindows.Show();
            this.Close();
        }

        private void LoginUserEvent(object sender, RoutedEventArgs e)
        {
            LoginUserWindow loginUserWindow = new LoginUserWindow();
            loginUserWindow.Show();
        }
    }
}
