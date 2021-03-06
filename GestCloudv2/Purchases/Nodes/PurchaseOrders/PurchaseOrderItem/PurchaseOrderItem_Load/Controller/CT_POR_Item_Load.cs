﻿using System;
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
using System.Security.Cryptography;
using FrameworkView.V1;

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderItem.PurchaseOrderItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_STA_Item_New.xaml
    /// </summary>
    public partial class CT_POR_Item_Load : Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load
    {
        public PurchaseOrder purchaseOrder;

        public CT_POR_Item_Load(PurchaseOrder purchaseOrder, int editable):base(editable)
        {
            this.purchaseOrder = db.PurchaseOrders.Where(c => c.PurchaseOrderID == purchaseOrder.PurchaseOrderID).Include(e => e.provider).Include(i => i.provider.entity).Include(p => p.store).Include(p => p.paymentMethod).First();
            Information["operationType"] = 1;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            base.EV_Start(sender, e);
        }

        public override void SetCode(string code)
        {
            purchaseOrder.Code = code;
            base.SetCode(code);
        }

        override public void SetDate(DateTime date)
        {
            purchaseOrder.Date = date;
            base.SetDate(date);
        }

        public override void SetStore(int num)
        {
            purchaseOrder.store = db.Stores.Where(s => s.StoreID == num).First();
            base.SetStore(num);
        }

        public override void SetPaymentMethod(int num)
        {
            purchaseOrder.paymentMethod = db.PaymentMethods.Where(s => s.PaymentMethodID== num).First();
            base.SetPaymentMethod(num);
        }

        public override Store GetStore()
        {
            return purchaseOrder.store;
        }

        public override PaymentMethod GetPaymentMethod()
        {
            return purchaseOrder.paymentMethod;
        }

        public override void SetMC(int i)
        {
            switch(i)
            {
                case 1:
                    MC_Page = new View.MC_POR_Item_Load_PurchaseOrder();
                    break;

                case 2:
                    MC_Page = new View.MC_POR_Item_Load_Movements();
                    break;

                case 3:
                    MC_Page = new View.MC_POR_Item_Load_Summary();
                    break;
            }
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_POR_Item_Load_PurchaseOrder();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_POR_Item_Load_PurchaseOrder();
        }

        public override Provider GetProvider()
        {
            return purchaseOrder.provider;
        }

        public override string GetCode()
        {
            return $"{purchaseOrder.Code}";
        }

        public override int GetDocumentID()
        {
            return purchaseOrder.PurchaseOrderID;
        }

        public override DocumentType GetDocumentType()
        {
            return db.DocumentTypes.Where(d => d.Input == 1 && d.Name.Contains("Order")).First();
        }

        public override DateTime GetDate()
        {
            return (DateTime)purchaseOrder.Date;
        }

        public override Shortcuts.ShortcutDocument GetShortcutDocument(int num)
        {
            return new Shortcuts.ShortcutDocument
            {
                Id = num,
                Name = $"Pedido de Compra ({GetCode()})",
                Controller = this

            };
        }

        override public void CleanCode()
        {
            purchaseOrder.Code = "";
            base.CleanCode();
        }

        override public int LastCode()
        {
            return lastCode;
        }

        override public void MD_MovementAdd()
        {
            View.FW_POR_Item_Load_Movements floatWindow = new View.FW_POR_Item_Load_Movements();
            floatWindow.Show();
        }

        override public void MD_MovementEdit()
        {
            View.FW_POR_Item_Load_Movements floatWindow = new View.FW_POR_Item_Load_Movements(new Movement(movementSelected));
            floatWindow.Show();
        }

        override public Boolean CodeExist(string order)
        {
            List<PurchaseOrder> orders = db.PurchaseOrders.ToList();
            foreach (var item in orders)
            {
                if (item.Code.Contains(order) || order.Length == 0)
                {
                    CleanCode();
                    return true;
                }
            }
            purchaseOrder.Code = order;
            TestMinimalInformation();
            return false;
        }

        public override void EV_ActivateSaveButton(bool verificated)
        {
            if(verificated)
            {
                Information["entityValid"] = 1;
            }

            else
            {
                Information["entityValid"] = 0;
            }

            TestMinimalInformation();
        }

        override public void TestMinimalInformation()
        {
            if(purchaseOrder.Date != null && purchaseOrder.provider.ProviderID > 0 && GetStore().StoreID > 0)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            SetTS();
            LeftSide.Content = TS_Page;
        }

        public override void SaveDocument()
        {
            base.SaveDocument();
        }

        override public void ChangeController()
        {
            switch (Information["controller"])
            {
                case 0:
                    if (Information["fieldEmpty"] == 1)
                    {
                        MessageBoxResult result = MessageBox.Show("Usted ha realizado cambios, ¿Esta seguro que desea salir?", "Volver", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                    Main.View.MainWindow a = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    a.MainFrame.Content = new Purchases.Nodes.PurchaseOrders.PurchaseOrderMenu.Controller.CT_PurchaseOrderMenu();
                    break;
            }
        }
    }
}