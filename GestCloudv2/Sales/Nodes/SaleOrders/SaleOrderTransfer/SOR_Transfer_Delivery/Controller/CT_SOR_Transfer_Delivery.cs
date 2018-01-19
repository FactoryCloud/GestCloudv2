using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestCloudv2.Sales.Nodes.SaleOrders.SaleOrderTransfer.SOR_Transfer_Delivery.Controller
{
    public class CT_SOR_Transfer_Delivery : GestCloudv2.Documents.DCM_Transfers.Controller.CT_DCM_Transfers
    {
        public List<SaleOrder> Documents;

        public CT_SOR_Transfer_Delivery():base()
        {
            Documents = new List<SaleOrder>();
            itemsView = new SaleOrdersView(Documents);
        }

        public override void SetMC()
        {
            MC_Page = new View.MC_SOR_Transfer_Delivery();
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_SOR_Transfer_Delivery();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_SOR_Transfer_Delivery();
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

        public override string GetDeliveryCode()
        {
            return saleDelivery.Code;
        }

        public override bool DeliveryExist()
        {
            if (saleDelivery != null)
                return true;

            else
                return false;
        }

        public override void EV_DocumentAdd()
        {
            View.FW_SOR_Transfer_Delivery_Orders floatWindow;
            if (Documents.Count() == 0)
                floatWindow = new View.FW_SOR_Transfer_Delivery_Orders(Documents, new Client());

            else
                floatWindow = new View.FW_SOR_Transfer_Delivery_Orders(Documents, Documents[0].client);

            floatWindow.Show();
        }

        public override void EV_SaleDelivery()
        {
            View.FW_SOR_Transfer_Delivery_Deliveries floatWindow = new View.FW_SOR_Transfer_Delivery_Deliveries(Documents[0].client);
            floatWindow.Show();
        }

        public override void EV_SaleOrderAdd(int num)
        {
            Documents.Add(db.SaleOrders.Where(p => p.SaleOrderID == num).Include(p => p.client).Include(e => e.client.entity).First());
            UpdateComponents();
        }

        public override void GenerateTransfer()
        {
            if(saleDelivery == null)
            {
                int code = Convert.ToInt32(db.SaleDeliveries.Where(p => p.Code != null).OrderBy(p => p.Code).Last().Code) + 1;
                saleDelivery = new SaleDelivery
                {
                    CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID,
                    ClientID = Convert.ToInt32(Documents[0].ClientID),
                    StoreID = db.Stores.Where(s => s.StoreID == Convert.ToInt32(Documents[0].StoreID)).First().StoreID,
                    Date = DateTime.Today,
                    Code = $"{DateTime.Today.ToString("yy")}/{code}"
                };

                db.SaleDeliveries.Add(saleDelivery);
                db.SaveChanges();
            }

            decimal finalPrice = 0;
            foreach(SaleOrder item in Documents)
            {
                finalPrice = finalPrice + item.SaleOrderFinalPrice;
                SaleOrder temp = db.SaleOrders.Where(p => p.SaleOrderID == item.SaleOrderID).First();
                temp.SaleDeliveryID = saleDelivery.SaleDeliveryID;
                db.SaleOrders.Update(temp);
            }

            SaleDelivery final = db.SaleDeliveries.Where(p => p.SaleDeliveryID == saleDelivery.SaleDeliveryID).First();
            final.SaleDeliveryFinalPrice = final.SaleDeliveryFinalPrice + finalPrice;
            db.SaleDeliveries.Update(final);

            base.GenerateTransfer();
        }
    }
}
