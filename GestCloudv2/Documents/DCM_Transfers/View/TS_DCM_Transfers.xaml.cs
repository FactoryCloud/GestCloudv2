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

namespace GestCloudv2.Documents.DCM_Transfers.View
{
    /// <summary>
    /// Interaction logic for TS_DCM_Menu.xaml
    /// </summary>
    public partial class TS_DCM_Transfers : Page
    {
        public TS_DCM_Transfers()
        {
            InitializeComponent();
        }

        public void EV_DocumentAdd(object sender, RoutedEventArgs e)
        {
            GetController().EV_DocumentAdd();
        }

        virtual public Controller.CT_DCM_Transfers GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_DCM_Transfers)a.MainFrame.Content;
        }
    }
}
