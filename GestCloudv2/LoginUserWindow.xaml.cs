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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GestCloudDB db = new GestCloudDB();
            var query =  db.Users.Select(u => new {u.Username, u.Password,u.UserID});
            
            foreach (var u in query)
            {
                if (u.Username == UserNameText.Text && u.Password == PasswordText.Password)
                {
                   
                    UserMantenantWindow userMantenant = new UserMantenantWindow();
                    userMantenant.Show();
                    this.Close();
                    return;
                }
            }
            MessageBoxResult result = MessageBox.Show("Los datos son incorrectos");
        }
    }
}
