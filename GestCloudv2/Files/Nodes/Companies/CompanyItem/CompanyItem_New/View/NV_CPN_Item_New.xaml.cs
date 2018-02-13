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
using GestCloudv2;
using FrameworkDB.V1;

namespace GestCloudv2.Files.Nodes.Companies.CompanyItem.CompanyItem_New.View
{
    /// <summary>
    /// Interaction logic for NV_CPN_Item_New.xaml
    /// </summary>
    public partial class NV_CPN_Item_New : Page
    {
        public NV_CPN_Item_New()
        {
            InitializeComponent();
        }

        private void EV_MD_Company(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(1,0);
        }

        private void EV_MD_Stores(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(2,0);
        }

        private void EV_MD_Taxes(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(3,0);
        }

        private void EV_MD_PaymentMethods(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(4, 0);
        }

        private void EV_MD_Configuration(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(5, 0);
        }

        private void EV_CT_Menu(object sender, RoutedEventArgs e)
        {
            GetController().CT_Menu();
        }

        private Controller.CT_CPN_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CPN_Item_New)a.MainFrame.Content;
        }
    }
}
