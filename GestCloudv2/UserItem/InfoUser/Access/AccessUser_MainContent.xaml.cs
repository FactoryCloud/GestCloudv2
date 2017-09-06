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
using FrameworkView.V1;

namespace GestCloudv2.UserItem.InfoUser
{
    /// <summary>
    /// Interaction logic for AccessUser_MainContent.xaml
    /// </summary>
    public partial class AccessUser_MainContent : Page
    {
        UsersAccessControlView usersControl;

        public AccessUser_MainContent()
        {
            usersControl = new UsersAccessControlView();
            InitializeComponent();
            UpdateDataAccess();
        }

        public void UpdateDataAccess()
        {
            AccessUserTable.ItemsSource = null;
            AccessUserTable.ItemsSource = usersControl.GetTableAccess();
        }

        private void AccessUserTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
