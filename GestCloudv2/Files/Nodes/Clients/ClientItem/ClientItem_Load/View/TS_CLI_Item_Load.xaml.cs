﻿using System;
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

namespace GestCloudv2.Files.Nodes.Clients.ClientItem.ClientItem_Load.View
{
    /// <summary>
    /// Interaction logic for TS_USR_Item_Load.xaml
    /// </summary>
    public partial class TS_CLI_Item_Load : Page
    {
        int external;
        public TS_CLI_Item_Load(int num, int external)
        {
            InitializeComponent();
            this.external = external;
        }

        /*private void EV_ClientSave(object sender, RoutedEventArgs e)
        {
            GetController().SaveLoadClient();
        }*/

        private Controller.CT_CLI_Item_Load GetController()
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
