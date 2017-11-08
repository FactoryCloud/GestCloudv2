using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Files.Nodes.Clients.ClientItem.ClientItem_New.View
{
    public partial class MC_CLI_Item_New_Entity_New : Files.Nodes.Entities.View.MC_Entity_New
    {
        public MC_CLI_Item_New_Entity_New()
        {
        }

        override public Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (ClientItem_New.Controller.CT_CLI_Item_New)a.MainFrame.Content;
        }
    }
}
