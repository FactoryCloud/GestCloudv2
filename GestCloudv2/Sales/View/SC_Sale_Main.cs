using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Sales.View
{
    public class SC_Sale_Main:Main.View.SC_Common
    {
        public SC_Sale_Main() : base()
        {
            BT_Minimize.IsEnabled = false;
        }

        public override Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_Sales)a.MainFrame.Content;
        }
    }
}
