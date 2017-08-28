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

namespace GestCloudv2
{
    /// <summary>
    /// Interaction logic for UserMantenantWindow.xaml
    /// </summary>
    public partial class UserMantenantWindow : Window
    {
        public UserMantenantWindow()
        {
            InitializeComponent();
        }

        private void NewUser(object sender, RoutedEventArgs e)
        {
            NewUserWindow newuserwindow = new NewUserWindow();
            newuserwindow.Show();
        }
    }
}