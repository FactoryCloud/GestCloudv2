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

namespace GestCloudv2.Files.Nodes.Users.UserItem.UserItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_USR_Item_Load_Entity_Select.xaml
    /// </summary>
    public partial class MC_USR_Item_Load_Entity_Select : Files.Nodes.Entities.View.MC_Entity_Load_Select
    {

        public MC_USR_Item_Load_Entity_Select()
        {

        }

        override public Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_USR_Item_Load)a.MainFrame.Content;
        }
    }
}
