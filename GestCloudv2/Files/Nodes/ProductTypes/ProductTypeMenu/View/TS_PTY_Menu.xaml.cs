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

namespace GestCloudv2.Files.Nodes.ProductTypes.ProductTypeMenu.View
{
    /// <summary>
    /// Interaction logic for TS_STR_Menu.xaml
    /// </summary>
    public partial class TS_PTY_Menu : Page
    {
        public TS_PTY_Menu()
        {
            InitializeComponent();

            if (GetController().productType != null)
            {
                BT_ProductTypeLoad.IsEnabled = true;
                BT_ProductTypeLoadEdit.IsEnabled = true;
            }
        }

        private void EV_ProductTypeNew(object sender, RoutedEventArgs e)
        {
            GetController().CT_ProductTypeNew();
        }

        private void EV_CT_ProductTypeLoad(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_ProductTypeLoad();
        }

        private void EV_CT_ProductTypeLoadEditable(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_ProductTypeLoadEditable();
        }

        private Files.Nodes.ProductTypes.ProductTypeMenu.Controller.CT_ProductTypeMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Files.Nodes.ProductTypes.ProductTypeMenu.Controller.CT_ProductTypeMenu)a.MainFrame.Content;
        }
    }
}
