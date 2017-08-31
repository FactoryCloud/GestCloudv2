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

namespace GestCloudv2
{
    /// <summary>
    /// Interaction logic for UserList_ToolSide.xaml
    /// </summary>
    public partial class UserList_ToolSide : Page
    {
        public UserList_ToolSide()
        {
            InitializeComponent();
        }

        private void NewUserEvent(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            a.changeMainContent(new UserItem.NewUser_MainPage());
            a.changeLeftSide(new UserItem.NewUser_ToolSide());
            a.changeTopSide(new UserItem.NewUser_Navigation());
        }

        private void EditUserEvent(object sender, RoutedEventArgs e)
        {

        }
    }
}
