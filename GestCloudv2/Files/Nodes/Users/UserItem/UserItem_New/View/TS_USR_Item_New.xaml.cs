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

namespace GestCloudv2.Files.Nodes.Users.UserItem.UserItem_New.View
{
    /// <summary>
    /// Interaction logic for TS_USR_Item_New.xaml
    /// </summary>
    public partial class TS_USR_Item_New : Page
    {
        public TS_USR_Item_New()
        {
            InitializeComponent();
        }

        private void EV_UserSave(object sender, RoutedEventArgs e)
        {
            GetController().SaveNewUser();
            GetController().Information["fieldEmpty"] = 0;
            GetController().CT_Menu();
        }

        public void EnableButtonSaveUser(bool enable)
        {
            if (enable)
            {
                BT_UserSave.IsEnabled = true;
            }
            else
            {
                BT_UserSave.IsEnabled = false;
            }
        }

        private Files.Nodes.Users.UserItem.UserItem_New.Controller.CT_USR_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Files.Nodes.Users.UserItem.UserItem_New.Controller.CT_USR_Item_New)a.MainFrame.Content;
        }
    }
}
