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

        private void BackMainWindowEvent(object sender, RoutedEventArgs e)
        {
            GetController().BackToMain();
        }

        private UserItem.NewUser.NewUser_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (UserItem.NewUser.NewUser_Controller)a.MainFrame.Content;
        }
    }
}
