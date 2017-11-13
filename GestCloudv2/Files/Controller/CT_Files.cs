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

namespace GestCloudv2.Files.Controller
{
    /// <summary>
    /// Interaction logic for MainController.xaml
    /// </summary>
    public partial class CT_Files : Main.Controller.CT_Common
    {
        public CT_Files()
        {
            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        override public void EV_Start (object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void MD_Change(int i)
        {
            Information["oldmode"] = Information["mode"];
            Information["mode"] = i;

            UpdateComponents();
        }

        public void CT_Stores()
        {
            Information["controller"] = 4;
            ChangeController();
        }

        public void CT_Companies()
        {
            Information["controller"] = 5;
            ChangeController();
        }

        public void CT_Clients()
        {
            Information["controller"] = 7;
            ChangeController();
        }

        public void CT_Providers()
        {
            Information["controller"] = 8;
            ChangeController();
        }

        public void CT_Users()
        {
            Information["controller"] = 11;
            ChangeController();
        }

        public void CT_Main()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        public override void UpdateComponents()
        {
            switch(Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new Files.View.NV_Files_Main();
                    TS_Page = null;
                    MC_Page = null;
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
                    a.MainFrame.Content = new Main.Controller.CT_Main();
                    break;

                case 4:
                    Main.View.MainWindow e = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    e.MainFrame.Content = new Files.Nodes.Stores.StoreMenu.Controller.CT_StoreMenu();
                    break;

                case 5:
                    Main.View.MainWindow f = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    f.MainFrame.Content = new Files.Nodes.Companies.CompanyMenu.Controller.CT_CompanyMenu();
                    break;

                case 7:
                    Main.View.MainWindow c = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    c.MainFrame.Content = new Files.Nodes.Clients.ClientMenu.Controller.CT_ClientMenu();
                    break;

                case 8:
                    Main.View.MainWindow d = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    d.MainFrame.Content = new Files.Nodes.Providers.ProviderMenu.Controller.CT_ProviderMenu();
                    break;

                case 11:
                    Main.View.MainWindow k = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    k.MainFrame.Content = new Files.Nodes.Users.UserMenu.Controller.CT_UserMenu();
                    break;
            }
        }
    }
}
