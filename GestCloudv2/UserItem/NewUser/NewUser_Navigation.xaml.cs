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
using GestCloudv2;
using FrameworkDB.V1;

namespace GestCloudv2.UserItem
{
    /// <summary>
    /// Interaction logic for NewUser_Navigation.xaml
    /// </summary>
    public partial class NewUser_Navigation : Page
    {
        public NewUser_Navigation()
        {
            InitializeComponent();
        }

        private void BackUserListEvent(object sender, RoutedEventArgs e)
        {
            Window main = Application.Current.MainWindow;
            var a = (MainWindow)main;
            var b = (UserItem.NewUser.NewUser_Controller)a.MainPage.Content;
            var c = (NewUser_MainPage)b.MainContent.Content;
            c.BackUserList();
        }
    }
}
