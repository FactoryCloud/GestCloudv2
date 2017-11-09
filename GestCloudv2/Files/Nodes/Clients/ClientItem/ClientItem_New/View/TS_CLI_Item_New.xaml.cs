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

namespace GestCloudv2.Files.Nodes.Clients.ClientItem.ClientItem_New.View
{
    /// <summary>
    /// Interaction logic for TS_USR_Item_New.xaml
    /// </summary>
    public partial class TS_CLI_Item_New : Page
    {
        public TS_CLI_Item_New(int num)
        {
            InitializeComponent();
            if (num >= 1)
            {
                BT_ClientSave.IsEnabled = true;
            }
        }

        private void EV_UserSave(object sender, RoutedEventArgs e)
        {
            GetController().SaveNewClient();
            //GetController().Information["fieldEmpty"] = 0;
            //GetController().CT_Menu();
        }

        /*public void EnableButtonSaveUser(bool enable)
        {
            if (enable)
            {
                BT_ClientSave.IsEnabled = true;
            }
            else
            {
                BT_ClientSave.IsEnabled = false;
            }
        }*/

        private ClientItem_New.Controller.CT_CLI_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (ClientItem_New.Controller.CT_CLI_Item_New)a.MainFrame.Content;
        }
    }
}
