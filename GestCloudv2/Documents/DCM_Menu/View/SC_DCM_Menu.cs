using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Documents.DCM_Menu.View
{
    public class SC_DCM_Menu: Main.View.SC_Common
    {
        public SC_DCM_Menu() : base()
        {
            BT_Minimize.IsEnabled = false;
        }

        public override Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_DCM_Menu)a.MainFrame.Content;
        }
    }
}
