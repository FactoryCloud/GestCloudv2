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
        public StoresView StoresView;
        public Store Store;

        public CT_StoreMenu()
        {
            StoresView = new StoresView();
        }

        public void SetCompany(int num)
        {
            Store = db.Stores.Where(c => c.StoreID == num).Include(c => c.CompaniesStores).First();
            //TS_Page = new WorkingBoard.View.TS_WB_ToDo();
            //LeftSide.Content = TS_Page;
        }

        override public void EV_Start (object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void CT_StoreNew()
        {
            Information["controller"] = 1;
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
                    b.MainFrame.Content = new StoreItem.StoreItem_New.Controller.CT_STR_Item_New();
                    break;
            }
        }
    }
}
