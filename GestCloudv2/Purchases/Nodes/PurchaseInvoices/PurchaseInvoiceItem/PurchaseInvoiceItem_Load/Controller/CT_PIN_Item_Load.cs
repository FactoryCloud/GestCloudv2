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

namespace GestCloudv2.Purchases.Nodes.PurchaseInvoices.PurchaseInvoiceItem.PurchaseInvoiceItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_STA_Item_New.xaml
    /// </summary>
    public partial class CT_PIN_Item_Load : Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load
    {
        public PurchaseInvoice purchaseInvoice;

        public CT_PIN_Item_Load(PurchaseInvoice purchaseInvoice, int editable):base(editable)
        {
            this.purchaseInvoice = db.PurchaseInvoices.Where(c => c.PurchaseInvoiceID == purchaseInvoice.PurchaseInvoiceID).Include(e => e.provider).Include(i => i.provider.entity).Include(p => p.store).Include(t => t.paymentMethod).First();
            Information["operationType"] = 1;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            base.EV_Start(sender, e);
        }

        override public void CleanCode()
        {
            purchaseInvoice.Code = "";
            base.CleanCode();
        }

        override public void SetCode(string code)
        {
            purchaseInvoice.Code = code;
        }

        override public void SetDate(DateTime date)
        {
            purchaseInvoice.Date = date;
            base.SetDate(date);
        }

        public override void SetStore(int num)
        {
            purchaseInvoice.store = db.Stores.Where(s => s.StoreID == num).First();
            base.SetStore(num);
        }

        public override void SetPaymentMethod(int num)
        {
            purchaseInvoice.paymentMethod = db.PaymentMethods.Where(s => s.PaymentMethodID == num).First();
            base.SetPaymentMethod(num);
        }

        public override Store GetStore()
        {
            return purchaseInvoice.store;
        }

        public override PaymentMethod GetPaymentMethod()
        {
            return purchaseInvoice.paymentMethod;
        }

        public override void SetMC(int i)
        {
            switch (i)
            {
                case 1:
                    MC_Page = new View.MC_PIN_Item_Load_PurchaseInvoice();
                    break;

                case 2:
                    MC_Page = new View.MC_PIN_Item_Load_Movements();
                    break;

                case 3:
                    MC_Page = new View.MC_PIN_Item_Load_Summary();
                    break;
            }
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_PIN_Item_Load_PurchaseInvoice();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_PIN_Item_Load_PurchaseInvoice();
        }

        public override string GetCode()
        {
            return purchaseInvoice.Code;
        }

        public override Provider GetProvider()
        {
            return purchaseInvoice.provider;
        }

        public override DateTime GetDate()
        {
            return Convert.ToDateTime(purchaseInvoice.Date);
        }

        public override int GetDocumentID()
        {
            return purchaseInvoice.PurchaseInvoiceID;
        }

        public override DocumentType GetDocumentType()
        {
            return db.DocumentTypes.Where(d => d.Input == 1 && d.Name.Contains("Invoice")).First();
        }

        public override Shortcuts.ShortcutDocument GetShortcutDocument(int num)
        {
            return new Shortcuts.ShortcutDocument
            {
                Id = num,
                Name = $"Factura de Compra ({GetCode()})",
                Controller = this

            };
        }

        override public int LastCode()
        {
            if (db.PurchaseDeliveries.ToList().Count > 0)
            {
                lastCode = db.PurchaseDeliveries.OrderBy(u => u.PurchaseDeliveryID).Last().PurchaseDeliveryID + 1;
                purchaseInvoice.Code = lastCode.ToString();
                return lastCode;
            }
            else
            {
                purchaseInvoice.Code = $"1";
                return lastCode = 1;
            }
        }

        override public void MD_MovementAdd()
        {
            View.FW_PIN_Item_Load_Movements floatWindow = new View.FW_PIN_Item_Load_Movements();
            floatWindow.Show();
        }

        override public void MD_MovementEdit()
        {
            View.FW_PIN_Item_Load_Movements floatWindow = new View.FW_PIN_Item_Load_Movements(new Movement(movementSelected));
            floatWindow.Show();
        }

        override public Boolean CodeExist(string code)
        {
            List<PurchaseDelivery> deliveries = db.PurchaseDeliveries.ToList();
            foreach (var item in deliveries)
            {
                if (item.Code.Contains(code) || code.Length == 0)
                {
                    CleanCode();
                    return true;
                }
            }
            purchaseInvoice.Code = code;

            base.CodeExist(code);
            return false;
        }

        override public void TestMinimalInformation()
        {
            if(purchaseInvoice.Date != null && purchaseInvoice.provider.ProviderID > 0 && GetStore().StoreID > 0)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            base.TestMinimalInformation();
        }

        override public void SaveDocument()
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
                    a.MainFrame.Content = new PurchaseInvoices.PurchaseInvoiceMenu.Controller.CT_PurchaseInvoiceMenu();
                    break;
            }
        }
    }
}