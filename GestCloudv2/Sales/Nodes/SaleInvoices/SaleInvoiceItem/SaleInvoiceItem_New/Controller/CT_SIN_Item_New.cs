using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Sales.Nodes.SaleInvoices.SaleInvoiceItem.SaleInvoiceItem_New.Controller
{
    public partial class CT_SIN_Item_New : Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New
    {
        public SaleInvoice saleInvoice;

        public CT_SIN_Item_New():base()
        {
            saleInvoice = new SaleInvoice();
            Information["operationType"] = 2;
            GetLastCode();
        }

        public void CleanPurchaseCode()
        {
            saleInvoice.Code = "";
            TestMinimalInformation();
        }

        override public void SetDate(DateTime date)
        {
            saleInvoice.Date = date;
            base.SetDate(date);
        }

        public override void SetMC(int i)
        {
            switch (i)
            {
                case 1:
                    MC_Page = new View.MC_SIN_Item_New_SaleInvoice();
                    break;

                case 2:
                    MC_Page = new View.MC_SIN_Item_New_Movements();
                    break;

                case 3:
                    MC_Page = new View.MC_PIN_Item_New_Summary();
                    break;
            }
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_SIN_Item_New_SaleInvoice();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_SIN_Item_New_SaleInvoice();
        }

        public override void SetSC()
        {
            SC_Page = new View.SC_SIN_Item_New_SaleInvoice();
        }

        public override string GetCode()
        {
            return saleInvoice.Code;
        }

        public override Client GetClient()
        {
            return client;
        }

        public override int GetDocumentID()
        {
            return saleInvoice.SaleInvoiceID;
        }

        public override DocumentType GetDocumentType()
        {
            return db.DocumentTypes.Where(d => d.Input == 0 && d.Name.Contains("Invoice")).First();
        }

        override public void GetLastCode()
        {
            if (db.SaleInvoices.ToList().Count > 0)
            {
                lastCode = db.SaleInvoices.OrderBy(u => u.SaleInvoiceID).Last().SaleInvoiceID + 1;
            }
            else
            {
                lastCode = 1;
            }

            saleInvoice.Code = lastCode.ToString();
        }

        override public void MD_ClientSelect()
        {
            View.FW_SIN_Item_New_SelectClient floatWindow = new View.FW_SIN_Item_New_SelectClient(1);
            floatWindow.Show();
        }

        override public void MD_MovementAdd()
        {
            View.FW_SIN_Item_New_Movements floatWindow = new View.FW_SIN_Item_New_Movements();
            switch (((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).GetConfigValue(db.Configurations.Where(c => c.Name.Contains("LimiteStock")).First().ConfigurationID))
            {
                case 0:
                    floatWindow = new View.FW_SIN_Item_New_Movements();
                    break;

                case 1:
                    floatWindow = new View.FW_SIN_Item_New_Movements(Information["operationType"], movements);
                    break;
            }
            floatWindow.Show();
        }

        override public void MD_MovementEdit()
        {
            View.FW_SIN_Item_New_Movements floatWindow = new View.FW_SIN_Item_New_Movements(new Movement(movementSelected));
            floatWindow.Show();
        }

        override public Boolean CodeExist(string code)
        {
            List<SaleInvoice> purchaseDeliveries = db.SaleInvoices.ToList();
            foreach (var item in purchaseDeliveries)
            {
                if (item.Code.Contains(code) || code.Length == 0)
                {
                    CleanPurchaseCode();
                    return true;
                }
            }
            saleInvoice.Code = code;
            base.CodeExist(code);
            return false;
        }

        override public void SaveDocument()
        {
            saleInvoice.CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID;
            saleInvoice.ClientID = client.ClientID;
            saleInvoice.store = db.Stores.Where(s => s.StoreID == store.StoreID).First();
            saleInvoice.SaleInvoiceFinalPrice = documentContent.SaleFinalPrice;
            db.SaleInvoices.Add(saleInvoice);
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
                    a.MainFrame.Content = new SaleInvoiceMenu.Controller.CT_SaleInvoiceMenu();
                    break;
            }
        }
    }
}
