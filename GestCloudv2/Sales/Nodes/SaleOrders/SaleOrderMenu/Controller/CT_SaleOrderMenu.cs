﻿using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Sales.Nodes.SaleOrders.SaleOrderMenu.Controller
{
    public partial class CT_SaleOrderMenu : Main.Controller.CT_Common
    {
        public SaleOrdersView saleOrdersView;
        public SaleOrder saleOrder;
        public StockAdjust stockAdjust;

        public CT_SaleOrderMenu()
        {
           saleOrdersView = new SaleOrdersView();
        }

        public void SetSaleOrder(int num)
        {
            saleOrder = db.SaleOrders.Where(c => c.SaleOrderID == num).Include(c => c.company).First();
            TS_Page = new View.TS_SOR_Menu();
            LeftSide.Content = TS_Page;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void EV_CT_OrderSaleNew()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        public void EV_CT_SaleOrderLoad()
        {
            Information["controller"] = 2;
            ChangeController();
        }

        public void EV_CT_SaleOrderLoadEditable()
        {
            Information["controller"] = 3;
            ChangeController();
        }

        public override void EV_UpdateShortcutDocuments(int option)
        {
            base.EV_UpdateShortcutDocuments(option);
            SC_Page = new View.SC_SOR_Menu();
            RightSide.Content = SC_Page;
        }

        public void CT_Main()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        public override void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new View.NV_SOR_Menu();
                    TS_Page = new View.TS_SOR_Menu();
                    MC_Page = new View.MC_SOR_Menu();
                    SC_Page = new View.SC_SOR_Menu();
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
                    a.MainFrame.Content = new Sales.Controller.CT_Sales();
                    break;

                case 1:
                    Main.View.MainWindow b = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new SaleOrderItem.SaleOrderItem_New.Controller.CT_SOR_Item_New();
                    break;

                case 2:
                    Main.View.MainWindow c = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    c.MainFrame.Content = new Sales.Nodes.SaleOrders.SaleOrderItem.SaleOrderItem_Load.Controller.CT_SOR_Item_Load(saleOrder, 0);
                    break;

                case 3:
                    Main.View.MainWindow d = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    d.MainFrame.Content = new Sales.Nodes.SaleOrders.SaleOrderItem.SaleOrderItem_Load.Controller.CT_SOR_Item_Load(saleOrder, 1);
                    break;
            
            }

        }
    }
}

