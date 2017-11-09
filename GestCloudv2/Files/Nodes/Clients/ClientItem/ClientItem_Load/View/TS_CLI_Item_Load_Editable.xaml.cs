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

namespace GestCloudv2.Files.Nodes.Clients.ClientItem.ClientItem_Load.View
{
    /// <summary>
    /// Interaction logic for TS_USR_Item_Load_Editable.xaml
    /// </summary>
    public partial class TS_CLI_Item_Load_Editable : Page
    {
        public TS_CLI_Item_Load_Editable(int num)
        {
            InitializeComponent();

            if(num >= 1)
            {
                BT_ClientSave.IsEnabled = true;
            }
        }

        private void EV_ClientSave(object sender, RoutedEventArgs e)
        {
            GetController().SaveNewClient();
        }

        private Controller.CT_CLI_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CLI_Item_Load)a.MainFrame.Content;
        }
    }
}
