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

namespace GestCloudv2.Documents.DCM_Menu.View
{
    /// <summary>
    /// Interaction logic for TS_DCM_Menu.xaml
    /// </summary>
    public partial class TS_DCM_Menu : Page
    {
        public TS_DCM_Menu()
        {
            InitializeComponent();
            if (GetController().SelectedItem())
            {
                BT_Load.IsEnabled = true;
                BT_LoadEditable.IsEnabled = true;
            }
        }

        private void EV_MD_New(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_New();
        }

        private void EV_MD_Load(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_Load();
        }

        private void EV_MD_LoadEditable(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_LoadEditable();
        }

        private void EV_MD_DeliveryTransfer(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_Transfer(1);
        }

        private void EV_MD_InvoiceTransfer(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_Transfer(2);
        }

        virtual public Controller.CT_DCM_Menu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_DCM_Menu)a.MainFrame.Content;
        }
    }
}
