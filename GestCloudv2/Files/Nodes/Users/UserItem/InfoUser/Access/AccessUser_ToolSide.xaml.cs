using FrameworkDB.V1;
using FrameworkView.V1;
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

namespace GestCloudv2.UserItem.InfoUser
{
    /// <summary>
    /// Interaction logic for AccessUser_ToolSide.xaml
    /// </summary>
    public partial class AccessUser_ToolSide : Page
    {

        public AccessUser_ToolSide()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(StartEvent);
        }

        public void StartEvent(object sender, RoutedEventArgs e)
        {
            if (GetController().userView.user.Enabled == 1)
            {
                BT_DisableUser.Visibility = Visibility.Visible;
                BT_EnableUser.Visibility = Visibility.Hidden;
            }
            else
            {
                BT_EnableUser.Visibility = Visibility.Visible;
                BT_DisableUser.Visibility = Visibility.Hidden;
            }
        }

        private void EV_DisableUser(object sender, RoutedEventArgs e)
        {
            GetController().DisableUserEvent();
        }

        private void EV_EnableUser(object sender, RoutedEventArgs e)
        {
            GetController().EnableUserEvent();
        }

        private void EV_ResetPassword(object sender, RoutedEventArgs e)
        {
            GetController().RestorePassword();
        }

        private InfoUser.InfoUser_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MC_Main)mainWindow;
            return (InfoUser.InfoUser_Controller)a.MainPage.Content;
        }
    }
}
