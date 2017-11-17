﻿using FrameworkDB.V1;
using FrameworkView.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestCloudv2.Stocks.Nodes.StockAdjusts.StockAdjustItem.StockAdjustItem_New.View
{
    public partial class FW_STA_Item_New_IncreaseStock : FloatWindows.ProductSelectWindow
    {
        public FW_STA_Item_New_IncreaseStock(int option)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            CB_ProductType.SelectionChanged += new SelectionChangedEventHandler(EV_Search);
            CB_Expansion.SelectionChanged += new SelectionChangedEventHandler(EV_Search);
            TB_ProductName.KeyUp += new KeyEventHandler(EV_Search);
            TB_Quantity.KeyUp += new KeyEventHandler(EV_QuantityChange);
            DG_Products.MouseLeftButtonUp += new MouseButtonEventHandler(EV_ProductsSelect);

            productsView = new ProductsView(1);
            movement = new Movement();
            UpdateData();
        }

        override public Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (StockAdjustItem_New.Controller.CT_STA_Item_New)a.MainFrame.Content;
        }
    }
}
