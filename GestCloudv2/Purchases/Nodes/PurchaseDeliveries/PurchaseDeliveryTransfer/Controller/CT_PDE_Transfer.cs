using FrameworkDB.V1;
using FrameworkView.V1;
using GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryTransfer.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryTransfer.Controller
{
    public class CT_PDE_Transfer : GestCloudv2.Documents.DCM_Transfers.Controller.CT_DCM_Transfers
    {
        public List<PurchaseDelivery> Documents;
        public PurchaseInvoice purchaseInvoice;

        public CT_PDE_Transfer():base()
        {
            Documents = new List<PurchaseDelivery>();
            itemsView = new PurchaseDeliveriesView(Documents);
        }

        public override void SetMC()
        {
            MC_Page = new View.MC_PDE_Transfer();
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_PDE_Transfer();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_PDE_Transfer();
        }

        public override void EV_DocumentAdd()
        {
            FW_PDE_Deliveries floatWindow;
            if (Documents.Count() == 0)
                floatWindow = new FW_PDE_Deliveries(Documents, new Provider());

            else
                floatWindow = new FW_PDE_Deliveries(Documents, Documents[0].provider);

            floatWindow.Show();
        }

        public override void EV_PurchaseDeliveryAdd(PurchaseDelivery purchaseDelivery)
        {
            Documents.Add(db.PurchaseDeliveries.Where(p => p.PurchaseDeliveryID == purchaseDelivery.PurchaseDeliveryID).Include(p => p.provider).Include(e => e.provider.entity).First());
            UpdateComponents();
        }

        public override void GenerateTransfer()
        {
            if(purchaseInvoice == null)
            {
                purchaseInvoice = new PurchaseInvoice
                {
                    CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID,
                    ProviderID = Convert.ToInt32(Documents[0].ProviderID),
                    StoreID = db.Stores.Where(s => s.StoreID == Convert.ToInt32(Documents[0].StoreID)).First().StoreID,
                    //PurchaseInvoiceFinalPrice = documentContent.PurchaseFinalPrice,
                };

                db.PurchaseInvoices.Add(purchaseInvoice);
                db.SaveChanges();
            }

            decimal finalPrice = 0;
            foreach(PurchaseDelivery item in Documents)
            {
                finalPrice = finalPrice + item.PurchaseDeliveryFinalPrice;
                PurchaseDelivery temp = db.PurchaseDeliveries.Where(p => p.PurchaseDeliveryID == item.PurchaseDeliveryID).First();
                temp.PurchaseInvoiceID = purchaseInvoice.PurchaseInvoiceID;
                db.PurchaseDeliveries.Update(temp);
            }

            PurchaseInvoice final = db.PurchaseInvoices.Where(p => p.PurchaseInvoiceID == purchaseInvoice.PurchaseInvoiceID).First();
            final.PurchaseInvoiceFinalPrice = final.PurchaseInvoiceFinalPrice + finalPrice;
            db.PurchaseInvoices.Update(final);
        }
    }
}
