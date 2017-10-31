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
            this.Loaded += new RoutedEventHandler(StartEvent);
        }

        private void StartEvent(object sender, RoutedEventArgs e)
        {
            if(GetController().Information["selectedUser"]==1)
            {
                ViewUserButton.IsEnabled = true;
                EditUserButton.IsEnabled = true;
            }
        }

        private void NewUserEvent(object sender, RoutedEventArgs e)
        {
            //GetController().StartNewUser();
        }

        private void EditUserEvent(object sender, RoutedEventArgs e)
        {
            //GetController().StartEditUser();
        }

        private void ViewUserEvent(object sender, RoutedEventArgs e)
        {
            //GetController().StartViewUser();
        }

        private Main.Controller.CT_Main GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Main.Controller.CT_Main)a.MainFrame.Content;
        } 
    }
}
