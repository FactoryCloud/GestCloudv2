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
using Microsoft.EntityFrameworkCore;


namespace GestCloudv2
{
    /// <summary>
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public sealed partial class NewUserWindow : Window
    {
        GestCloudDB db;

        public NewUserWindow()
        {
            this.InitializeComponent();
        }

        private void SaveUser(object sender, RoutedEventArgs e)
        {
            using (db = new GestCloudDB())
            {
                var newUser = new User()
                {
                    FirstName = firsnameText.Text,
                    LastName = lastnameText.Text,
                    Username = usernameText.Text,
                    Password = passwordText.Password
                };
                db.Users.Add(newUser);
                db.SaveChanges();
            }
            MessageBoxResult result = MessageBox.Show("Datos guardados correctamente");
            firsnameText.Text = "";
            lastnameText.Text = "";
            usernameText.Text = "";
            passwordText.Password = "";
        }

        private void BacktoMenu(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
