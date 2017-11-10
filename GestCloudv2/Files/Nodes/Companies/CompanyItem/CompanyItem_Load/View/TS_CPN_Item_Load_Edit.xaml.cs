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

namespace GestCloudv2.Files.Nodes.Companies.CompanyItem.CompanyItem_Load.View
{
    /// <summary>
    /// Interaction logic for TS_CPN_Item_Load_Edit.xaml
    /// </summary>
    public partial class TS_CPN_Item_Load_Edit : Page
    {
        public TS_CPN_Item_Load_Edit(int num)
        {
            InitializeComponent();

            if(num >= 1)
            {
                BT_CompanySave.IsEnabled = true;
            }
        }

        private void EV_CompanySave(object sender, RoutedEventArgs e)
        {
            GetController().SaveNewCompany();
        }

        private Controller.CT_CPN_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CPN_Item_Load)a.MainFrame.Content;
        }
    }
}
