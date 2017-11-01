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
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;

namespace GestCloudv2.Files.Nodes.Stores.StoreMenu.Controller
{
    /// <summary>
    /// Interaction logic for CT_StoreMenu.xaml
    /// </summary>
    public partial class CT_StoreMenu : Main.Controller.CT_Common
    {
        private Page NV_Page;
        private Page TS_Page;
        private Page MC_Page;

        GestCloudDB db;

        public StoresView StoresView;
        public Store Store;

        public CT_StoreMenu()
        {
            InitializeComponent();
            db = new GestCloudDB();
            StoresView = new StoresView();
            Information = new Dictionary<string, int>();
            Information.Add("mode", 1);
            Information.Add("oldmode", 1);
            Information.Add("controller", 0);
            Information.Add("oldcontroller", 0);

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        public void SetCompany(int num)
        {
            Store = db.Stores.Where(c => c.StoreID == num).Include(c => c.CompaniesStores).First();
            //TS_Page = new WorkingBoard.View.TS_WB_ToDo();
            //LeftSide.Content = TS_Page;
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
                    NV_Page = new Files.Nodes.Stores.StoreMenu.View.NV_STR_Menu();
                    TS_Page = new Files.Nodes.Stores.StoreMenu.View.TS_STR_Menu(); ;
                    MC_Page = new Files.Nodes.Stores.StoreMenu.View.MC_STR_Menu(); ;
                    ChangeComponents();
                    break;

                case 2:
                    ChangeComponents();
                    break;

                case 3:
                    ChangeComponents();
                    break;

                case 4:
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
                    a.MainFrame.Content = new Files.Controller.CT_Files();
                    break;

                case 1:
                    /*MainWindow b = (MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new Main.Controller.MainController();*/
                    break;
            }
        }
    }
}
