using FrameworkDB.V1;
using FrameworkView.V1;
using GestCloudv2.Shortcuts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryItem.PurchaseDeliveryItem_New.Controller
{
    public partial class CT_PDE_Item_New : Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New
    {
        public PurchaseDelivery purchaseDelivery;

        public CT_PDE_Item_New():base()
        {
            purchaseDelivery = new PurchaseDelivery();
            Information["operationType"] = 1;
            GetLastCode();
        }

        public void CleanPurchaseCode()
        {
            purchaseDelivery.Code = "";
            TestMinimalInformation();
        }

        override public void SetDate(DateTime date)
        {
            purchaseDelivery.Date = date;
            base.SetDate(date);
        }

        public override void SetMC(int i)
        {
            switch (i)
            {
                case 1:
                    MC_Page = new View.MC_PDE_Item_New_PurchaseDelivery();
                    break;

                case 2:
                    MC_Page = new View.MC_PDE_Item_New_Movements();
                    break;

                case 3:
                    MC_Page = new View.MC_PDE_Item_New_Summary();
                    break;
            }
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_PDE_Item_New_PurchaseDelivery();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_PDE_Item_New_PurchaseDelivery();
        }

        public override string GetCode()
        {
            return purchaseDelivery.Code;
        }

        public override int GetDocumentID()
        {
            return purchaseDelivery.PurchaseDeliveryID;
        }

        public override DocumentType GetDocumentType()
        {
            return db.DocumentTypes.Where(d => d.Input == 1 && d.Name.Contains("Delivery")).First();
        }

        override public void GetLastCode()
        {
            if (db.PurchaseDeliveries.ToList().Count > 0)
            {
                lastCode = db.PurchaseDeliveries.OrderBy(u => u.PurchaseDeliveryID).Last().PurchaseDeliveryID + 1;
            }
            else
            {
                lastCode = 1;
            }

            purchaseDelivery.Code = lastCode.ToString();
        }

        public override ShortcutDocument GetShortcutDocument(int num)
        {
            return new Shortcuts.ShortcutDocument
            {
                Id = num,
                Name = $"Albarán de Compra (Nuevo)",
                Controller = this

            };
        }

        override public void MD_ProviderSelect()
        {
            View.FW_PDE_Item_New_SelectProvider floatWindow = new View.FW_PDE_Item_New_SelectProvider();
            floatWindow.Show();
        }

        override public void MD_MovementAdd()
        {
            View.FW_PDE_Item_New_Movements floatWindow = new View.FW_PDE_Item_New_Movements();
            floatWindow.Show();
        }

        override public void MD_MovementEdit()
        {
            View.FW_PDE_Item_New_Movements floatWindow = new View.FW_PDE_Item_New_Movements(new Movement(movementSelected));
            floatWindow.Show();
        }

        override public Boolean CodeExist(string code)
        {
            List<PurchaseDelivery> purchaseDeliveries = db.PurchaseDeliveries.ToList();
            foreach (var item in purchaseDeliveries)
            {
                if (item.Code.Contains(code) || code.Length == 0)
                {
                    CleanPurchaseCode();
                    return true;
                }
            }
            purchaseDelivery.Code = code;
            base.CodeExist(code);
            return false;
        }

        override public void SaveDocument()
        {
            purchaseDelivery.CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID;
            purchaseDelivery.ProviderID = provider.ProviderID;
            purchaseDelivery.store = db.Stores.Where(s => s.StoreID == store.StoreID).First();
            purchaseDelivery.PurchaseDeliveryFinalPrice = documentContent.PurchaseFinalPrice;
            db.PurchaseDeliveries.Add(purchaseDelivery);
            db.SaveChanges();

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
                    a.MainFrame.Content = new Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryMenu.Controller.CT_PurchaseDeliveryMenu();
                    break;
            }
        }
    }
}
