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

namespace GestCloudv2.Purchases.Nodes.PurchaseAdjust.PurchaseMenu.View
{ 
    /// <summary>
    /// Interaction logic for NV_STA_Menu.xaml
    /// </summary>
    public partial class NV_PUR_Menu : Page
    {
        public NV_PUR_Menu()
        {
            InitializeComponent();
        }

        private void EV_CT_Back(object sender, RoutedEventArgs e)
        {
            GetController().CT_Main();
        }

        private Controller.CT_PurchaseAdjustMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PurchaseAdjustMenu)a.MainFrame.Content;
        }
    }
}
