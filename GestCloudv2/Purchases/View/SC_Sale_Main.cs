using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.View
{
    public class SC_Purchase_Main:Main.View.SC_Common
    {
        public SC_Purchase_Main() : base()
        {
            BT_Minimize.IsEnabled = false;
        }

        public override Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_Purchases)a.MainFrame.Content;
        }
    }
}
