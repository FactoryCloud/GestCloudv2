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
using System.Data;

namespace GestCloudv2
{
    /// <summary>
    /// Interaction logic for UserMantenantWindow.xaml
    /// </summary>
    public partial class UserMantenantWindow : Window
    {
        public UserMantenantWindow()
        {
            InitializeComponent();
            GestCloudDB db = new GestCloudDB();
            List < User > users = db.Users.ToList();
            
            UsersTable.ItemsSource = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("Code", typeof(int));
            dt.Columns.Add("First Name", typeof(string));
            dt.Columns.Add("Last Name", typeof(string));
            dt.Columns.Add("Username", typeof(string));
            foreach (var item in users)
            {
                dt.Rows.Add(item.UserID, item.FirstName, item.LastName, item.Username);
            }
            UsersTable.ItemsSource = dt.DefaultView;
        }

        private void NewUser(object sender, RoutedEventArgs e)
        {
            NewUserWindow newuserwindow = new NewUserWindow();
            newuserwindow.Show();
        }
    }
}