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
    /// Interaction logic for LoginUserWindow.xaml
    /// </summary>
    public partial class LoginUserWindow : Window
    {
        int mode;

        public LoginUserWindow()
        {
            InitializeComponent();
            mode = 0;
            this.WindowStyle = WindowStyle.ToolWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GestCloudDB db = new GestCloudDB();
            List<User> users = db.Users.ToList();

            if (mode == 0)
            {
                List<AccessType> accessTypes = db.AccessTypes.Where(a => a.Name == "WindowsApp_Password").ToList();

                foreach (User u in users)
                {
                    if (u.Username == UserNameText.Text && u.Password == PasswordText.Password)
                    {
                        UserAccessControl accessControl = new UserAccessControl
                        {
                            user = u,
                            accessType = accessTypes[0],
                            DateStartAccess = DateTime.Now,
                            DateEndAccess = DateTime.Now
                        };
                        db.UsersAccessControl.Add(accessControl);
                        db.SaveChanges();
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                        return;
                    }
                }
                MessageBoxResult result = MessageBox.Show("Los datos son incorrectos");
            }

            else if(mode == 1)
            {
                List<AccessType> accessTypes = db.AccessTypes.Where(a => a.Name == "WindowsApp_Code").ToList();
                foreach (User u in users)
                {
                    if (u.ActivationCode == CodeText.Password)
                    {
                        UserAccessControl accessControl = new UserAccessControl
                        {
                            user = u,
                            accessType = accessTypes[0],
                            DateStartAccess = DateTime.Now,
                            DateEndAccess = DateTime.Now
                        };
                        db.UsersAccessControl.Add(accessControl);
                        //u.ActivationCode = null;
                        //db.UpdateRange(users);
                        db.SaveChanges();
                        ChangePasswordUserWindow mainWindow = new ChangePasswordUserWindow(u);
                        mainWindow.Show();
                        this.Close();
                        return;
                    }
                }
                MessageBoxResult result = MessageBox.Show("Los datos son incorrectos");
            }
            
        }

        private void ActivateCode_Event(object sender, RoutedEventArgs e)
        {
            ChangeMode(1);
        }

        private void ActivateUser_Event(object sender, RoutedEventArgs e)
        {
            ChangeMode(0);
        }

        private void ChangeMode(int mode)
        {
            if(mode == 0)
            {
                this.mode = mode;
                ActivateCodeButton.Visibility = Visibility.Visible;
                UserNameLabel.Visibility = Visibility.Visible;
                UserNameText.Visibility = Visibility.Visible;
                PasswordLabel.Visibility = Visibility.Visible;
                PasswordText.Visibility = Visibility.Visible;

                ActivateUserButton.Visibility = Visibility.Hidden;
                CodeLabel.Visibility = Visibility.Hidden;
                CodeText.Visibility = Visibility.Hidden;
            }

            else if(mode == 1)
            {
                this.mode = mode;
                ActivateCodeButton.Visibility = Visibility.Hidden;
                UserNameLabel.Visibility = Visibility.Hidden;
                UserNameText.Visibility = Visibility.Hidden;
                PasswordLabel.Visibility = Visibility.Hidden;
                PasswordText.Visibility = Visibility.Hidden;

                ActivateUserButton.Visibility = Visibility.Visible;
                CodeLabel.Visibility = Visibility.Visible;
                CodeText.Visibility = Visibility.Visible;
            }
        }
    }
}
