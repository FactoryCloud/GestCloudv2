using FrameworkView.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryTransfer.Controller
{
    public class CT_PDE_Transfer : GestCloudv2.Documents.DCM_Transfers.Controller.CT_DCM_Transfers
    {
        public CT_PDE_Transfer()
        {
            itemsView = new PurchaseDeliveriesView();
        }

        public override void SetMC(int i)
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
    }
}
