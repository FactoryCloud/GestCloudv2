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
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.Data;
using System.Collections;

namespace GestCloudv2.UserItem
{
    /// <summary>
    /// Interaction logic for NewUser_MainPage.xaml
    /// </summary>
    public partial class NewUser_MainPage : Page
    {
        private GestCloudDB db;
        private DataTable dt;

        public NewUser_MainPage()
        {
            InitializeComponent();
            dt = new DataTable();
            db = new GestCloudDB();
            this.Loaded += new RoutedEventHandler(StartNewUserMain_Event);
        }

        private void StartNewUserMain_Event(object sender, RoutedEventArgs e)
        {
            firstnameText.KeyUp += new KeyEventHandler(ControlFieldsKey_Event);
            lastnameText.KeyUp += new KeyEventHandler(ControlFieldsKey_Event);
            usernameText.KeyUp += new KeyEventHandler(ControlFieldsKey_Event);
        }

        private void ControlFieldsKey_Event(object sender, RoutedEventArgs e)
        {
            if (firstnameText.Text.Length <= 30 && lastnameText.Text.Length <= 30 && usernameText.Text.Length <= 20 && UserControlExist() == false)
            {
                GetController().ControlFieldChangeButton(true);
            }
            else
            {
                GetController().ControlFieldChangeButton(false);
            }

            if(!string.IsNullOrEmpty(firstnameText.Text.ToString()) || !string.IsNullOrEmpty(lastnameText.Text.ToString()) || !string.IsNullOrEmpty(usernameText.Text.ToString()))
            {
                GetController().UpdateIfNotEmpty(true);
            }

            else
            {
                GetController().UpdateIfNotEmpty(false);
            }
        }

        public void SaveUser()
        {
            if (firstnameText.Text.Length <= 30 && lastnameText.Text.Length <= 30 && usernameText.Text.Length <= 20 && UserControlExist() == false)
            {
                using (db = new GestCloudDB())
                {
                    var newUser = new User()
                    {
                        FirstName = firstnameText.Text,
                        LastName = lastnameText.Text,
                        Username = usernameText.Text,
                        Password = "NULL",
                        Mail = mailText.Text,
                        ActivationCode = "1"
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                }
                MessageBoxResult result = MessageBox.Show("Datos guardados correctamente");

                Window main = Application.Current.MainWindow;
                var a = (MainWindow)main;
            }         
        }

        private Boolean UserControlExist()
        {
            List<User> users = db.Users.ToList();
            foreach (var item in users)
            {
                if (item.Username.Contains(usernameText.Text))
                {
                    return true;
                }
            }
            return false;
        }

        private UserItem.NewUser.NewUser_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (UserItem.NewUser.NewUser_Controller)a.MainPage.Content;
        }
    }
}
