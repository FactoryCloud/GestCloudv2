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
using FrameworkDB.V1;

namespace GestCloudv2.Files.Nodes.ProductTypes.ProductTypeItem.ProductTypeItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_Load_Company.xaml
    /// </summary>
    public partial class MC_PTY_Item_Load_ProductType_Taxes : Page
    {
        int external;
        public MC_PTY_Item_Load_ProductType_Taxes(int external)
        {
            InitializeComponent();
            this.external = external;

            this.Loaded += new RoutedEventHandler(EV_Start);  
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            if (GetController().Information["editable"] == 0)
            {
                BT_SelectAll.Visibility = Visibility.Hidden;
                BT_SelectNone.Visibility = Visibility.Hidden;
            }
        }

        /*private void EV_MD_StoresAll(object sender, RoutedEventArgs e)
        {
            GetController().MD_StoresChange(1);
        }

        private void EV_MD_StoresNone(object sender, RoutedEventArgs e)
        {
            GetController().MD_StoresChange(0);
        }*/

        private Controller.CT_PTY_Item_Load GetController()
        {
            if (external == 0)
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = (Main.View.MainWindow)mainWindow;
                return (Controller.CT_PTY_Item_Load)a.MainFrame.Content;
            }

            else
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = ((Main.Controller.CT_Common)((Main.View.MainWindow)mainWindow).MainFrame.Content);
                return (Controller.CT_PTY_Item_Load)a.CT_Submenu.Subcontroller;
            }
        }
    }
}
