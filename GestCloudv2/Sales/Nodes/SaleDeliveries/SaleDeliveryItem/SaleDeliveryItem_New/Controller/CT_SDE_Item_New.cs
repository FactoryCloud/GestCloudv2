using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Sales.Nodes.SaleDeliveries.SaleDeliveryItem.SaleDeliveryItem_New.Controller
{
    public partial class CT_SDE_Item_New : Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New
    {
        public SaleDelivery saleDelivery;

        public CT_SDE_Item_New():base()
        {
            saleDelivery = new SaleDelivery();
            Information["operationType"] = 2;
            GetLastCode();
        }

        public void CleanPurchaseCode()
        {
            saleDelivery.Code = "";
            TestMinimalInformation();
        }

        override public void SetDate(DateTime date)
        {
            saleDelivery.Date = date;
            base.SetDate(date);
        }

        public override void SetMC(int i)
        {
            switch (i)
            {
                case 1:
                    MC_Page = new View.MC_SDE_Item_New_SaleDelivery();
                    break;

                case 2:
                    MC_Page = new View.MC_SDE_Item_New_Movements();
                    break;

                case 3:
                    MC_Page = new View.MC_SDE_Item_New_Summary();
                    break;
            }
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_SDE_Item_New_SaleDelivery();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_SDE_Item_New_SaleDelivery();
        }

        public override string GetCode()
        {
            return saleDelivery.Code;
        }

        override public void GetLastCode()
        {
            if (db.SaleDeliveries.ToList().Count > 0)
            {
                lastCode = db.SaleDeliveries.OrderBy(u => u.SaleDeliveryID).Last().SaleDeliveryID + 1;
            }
            else
            {
                lastCode = 1;
            }

            saleDelivery.Code = lastCode.ToString();
        }

        public override Client GetClient()
        {
            return client;
        }

        public override int GetDocumentID()
        {
            return saleDelivery.SaleDeliveryID;
        }

        public override DocumentType GetDocumentType()
        {
            return db.DocumentTypes.Where(d => d.Input == 0 && d.Name.Contains("Delivery")).First();
        }

        public override Shortcuts.ShortcutDocument GetShortcutDocument(int num)
        {
            return new Shortcuts.ShortcutDocument
            {
                Id = num,
                Name = $"Albarán de Venta (Nuevo)",
                Controller = this

            };
        }

        override public void MD_ClientSelect()
        {
            View.FW_SDE_Item_New_SelectClient floatWindow = new View.FW_SDE_Item_New_SelectClient(1);
            floatWindow.Show();
        }

        override public void MD_MovementAdd()
        {
            View.FW_SDE_Item_New_Movements floatWindow = new View.FW_SDE_Item_New_Movements();
            switch (((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).GetConfigValue(db.Configurations.Where(c => c.Name.Contains("LimiteStock")).First().ConfigurationID))
            {
                case 0:
                    floatWindow = new View.FW_SDE_Item_New_Movements();
                    break;

                case 1:
                    floatWindow = new View.FW_SDE_Item_New_Movements(Information["operationType"], movements);
                    break;
            }
            floatWindow.Show();
        }

        override public void MD_MovementEdit()
        {
            View.FW_SDE_Item_New_Movements floatWindow = new View.FW_SDE_Item_New_Movements();
            switch (((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).GetConfigValue(db.Configurations.Where(c => c.Name.Contains("LimiteStock")).First().ConfigurationID))
            {
                case 0:
                    floatWindow = new View.FW_SDE_Item_New_Movements(new Movement(movementSelected));
                    break;

                case 1:
                    floatWindow = new View.FW_SDE_Item_New_Movements(Information["operationType"], movements, new Movement(movementSelected));
                    break;
            }
            floatWindow.Show();
        }

        override public Boolean CodeExist(string code)
        {
            List<SaleDelivery> purchaseDeliveries = db.SaleDeliveries.ToList();
            foreach (var item in purchaseDeliveries)
            {
                if (item.Code.Contains(code) || code.Length == 0)
                {
                    CleanPurchaseCode();
                    return true;
                }
            }
            saleDelivery.Code = code;
            base.CodeExist(code);
            return false;
        }

        override public void SaveDocument()
        {
            saleDelivery.CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID;
            saleDelivery.ClientID = client.ClientID;
            saleDelivery.store = db.Stores.Where(s => s.StoreID == store.StoreID).First();
            saleDelivery.SaleDeliveryFinalPrice = documentContent.SaleFinalPrice;
            saleDelivery.paymentMethod = db.PaymentMethods.Where(p => p.PaymentMethodID == paymentMethod.PaymentMethodID).First();
            db.SaleDeliveries.Add(saleDelivery);
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
                    a.MainFrame.Content = new SaleDeliveryMenu.Controller.CT_SaleDeliveryMenu();
                    break;
            }
        }
    }
}
