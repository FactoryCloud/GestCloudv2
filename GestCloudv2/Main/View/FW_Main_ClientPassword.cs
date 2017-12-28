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

namespace GestCloudv2.Main.View
{
    public partial class FW_Main_ClientPassword : FloatWindows.PasswordWindow
    {
        public FW_Main_ClientPassword(int userID, UserAccessControl uac) : base(userID, uac)
        {
            
        }

        override public Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_Main)a.MainFrame.Content;
        }
    }
}
