using FrameworkDB.V1;
using FrameworkView.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestCloudv2.Documents.DCM_Items.DCM_Item_Load.View
{
    public partial class SC_DCM_Item_Load_Main : Main.View.SC_Common
    {
        public override void CT_DocumentMinimize(object sender, RoutedEventArgs e)
        {
            GetController().AddShortcutDocument();
            GetController().CT_MainWindow();
        }

        public override Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_DCM_Item_Load)a.MainFrame.Content;
        }
    }
}
