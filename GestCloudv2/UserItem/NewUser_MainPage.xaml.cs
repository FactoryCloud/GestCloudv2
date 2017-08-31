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

namespace GestCloudv2.UserItem
{
    /// <summary>
    /// Interaction logic for NewUser_MainPage.xaml
    /// </summary>
    public partial class NewUser_MainPage : Page
    {
        GestCloudDB db;

        public NewUser_MainPage()
        {
            InitializeComponent();
        }

        public void SaveUser()
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

            Window main = Application.Current.MainWindow;
            var a = (MainWindow)main;
            a.changeLeftSide(new UserList_ToolSide());
            a.changeMainContent(new UserList_MainContent());
        }
    }
}
