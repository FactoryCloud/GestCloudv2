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

namespace GestCloudv2.UserItem
{
    /// <summary>
    /// Interaction logic for NewUser_ToolSide.xaml
    /// </summary>
    public partial class NewUser_ToolSide : Page
    {
        public NewUser_ToolSide()
        {
            InitializeComponent();
        }

        private void SaveUserEvent(object sender, RoutedEventArgs e)
        {
            GetController().SaveNewUser();
            GetController().Information["fieldEmpty"] = 0;
            GetController().BackToMain();
        }

        public void EnableButtonSaveUser(bool enable)
        {
            if (enable)
            {
                SaveButton.IsEnabled = true;
            }
            else
            {
                SaveButton.IsEnabled = false;
            }
        }

        private UserItem.NewUser.NewUser_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (UserItem.NewUser.NewUser_Controller)a.MainPage.Content;
        }
    }
}
