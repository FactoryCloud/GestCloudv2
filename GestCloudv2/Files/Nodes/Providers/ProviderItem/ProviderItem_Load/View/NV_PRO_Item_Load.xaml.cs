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

namespace GestCloudv2.Files.Nodes.Providers.ProviderItem.ProviderItem_Load.View
{
    /// <summary>
    /// Interaction logic for NV_USR_Item_Load.xaml
    /// </summary>
    public partial class NV_PRO_Item_Load : Page
    {
        int external;
        public NV_PRO_Item_Load(int external)
        {
            InitializeComponent();

            this.external = external;
        }

        private void EV_MD_Provider(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(1);
        }

        private void EV_MD_Entity(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(2);
        }

        private void EV_CT_Menu(object sender, RoutedEventArgs e)
        {
            GetController().CT_Menu();
        }

        private Controller.CT_PRO_Item_Load GetController()
        {
            if (external == 0)
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = (Main.View.MainWindow)mainWindow;
                return (Controller.CT_PRO_Item_Load)a.MainFrame.Content;
            }

            else
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = ((Main.Controller.CT_Common)((Main.View.MainWindow)mainWindow).MainFrame.Content);
                return (Controller.CT_PRO_Item_Load)a.CT_Submenu.Subcontroller;
            }
        }
    }
}
