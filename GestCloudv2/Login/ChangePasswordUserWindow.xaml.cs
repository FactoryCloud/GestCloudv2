using FrameworkDB.V1;
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

namespace GestCloudv2
{
    /// <summary>
    /// Interaction logic for ChangePasswordUserWindow.xaml
    /// </summary>
    public partial class ChangePasswordUserWindow : Window
    {
        User user;
        public ChangePasswordUserWindow(User user)
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.ToolWindow;
            NewPasswordText.KeyUp += new KeyEventHandler(KeyPushDetected_Event);
            this.user = user;
        }

        private void KeyPushDetected_Event(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(NewPasswordText.Password))
            {
                SaveNewPasswordButton.IsEnabled = true;
            }

            else
            {
                SaveNewPasswordButton.IsEnabled = false;
            }
        }

        private void PasswordChange_Event(object sender, RoutedEventArgs e)
        {
            GestCloudDB db = new GestCloudDB();
            List<User> users = db.Users.Where(u => u.UserID == user.UserID).ToList();
            if(users[0].Password == NewPasswordText.Password)
            {
                NewPasswordText.Password = "";
                MessageBox.Show("La contraseña debe ser diferente a la que usaba anteriormente.");
            }

            else
            {
                users[0].Password = NewPasswordText.Password;
                //u.ActivationCode = null;
                //db.UpdateRange(users);
                db.SaveChanges();
                MainWindow mainWindow = new MainWindow(user);
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
