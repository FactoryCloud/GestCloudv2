using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseInvoices.PurchaseInvoiceItem.PurchaseInvoiceItem_New.Controller
{
    public partial class CT_PIN_Item_New : Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New
    {
        public PurchaseInvoice purchaseInvoice;

        public CT_PIN_Item_New():base()
        {
            purchaseInvoice = new PurchaseInvoice();
            GetLastCode();
            Information["operationType"] = 1;
        }

        public void CleanPurchaseCode()
        {
            purchaseInvoice.Code = "";
            TestMinimalInformation();
        }

        override public void SetDate(DateTime date)
        {
            purchaseInvoice.Date = date;
            base.SetDate(date);
        }

        public override void SetMC(int i)
        {
            switch (i)
            {
                case 1:
                    MC_Page = new View.MC_PIN_Item_New_PurchaseInvoice();
                    break;

                case 2:
                    MC_Page = new View.MC_PIN_Item_New_Movements();
                    break;
            }
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_PIN_Item_New_PurchaseInvoice();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_PIN_Item_New_PurchaseInvoice();
        }

        public override void SetSC()
        {
            SC_Page = new View.SC_PIN_Item_New_PurchaseOrder();
        }

        public override string GetCode()
        {
            return purchaseInvoice.Code;
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

            purchaseInvoice.Code = lastCode.ToString();
        }

        override public void MD_ProviderSelect()
        {
            View.FW_PIN_Item_New_SelectProvider floatWindow = new View.FW_PIN_Item_New_SelectProvider();
            floatWindow.Show();
        }

        override public void MD_MovementAdd()
        {
            View.FW_PIN_Item_New_Movements floatWindow = new View.FW_PIN_Item_New_Movements();
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
            purchaseInvoice.Code = code;
            base.CodeExist(code);
            return false;
        }

        override public void TestMinimalInformation()
        {
            if (purchaseInvoice.Date != null && provider.ProviderID > 0 && store.StoreID > 0)
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
            purchaseInvoice.CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID;
            purchaseInvoice.ProviderID = provider.ProviderID;
            db.PurchaseInvoices.Add(purchaseInvoice);
            db.SaveChanges();

            foreach (Movement movement in movementsView.movements)
            {
                movement.MovementID = 0;
                movement.ConditionID = movement.condition.ConditionID;
                movement.condition = null;
                movement.ProductID = movement.product.ProductID;
                movement.product = null;

                if (movement.store == null)
                {
                    movement.DocumentTypeID = db.DocumentTypes.Where(c => c.Name == "Invoice" && c.Input == 1).First().DocumentTypeID;
                    movement.StoreID = store.StoreID;
                }

                else
                {
                    movement.DocumentTypeID = movement.documentType.DocumentTypeID;
                    movement.documentType = null;
                    if (movement.Quantity < 0)
                        movement.Quantity = movement.Quantity * -1;
                }

                movement.DocumentID = purchaseInvoice.PurchaseInvoiceID;
                db.Movements.Add(movement);
            }

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
                    a.MainFrame.Content = new Purchases.Nodes.PurchaseInvoices.PurchaseInvoiceMenu.Controller.CT_PurchaseInvoiceMenu();
                    break;
            }
        }
    }
}
