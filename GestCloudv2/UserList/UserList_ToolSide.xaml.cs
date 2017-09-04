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
            GetController().StartNewUser();
        }

        private void EditUserEvent(object sender, RoutedEventArgs e)
        {
            /*Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            var b = (Main.Main_Controller)a.MainPage.Content;*/
        }

        private Main.Main_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (Main.Main_Controller)a.MainPage.Content;
        }
    }
}
