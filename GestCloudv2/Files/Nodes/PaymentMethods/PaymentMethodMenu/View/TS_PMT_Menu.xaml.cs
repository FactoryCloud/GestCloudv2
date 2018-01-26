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

namespace GestCloudv2.Files.Nodes.PaymentMethods.PaymentMethodMenu.View
{
    /// <summary>
    /// Interaction logic for TS_STR_Menu.xaml
    /// </summary>
    public partial class TS_PMT_Menu : Page
    {
        public TS_PMT_Menu()
        {
            InitializeComponent();

            if (GetController().paymentMethod != null)
            {
                BT_PaymentMethodLoad.IsEnabled = true;
                BT_PaymentMethodLoadEdit.IsEnabled = true;
            }
        }

        private void EV_PaymentMethodNew(object sender, RoutedEventArgs e)
        {
            GetController().CT_PaymentMethodNew();
        }

        private void EV_CT_PaymentMethodLoad(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_PaymentMethodLoad();
        }

        private void EV_CT_PaymentMethodLoadEditable(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_PaymentMethodLoadEditable();
        }

        private Files.Nodes.PaymentMethods.PaymentMethodMenu.Controller.CT_PaymentMethodMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Files.Nodes.PaymentMethods.PaymentMethodMenu.Controller.CT_PaymentMethodMenu)a.MainFrame.Content;
        }
    }
}
