using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Main.View
{
    public class SC_Main : Main.View.SC_Common
    {
        public SC_Main():base()
        {
            BT_Minimize.IsEnabled = false;
        }

        public override Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Main.Controller.CT_Main)a.MainFrame.Content;
        }
    }
}
