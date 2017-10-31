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

namespace GestCloudv2.Files.Nodes.Companies.CompanyMenu.View
{
    /// <summary>
    /// Interaction logic for NV_CPN_Menu.xaml
    /// </summary>
    public partial class NV_CPN_Menu : Page
    {
        public NV_CPN_Menu()
        {
            InitializeComponent();
        }

        private void EV_CT_Back(object sender, RoutedEventArgs e)
        {
            GetController().CT_Main();
        }

        private Files.Nodes.Companies.CompanyMenu.Controller.CT_CompanyMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Files.Nodes.Companies.CompanyMenu.Controller.CT_CompanyMenu)a.MainFrame.Content;
        }
    }
}
