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

namespace GestCloudv2.Files.Nodes.Providers.ProviderItem.ProviderItem_New.View
{
    /// <summary>
    /// Interaction logic for NV_PRO_Item_New.xaml
    /// </summary>
    public partial class NV_PRO_Item_New : Page
    {
        public NV_PRO_Item_New()
        {
            InitializeComponent();
        }

        private void EV_PT_Menu(object sender, RoutedEventArgs e)
        {
            GetController().CT_Menu();
        }

        private ProviderItem_New.Controller.CT_PRO_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (ProviderItem_New.Controller.CT_PRO_Item_New)a.MainFrame.Content;
        }
    }
}
