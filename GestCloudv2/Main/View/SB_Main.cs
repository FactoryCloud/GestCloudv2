using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GestCloudv2.Main.View
{
    public class SB_Main : Main.View.SB_Common
    {
        public SB_Main():base()
        {
            //BT_Minimize.IsEnabled = false;
        }

        public override void EV_SetUser(object sender, RoutedEventArgs e)
        {
            BT_User.IsChecked = false;
            Window FL_Password = new FW_Main_ClientPassword(Convert.ToInt16(((Button)sender).Tag), ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).uac);
            FL_Password.Show();
        }

        public override Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Main.Controller.CT_Main)a.MainFrame.Content;
        }
    }
}
