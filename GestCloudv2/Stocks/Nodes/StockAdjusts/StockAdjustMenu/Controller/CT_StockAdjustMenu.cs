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

namespace GestCloudv2.Stocks.Nodes.StockAdjusts.StockAdjustMenu.Controller
{
    /// <summary>
    /// Interaction logic for CT_StockAdjustMenu.xaml
    /// </summary>
    public partial class CT_StockAdjustMenu : Main.Controller.CT_Common
    {
        public StockAdjustsView stocksAdjustsView;
        public StockAdjust stockAdjust;

        public CT_StockAdjustMenu()
        {
            stocksAdjustsView = new StockAdjustsView();
        }

        public void SetStockAdjust(int num)
        {
            stockAdjust = db.StockAdjusts.Where(c => c.StockAdjustID == num).Include(c => c.company).First();
            TS_Page = new View.TS_STA_Menu();
            LeftSide.Content = TS_Page;
        }

        override public void EV_Start (object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void CT_StockAdjustNew()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        public void EV_CT_StockAdjustLoad()
        {
            Information["controller"] = 2;
            ChangeController();
        }

        public void EV_CT_StockAdjustLoadEditable()
        {
            Information["controller"] = 3;
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
                    NV_Page = new View.NV_STA_Menu();
                    TS_Page = new View.TS_STA_Menu();
                    MC_Page = new View.MC_STA_Menu();
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
                    a.MainFrame.Content = new Stocks.Controller.CT_Stocks();
                    break;

                /*case 1:
                    Main.View.MainWindow b = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new Files.Nodes.Companies.CompanyItem.CompanyItem_New.Controller.CT_CPN_Item_New();
                    break;

                case 2:
                    Main.View.MainWindow c = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    c.MainFrame.Content = new Files.Nodes.Companies.CompanyItem.CompanyItem_Load.Controller.CT_CPN_Item_Load(company, 0);
                    break;

                case 3:
                    Main.View.MainWindow d = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    d.MainFrame.Content = new Files.Nodes.Companies.CompanyItem.CompanyItem_Load.Controller.CT_CPN_Item_Load(company, 1);
                    break;*/
            }
        }
    }
}
