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
        public LoginUserWindow()
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.ToolWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GestCloudDB db = new GestCloudDB();
            List<User> users = db.Users.ToList();
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
                    MessageBox.Show(accessControl.UserAccessControlID.ToString());
                    db.UsersAccessControl.Add(accessControl);
                    db.SaveChanges();
                    MainWindow userMantenant = new MainWindow();
                    userMantenant.Show();
                    this.Close();
                    return;
                }
            }
            MessageBoxResult result = MessageBox.Show("Los datos son incorrectos");
        }
    }
}
