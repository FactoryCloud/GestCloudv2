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

namespace GestCloudv2.Files.Nodes.Users.UserMenu.View
{
    /// <summary>
    /// Interaction logic for UserList_ToolSide.xaml
    /// </summary>
    public partial class TS_USR_Menu : Page
    {
        public TS_USR_Menu()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            if(GetController().User != null)
            {
                ViewUserButton.IsEnabled = true;
                EditUserButton.IsEnabled = true;
            }
        }

        private void EV_CT_UserNew(object sender, RoutedEventArgs e)
        {
            GetController().CT_UserNew();
        }

        private void EditUserEvent(object sender, RoutedEventArgs e)
        {
            //GetController().StartEditUser();
        }

        private void ViewUserEvent(object sender, RoutedEventArgs e)
        {
            //GetController().StartViewUser();
        }

        private Files.Nodes.Users.UserMenu.Controller.CT_UserMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Files.Nodes.Users.UserMenu.Controller.CT_UserMenu)a.MainFrame.Content;
        }
    }
}
