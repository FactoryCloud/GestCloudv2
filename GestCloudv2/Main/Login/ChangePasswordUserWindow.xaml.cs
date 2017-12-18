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
            User userTemp = db.Users.Where(u => u.UserID == user.UserID).First();
            List<AccessType> accessTypes = db.AccessTypes.Where(a => a.Name == "WindowsApp_Password").ToList();
            if (userTemp.Password == NewPasswordText.Password)
            {
                NewPasswordText.Password = "";
                MessageBox.Show("La contraseña debe ser diferente a la que usaba anteriormente.");
            }

            else
            {
                userTemp.Password = NewPasswordText.Password;
                userTemp.ActivationCode = null;
                db.Update(userTemp);

                UserAccessControl accessControl = new UserAccessControl
                {
                    user = userTemp,
                    accessType = accessTypes[0],
                    DateStartAccess = DateTime.Now,
                    DateEndAccess = DateTime.Now
                };
                db.UsersAccessControl.Add(accessControl);

                db.SaveChanges();
                Main.View.MainWindow mainWindow = new Main.View.MainWindow(user, accessControl);
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
