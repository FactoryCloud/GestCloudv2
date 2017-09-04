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
        }

        public void SaveUser()
        {
            if (firsnameText.Text.Length <= 30 && lastnameText.Text.Length <= 30 && usernameText.Text.Length <= 20 && passwordText.Password.Length <= 20 && UserControlExist() == false)
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
                //a.changeLeftSide(new UserList_ToolSide());
                //a.changeMainContent(new UserList_MainContent());
                //a.changeTopSide(new Main_Navigation());
            }
            else
            {
                if (UserControlExist() == true)
                {
                    MessageBox.Show("El usuario ya existe");
                }
                else
                {
                    MessageBox.Show("Los datos son incorrectos");
                }
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
        public void Data_Control()
        {
            if (string.IsNullOrWhiteSpace(firsnameText.Text) && string.IsNullOrWhiteSpace(lastnameText.Text) && string.IsNullOrWhiteSpace(usernameText.Text))
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = (MainWindow)mainWindow;
                //a.changeMainContent(new UserList_MainContent());
                //a.changeLeftSide(new UserList_ToolSide());
                //a.changeTopSide(new Main_Navigation());
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("¿Usted ha realizado cambios, esta segudo de que desea salir?", "Volver", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {

                }
                else
                {
                    Window mainWindow = Application.Current.MainWindow;
                    var a = (MainWindow)mainWindow;
                    //a.changeMainContent(new UserList_MainContent());
                    //a.changeLeftSide(new UserList_ToolSide());
                    //a.changeTopSide(new Main_Navigation());
                }
            }
        }
    }
}
