﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Files.Nodes.Clients.ClientItem.ClientItem_New.View
{
    class MC_CLI_Item_Load_Entity_Select : Files.Nodes.Entities.View.MC_Entity_Select
    {
        public MC_CLI_Item_Load_Entity_Select()
        {

        }

        override public Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CLI_Item_New)a.MainFrame.Content;
        }
    }
}
