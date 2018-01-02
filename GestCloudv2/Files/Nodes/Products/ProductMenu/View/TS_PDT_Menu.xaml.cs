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

namespace GestCloudv2.Files.Nodes.Products.ProductMenu.View
{
    /// <summary>
    /// Interaction logic for TS_STR_Menu.xaml
    /// </summary>
    public partial class TS_PDT_Menu : Page
    {
        public TS_PDT_Menu()
        {
            InitializeComponent();

            if (GetController().product != null)
            {
                BT_ProductLoad.IsEnabled = true;
                BT_ProductLoadEdit.IsEnabled = true;
            }
        }

        private void EV_ProductNew(object sender, RoutedEventArgs e)
        {
            GetController().CT_ProductNew();
        }

        private void EV_CT_ProductLoad(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_ProductLoad();
        }

        private void EV_CT_ProductLoadEditable(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_ProductLoadEditable();
        }

        private Files.Nodes.Products.ProductMenu.Controller.CT_ProductMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Files.Nodes.Products.ProductMenu.Controller.CT_ProductMenu)a.MainFrame.Content;
        }
    }
}
