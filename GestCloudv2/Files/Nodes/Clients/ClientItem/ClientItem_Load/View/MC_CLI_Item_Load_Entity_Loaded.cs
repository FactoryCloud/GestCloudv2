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
using FrameworkDB.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.Data;
using System.Collections;

namespace GestCloudv2.Files.Nodes.Clients.ClientItem.ClientItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_USR_Item_Load_Entity_Loaded.xaml
    /// </summary>
    public partial class MC_CLI_Item_Load_Entity_Loaded : Files.Nodes.Entities.View.MC_Entity_Loaded
    {
        int external;
        public MC_CLI_Item_Load_Entity_Loaded(int external)
        {
            this.external = external;
        }

        override public Main.Controller.CT_Common GetController()
        {
            if (external == 0)
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = (Main.View.MainWindow)mainWindow;
                return (Controller.CT_CLI_Item_Load)a.MainFrame.Content;
            }

            else
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = ((Main.Controller.CT_Common)((Main.View.MainWindow)mainWindow).MainFrame.Content);
                return (Controller.CT_CLI_Item_Load)a.CT_Submenu.Subcontroller;
            }
        }
    }
}
