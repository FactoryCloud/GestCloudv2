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

namespace GestCloudv2.Files.Nodes.Clients.ClientMenu.View
{
    /// <summary>
    /// Interaction logic for TS_Client.xaml
    /// </summary>
    public partial class TS_Client : Page
    {
        public TS_Client()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            if (GetController().client != null)
            {
                BT_ClientLoad.IsEnabled = true;
                BT_ClientLoadEditable.IsEnabled = true;
            }
        }

        private void EV_CT_ClientNew(object sender, RoutedEventArgs e)
        {
            GetController().CT_ClientNew();
        }

        private void EV_CT_ClientLoad(object sender, RoutedEventArgs e)
        {
            GetController().CT_ClientLoad();
        }

        private void EV_CT_ClientLoadEditable(object sender, RoutedEventArgs e)
        {
            GetController().CT_ClientLoadEditable();
        }

        private ClientMenu.Controller.CT_ClientMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (ClientMenu.Controller.CT_ClientMenu)a.MainFrame.Content;
        }
    }
}
