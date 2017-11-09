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

namespace GestCloudv2.Files.Nodes.Providers.ProviderMenu.Controller
{
    public partial class CT_ProviderMenu : Main.Controller.CT_Common
    {
        public ProvidersView providersView;
        public Provider provider;

        public CT_ProviderMenu()
        {
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            providersView = new ProvidersView();
            UpdateComponents();
        }

        public void SetProvider(int num)
        {
            provider = db.Providers.Where(c => c.ProviderID == num).Include(d => d.entity).First();
            TS_Page = new ProviderMenu.View.TS_Provider();
            LeftSide.Content = TS_Page;
        }

        public void CT_Main()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        public void CT_ProviderNew()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        public void CT_ProviderLoad()
        {
            Information["controller"] = 2;
            ChangeController();
        }

        public void CT_ProviderLoadEditable()
        {
            Information["controller"] = 3;
            ChangeController();
        }

        private void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 1:
                    NV_Page = new View.NV_Provider();
                    TS_Page = new View.TS_Provider();
                    MC_Page = new View.MC_Provider();
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
                    b.MainFrame.Content = new ProviderItem.ProviderItem_New.Controller.CT_PRO_Item_New();
                    break;

                case 2:
                    Main.View.MainWindow c = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    c.MainFrame.Content = new Files.Nodes.Providers.ProviderItem.ProviderItem_Load.Controller.CT_PRO_Item_Load(provider, 0);
                    break;

                case 3:
                    Main.View.MainWindow d = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    d.MainFrame.Content = new Files.Nodes.Providers.ProviderItem.ProviderItem_Load.Controller.CT_PRO_Item_Load(provider, 1);
                    break;
            }
        }
    }
}
