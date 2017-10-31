using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FrameworkView.V1;

namespace GestCloudv2.Files.Nodes.Clients.ClientMenu.Controller
{
    public partial class CT_ClientMenu : Main.Controller.CT_Common
    {
        private Page MC_Page;
        private Page TS_Page;
        private Page NV_Page;
        public ClientsView clientView;

        public CT_ClientMenu()
        {
            InitializeComponent();
            Information = new Dictionary<string, int>();
            Information.Add("mode", 0);
            Information.Add("controller", 0);

            this.Loaded += new RoutedEventHandler(EV_Start);
        }


        private void EV_Start(object sender, RoutedEventArgs e)
        {
            clientView = new ClientsView();
            UpdateComponents();
        }

        private void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    NV_Page = new View.NV_Client();
                    TS_Page = new View.TS_Client();
                    MC_Page = new View.MC_Client();
                    ChangeComponents();
                    break;
            }
        }

        private void ChangeComponents()
        {
            TopSide.Content = NV_Page;
            LeftSide.Content = TS_Page;
            MainContent.Content = MC_Page;
        }
    }
}
