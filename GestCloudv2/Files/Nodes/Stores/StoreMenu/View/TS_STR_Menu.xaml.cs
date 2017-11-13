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

namespace GestCloudv2.Files.Nodes.Stores.StoreMenu.View
{
    /// <summary>
    /// Interaction logic for TS_STR_Menu.xaml
    /// </summary>
    public partial class TS_STR_Menu : Page
    {
        public TS_STR_Menu()
        {
            InitializeComponent();

            if (GetController().store != null)
            {
                BT_StoreLoad.IsEnabled = true;
                BT_StoreLoadEdit.IsEnabled = true;
            }
        }

        private void EV_StoreNew(object sender, RoutedEventArgs e)
        {
            GetController().CT_StoreNew();
        }

        private void EV_CT_StoreLoad(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_StoreLoad();
        }

        private void EV_CT_StoreLoadEditable(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_StoreLoadEditable();
        }

        private Files.Nodes.Stores.StoreMenu.Controller.CT_StoreMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Files.Nodes.Stores.StoreMenu.Controller.CT_StoreMenu)a.MainFrame.Content;
        }
    }
}
