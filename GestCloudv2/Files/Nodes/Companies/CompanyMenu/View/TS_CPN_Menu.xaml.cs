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

namespace GestCloudv2.Files.Nodes.Companies.CompanyMenu.View
{
    /// <summary>
    /// Interaction logic for TS_CPN_Menu.xaml
    /// </summary>
    public partial class TS_CPN_Menu : Page
    {
        public TS_CPN_Menu()
        {
            InitializeComponent();

            if(GetController().company != null)
            {
                BT_CompanyLoad.IsEnabled = true;
                BT_CompanyLoadEdit.IsEnabled = true;
            }
        }

        private void EV_CT_CompanyNew(object sender, RoutedEventArgs e)
        {
            GetController().CT_CompanyNew();
        }

        private void EV_CT_CompanyLoad(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_CompanyLoad();
        }

        private void EV_CT_CompanyLoadEditable(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_CompanyLoadEditable();
        }

        private Controller.CT_CompanyMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CompanyMenu)a.MainFrame.Content;
        }
    }
}
