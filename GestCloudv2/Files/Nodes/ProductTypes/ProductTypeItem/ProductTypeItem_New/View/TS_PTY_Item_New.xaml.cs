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

namespace GestCloudv2.Files.Nodes.ProductTypes.ProductTypeItem.ProductTypeItem_New.View
{
    /// <summary>
    /// Interaction logic for TS_CPN_Item_New.xaml
    /// </summary>
    public partial class TS_PTY_Item_New : Page
    {
        public TS_PTY_Item_New(int num)
        {
            InitializeComponent();

            if(num >= 1)
            {
                BT_StoreSave.IsEnabled = true;
            }
        }

        private void EV_CompanySave(object sender, RoutedEventArgs e)
        {
            GetController().SaveNewStore();
        }

        private Controller.CT_PTY_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PTY_Item_New)a.MainFrame.Content;
        }
    }
}
