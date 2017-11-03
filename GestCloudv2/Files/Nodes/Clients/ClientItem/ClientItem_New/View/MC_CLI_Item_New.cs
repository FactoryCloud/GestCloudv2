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

namespace GestCloudv2.Files.Nodes.Clients.ClientItem.ClientItem_New.View
{
    /// <summary>
    /// Interaction logic for NewUser_MainPage.xaml
    /// </summary>
    public partial class MC_CLI_Item_New : Files.Nodes.Entities.View.MC_Entity_New
    {

        public MC_CLI_Item_New()
        {

        }

        private void ControlFieldsKey_Event(object sender, RoutedEventArgs e)
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
