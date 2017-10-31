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
        private Page NV_Page;
        private Page TS_Page;
        private Page MC_Page;

        GestCloudDB db;

        public CT_Files()
        {
            InitializeComponent();
            db = new GestCloudDB();
            Information = new Dictionary<string, int>();
            Information.Add("mode", 1);
            Information.Add("oldmode", 1);
            Information.Add("controller", 0);
            Information.Add("oldcontroller", 0);

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start (object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void MD_Change(int i)
        {
            Information["oldmode"] = Information["mode"];
            Information["mode"] = i;

            UpdateComponents();
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

        public void CT_Main()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        private void UpdateComponents()
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

        private void ChangeComponents()
        {
            TopSide.Content = NV_Page;
            LeftSide.Content = TS_Page;
            MainContent.Content = MC_Page;
        }

        private void ChangeController()
        {
            switch (Information["controller"])
            {
                case 0:
                    Main.View.MainWindow a = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    a.MainFrame.Content = new Main.Controller.CT_Main();
                    break;

                case 5:
                    Main.View.MainWindow b = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new Files.Nodes.Companies.CompanyMenu.Controller.CT_CompanyMenu();
                    break;

                case 7:
                    Main.View.MainWindow c = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    c.MainFrame.Content = new Files.Nodes.Clients.ClientMenu.Controller.CT_ClientMenu();
                    break;
            }
        }
    }
}
