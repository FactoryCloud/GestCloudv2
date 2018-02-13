using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderTransfer.POR_Transfer_Delivery.Controller
{
    public class CT_POR_Transfer_Delivery : GestCloudv2.Documents.DCM_Transfers.Controller.CT_DCM_Transfers
    {
        public List<PurchaseOrder> Documents;

        public CT_POR_Transfer_Delivery():base()
        {
            Documents = new List<PurchaseOrder>();
            itemsView = new PurchaseOrdersView(Documents);
        }

        public override void SetMC(int i)
        {
            MC_Page = new View.MC_POR_Transfer_Delivery();
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_POR_Transfer_Delivery();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_POR_Transfer_Delivery();
        }

        public override Main.Controller.CT_Common SetItemOriginal()
        {
            return new Purchases.Controller.CT_Purchases();
        }

        public override int GetDocumentsCount()
        {
            return Documents.Count;
        }

        public override string GetInvoiceCode()
        {
            return purchaseInvoice.Code;
        }

        public override string GetDeliveryCode()
        {
            return purchaseDelivery.Code;
        }

        public override bool DeliveryExist()
        {
            if (purchaseDelivery != null)
                return true;

            else
                return false;
        }

        public override void EV_DocumentAdd()
        {
            View.FW_POR_Transfer_Delivery_Orders floatWindow;
            if (Documents.Count() == 0)
                floatWindow = new View.FW_POR_Transfer_Delivery_Orders(Documents, new Provider());

            else
                floatWindow = new View.FW_POR_Transfer_Delivery_Orders(Documents, Documents[0].provider);

            floatWindow.Show();
        }

        public override void EV_PurchaseDelivery()
        {
            View.FW_POR_Transfer_Delivery_Deliveries floatWindow = new View.FW_POR_Transfer_Delivery_Deliveries(Documents[0].provider);
            floatWindow.Show();
        }

        public override void EV_PurchaseOrderAdd(int num)
        {
            Documents.Add(db.PurchaseOrders.Where(p => p.PurchaseOrderID == num).Include(p => p.provider).Include(e => e.provider.entity).First());
            UpdateComponents();
        }

        public override void GenerateTransfer()
        {
            if(purchaseDelivery.Equals(null))
            {
                int code;

                if (db.PurchaseDeliveries.Where(p => p.Code != null).Count() > 0)
                    code = Convert.ToInt32(db.PurchaseDeliveries.Where(p => p.Code != null).OrderBy(p => p.Code).Last().Code) + 1;

                else
                    code = 1;

                purchaseDelivery = new PurchaseDelivery
                {
                    CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID,
                    ProviderID = Convert.ToInt32(Documents[0].ProviderID),
                    StoreID = db.Stores.Where(s => s.StoreID == Convert.ToInt32(Documents[0].StoreID)).First().StoreID,
                    Date = DateTime.Today,
                    Code = $"{DateTime.Today.ToString("yy")}/{code}"
                };

                db.PurchaseDeliveries.Add(purchaseDelivery);
                db.SaveChanges();
            }

            decimal finalPrice = 0;
            foreach(PurchaseOrder item in Documents)
            {
                finalPrice = finalPrice + item.PurchaseOrderFinalPrice;
                PurchaseOrder temp = db.PurchaseOrders.Where(p => p.PurchaseOrderID == item.PurchaseOrderID).First();
                temp.PurchaseDeliveryID = purchaseDelivery.PurchaseDeliveryID;
                db.PurchaseOrders.Update(temp);
            }

            PurchaseDelivery final = db.PurchaseDeliveries.Where(p => p.PurchaseDeliveryID == purchaseDelivery.PurchaseDeliveryID).First();
            final.PurchaseDeliveryFinalPrice = final.PurchaseDeliveryFinalPrice + finalPrice;
            db.PurchaseDeliveries.Update(final);

            base.GenerateTransfer();
        }
    }
}
