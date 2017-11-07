using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FrameworkView.V1;
using FrameworkDB.V1;
using Microsoft.EntityFrameworkCore;

namespace GestCloudv2.Files.Nodes.Clients.ClientMenu.Controller
{
    public partial class CT_ClientMenu : Main.Controller.CT_Common
    {
        public Client client;
        public ClientsView clientView;

        public CT_ClientMenu()
        {
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            clientView = new ClientsView();
            UpdateComponents();
        }

        public void SetClient(int num)
        {
            client = db.Clients.Where(c => c.ClientID== num).Include(e => e.entity).First();
            TS_Page = new ClientMenu.View.TS_Client();
            LeftSide.Content = TS_Page;
        }


        public void CT_Main()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        public void CT_ClientNew()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        private void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 1:
                    NV_Page = new View.NV_Client();
                    TS_Page = new View.TS_Client();
                    MC_Page = new View.MC_Client();
                    ChangeComponents();
                    break;
            }
        }

        private void ChangeController()
        {
            switch (Information["controller"])
            {
                case 0:
                    Main.View.MainWindow a = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    a.MainFrame.Content = new Files.Controller.CT_Files();
                    break;

                case 1:
                    Main.View.MainWindow b = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new ClientItem.ClientItem_New.Controller.CT_CLI_Item_New();
                    break;
            }
        }
    }
}
