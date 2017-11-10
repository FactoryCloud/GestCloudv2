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

namespace GestCloudv2.Files.Nodes.Providers.ProviderMenu.View
{
    /// <summary>
    /// Interaction logic for TS_Client.xaml
    /// </summary>
    public partial class TS_Provider : Page
    {
        public TS_Provider()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            if (GetController().provider != null)
            {
                BT_ProviderLoad.IsEnabled = true;
                BT_ProviderLoadEditable.IsEnabled = true;
            }
        }

        private void EV_CT_ProviderNew(object sender, RoutedEventArgs e)
        {
            GetController().CT_ProviderNew();
        }

        private void EV_CT_ProviderLoad(object sender, RoutedEventArgs e)
        {
            GetController().CT_ProviderLoad();
        }

        private void EV_CT_ProviderLoadEditable(object sender, RoutedEventArgs e)
        {
            GetController().CT_ProviderLoadEditable();
        }

        private ProviderMenu.Controller.CT_ProviderMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (ProviderMenu.Controller.CT_ProviderMenu)a.MainFrame.Content;
        }
    }
}
