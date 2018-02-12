using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestCloudv2.Sales.Nodes.SaleDeliveries.SaleDeliveryTransfer.Controller
{
    public class CT_SDE_Transfer : GestCloudv2.Documents.DCM_Transfers.Controller.CT_DCM_Transfers
    {
        public List<SaleDelivery> Documents;

        public CT_SDE_Transfer():base()
        {
            Documents = new List<SaleDelivery>();
            itemsView = new SaleDeliveriesView(Documents);
        }

        public override void SetMC(int i)
        {
            MC_Page = new View.MC_SDE_Transfer();
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_SDE_Transfer();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_SDE_Transfer();
        }

        public override Main.Controller.CT_Common SetItemOriginal()
        {
            return new Sales.Controller.CT_Sales();
        }

        public override int GetDocumentsCount()
        {
            return Documents.Count;
        }

        public override string GetInvoiceCode()
        {
            return saleInvoice.Code;
        }

        public override bool InvoiceExist()
        {
            if (saleInvoice != null)
                return true;

            else
                return false;
        }

        public override void EV_DocumentAdd()
        {
            View.FW_SDE_Deliveries floatWindow;
            if (Documents.Count() == 0)
                floatWindow = new View.FW_SDE_Deliveries(Documents, new Client());

            else
                floatWindow = new View.FW_SDE_Deliveries(Documents, Documents[0].client);

            floatWindow.Show();
        }

        public override void EV_SaleInvoice()
        {
            View.FW_SDE_Invoices floatWindow = new View.FW_SDE_Invoices(Documents[0].client);
            floatWindow.Show();
        }

        public override void EV_SaleDeliveryAdd(int saleDelivery)
        {
            Documents.Add(db.SaleDeliveries.Where(p => p.SaleDeliveryID == saleDelivery).Include(p => p.client).Include(e => e.client.entity).First());
            UpdateComponents();
        }

        public override void GenerateTransfer()
        {
            if(saleInvoice == null)
            {
                int code;

                if (db.SaleInvoices.Where(p => p.Code != null).Count() > 0)
                    code = Convert.ToInt32(db.SaleInvoices.Where(p => p.Code != null).OrderBy(p => p.Code).Last().Code) + 1;

                else
                    code = 1;

                saleInvoice = new SaleInvoice
                {
                    CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID,
                    ClientID = Convert.ToInt32(Documents[0].ClientID),
                    StoreID = db.Stores.Where(s => s.StoreID == Convert.ToInt32(Documents[0].StoreID)).First().StoreID,
                    Date = DateTime.Today,
                    Code = $"{DateTime.Today.ToString("yy")}/{code}"
                };

                db.SaleInvoices.Add(saleInvoice);
                db.SaveChanges();
            }

            decimal finalPrice = 0;
            foreach(SaleDelivery item in Documents)
            {
                finalPrice = finalPrice + item.SaleDeliveryFinalPrice;
                SaleDelivery temp = db.SaleDeliveries.Where(p => p.SaleDeliveryID == item.SaleDeliveryID).First();
                temp.SaleInvoiceID = saleInvoice.SaleInvoiceID;
                db.SaleDeliveries.Update(temp);
            }

            SaleInvoice final = db.SaleInvoices.Where(p => p.SaleInvoiceID == saleInvoice.SaleInvoiceID).First();
            final.SaleInvoiceFinalPrice = final.SaleInvoiceFinalPrice + finalPrice;
            db.SaleInvoices.Update(final);

            base.GenerateTransfer();
        }
    }
}
