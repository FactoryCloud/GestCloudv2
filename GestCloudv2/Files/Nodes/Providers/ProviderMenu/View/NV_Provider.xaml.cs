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
    /// Interaction logic for NV_Client.xaml
    /// </summary>
    public partial class NV_Provider : Page
    {
        public NV_Provider()
        {
            InitializeComponent();
        }

        private void EV_BackEvent(object sender, RoutedEventArgs e)
        {
            GetController().CT_Main();
        }

        private Providers.ProviderMenu.Controller.CT_ProviderMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Providers.ProviderMenu.Controller.CT_ProviderMenu)a.MainFrame.Content;
        }
    }
}
