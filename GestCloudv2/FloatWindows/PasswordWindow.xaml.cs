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
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace GestCloudv2.FloatWindows
{
    /// <summary>
    /// Interaction logic for GestCloudv2.FloatWindows.PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        string username;
        UserAccessControl uac;

        public PasswordWindow(string username, UserAccessControl uac)
        {
            InitializeComponent();
            this.username = username;
            this.uac = uac;
            this.WindowStyle = WindowStyle.ToolWindow;
            this.Loaded += new RoutedEventHandler(EV_Start);
            TB_Password.KeyUp += new KeyEventHandler(KeyPushDetected_Event);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            GestCloudDB db = new GestCloudDB();
        }

        private void KeyPushDetected_Event(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(TB_Password.Password))
            {
                BT_Enter.IsEnabled = true;
            }

            else
            {
                BT_Enter.IsEnabled = false;
            }
        }

        private void EV_PasswordEnter(object sender, RoutedEventArgs e)
        {
            GestCloudDB db = new GestCloudDB();
            List<User> users = db.Users.Include(u => u.entity).ToList();
            string advice = "Los datos son incorrectos";

            List<AccessType> accessTypes = db.AccessTypes.Where(a => a.Name == "WindowsApp_Password").ToList();

            foreach (User u in users)
            {
                if (u.Enabled == 0)
                {
                    MessageBox.Show("Este usuario esta desactivado");
                }

                else
                {
                    if (u.Username == username && u.Password == TB_Password.Password)
                    {
                        if (u.ActivationCode != null)
                        {
                            advice = "Este usuario tiene un código de activación, no se puede iniciar sesión mediante la contraseña actual.";
                        }

                        else
                        {
                            UserAccessControl temp = db.UsersAccessControl.Where(uact => uact.UserAccessControlID == uac.UserAccessControlID).First();
                            temp.DateEndAccess = DateTime.Now;
                            db.UsersAccessControl.Update(temp);
                            db.SaveChanges();

                            UserAccessControl temp2 = new UserAccessControl
                            {
                                user = u,
                                accessType = accessTypes[0],
                                DateStartAccess = DateTime.Now,
                            };
                            db.UsersAccessControl.Add(temp2);
                            db.SaveChanges();

                            Window mainWindow = Application.Current.MainWindow;
                            var a = (Main.View.MainWindow)mainWindow;
                            a.selectedUser = u;
                            a.uac = temp2;
                            a.userPermissions = db.UserPermissions.Where(p => p.UserID == u.UserID).ToList();
                            a.InitializingUser();

                            this.Close();
                            return;
                        }
                    }
                }
            }
            MessageBoxResult result = MessageBox.Show(advice);        
        }
    }
}
