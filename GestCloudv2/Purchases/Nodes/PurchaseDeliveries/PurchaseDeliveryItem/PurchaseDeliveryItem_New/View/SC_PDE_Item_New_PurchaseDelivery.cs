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

namespace GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryItem.PurchaseDeliveryItem_New.View
{
    public partial class SC_PDE_Item_New_PurchaseDelivery : Main.View.SC_Common
    {
        public override void CT_DocumentMinimize(object sender, RoutedEventArgs e)
        {
            int num = 0;
            if (((Main.View.MainWindow)Application.Current.MainWindow).shortcutDocuments.Count == 0)
                num = 1;

            else
                num = ((Main.View.MainWindow)Application.Current.MainWindow).shortcutDocuments.OrderBy(sd => sd.Id).Last().Id + 1;

            ((Main.View.MainWindow)Application.Current.MainWindow).shortcutDocuments.Add(new Shortcuts.ShortcutDocument
            {
                Id = num,
                Name = $"Albarán de Compra (Nuevo)",
                Controller = GetController()
            });

            GetController().CT_MainWindow();
        }

        public override Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PDE_Item_New)a.MainFrame.Content;
        }
    }
}