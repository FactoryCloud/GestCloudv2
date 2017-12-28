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
        int userID;
        UserAccessControl uac;

        public PasswordWindow(int userID, UserAccessControl uac)
        {
            InitializeComponent();
            this.userID = userID;
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
            User user = db.Users.Where(u => u.UserID == userID).Include(u => u.entity).First();
            string advice = "Los datos son incorrectos";

            AccessType accessType = db.AccessTypes.Where(a => a.Name == "WindowsApp_Password").First();

            if (user.Enabled == 0)
            {
                MessageBox.Show("Este usuario esta desactivado");
            }

            else
            {
                if (user.Password == TB_Password.Password)
                {
                    if (user.ActivationCode != null)
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
                            user = user,
                            accessType = accessType,
                            DateStartAccess = DateTime.Now,
                        };
                        db.UsersAccessControl.Add(temp2);
                        db.SaveChanges();

                        ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).SetUserSelected(user.UserID);
                        ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).SetUserAccessControl(temp2);
                        ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).SetDefaultCompany();
                        GetController().EV_UserSelected();

                        this.Close();
                        return;
                    }
                }
            }
            MessageBoxResult result = MessageBox.Show(advice);        
        }

        virtual public Main.Controller.CT_Common GetController()
        {
            return new Main.Controller.CT_Common();
        }
    }
}
