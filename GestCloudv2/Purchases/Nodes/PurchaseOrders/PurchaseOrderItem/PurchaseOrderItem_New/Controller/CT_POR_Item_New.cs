using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderItem.PurchaseOrderItem_New.Controller
{
    public partial class CT_POR_Item_New : Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New
    {
        public PurchaseOrder purchaseOrder;

        public CT_POR_Item_New():base()
        {
            purchaseOrder = new PurchaseOrder();
            purchaseOrder.Date = DateTime.Today;
            GetLastCode();
            Information["operationType"] = 1;
        }

        override public void CleanCode()
        {
            purchaseOrder.Code = "";
            TestMinimalInformation();
        }

        override public void SetDate(DateTime date)
        {
            purchaseOrder.Date = date;
            base.SetDate(date);
        }

        override public void SetMC(int i)
        {
            switch (i)
            {
                case 1:
                    MC_Page = new View.MC_POR_Item_New_PurchaseOrder();
                    break;

                case 2:
                    MC_Page = new View.MC_POR_Item_New_Movements();
                    break;

                case 3:
                    MC_Page = new View.MC_POR_Item_New_Summary();
                    break;
            }
        }

        override public void SetTS()
        {
            TS_Page = new View.TS_POR_Item_New_PurchaseOrder();
        }

        override public void SetNV()
        {
            NV_Page = new View.NV_POR_Item_New_PurchaseOrder();
        }

        public override void SetSC()
        {
            SC_Page = new View.SC_POR_Item_New_PurchaseOrder();
        }

        public override int GetDocumentID()
        {
            return purchaseOrder.PurchaseOrderID;
        }

        public override DocumentType GetDocumentType()
        {
            return db.DocumentTypes.Where(d => d.Input == 1 && d.Name.Contains("Order")).First();
        }

        public override DateTime GetDate()
        {
            return (DateTime)purchaseOrder.Date;
        }

        public override string GetCode()
        {
            return $"{purchaseOrder.Code}";
        }

        override public void GetLastCode()
        {
            if (db.PurchaseOrders.ToList().Count > 0)
            {
                lastCode = db.PurchaseOrders.OrderBy(u => u.PurchaseOrderID).Last().PurchaseOrderID + 1;
            }
            else
            {
                lastCode = 1;
            }

            purchaseOrder.Code = lastCode.ToString();
        }

        override public void MD_ProviderSelect()
        {
            View.FW_POR_Item_New_SelectProvider floatWindow = new View.FW_POR_Item_New_SelectProvider();
            floatWindow.Show();
        }

        override public void MD_MovementAdd()
        {
            View.FW_POR_Item_New_Movements floatWindow = new View.FW_POR_Item_New_Movements();
            floatWindow.Show();
        }

        override public void MD_MovementEdit()
        {
            View.FW_POR_Item_New_Movements floatWindow = new View.FW_POR_Item_New_Movements(new Movement(movementSelected));
            floatWindow.Show();
        }

        override public Boolean CodeExist(string test)
        {
            List<PurchaseOrder> purchaseOrders = db.PurchaseOrders.ToList();
            foreach (var item in purchaseOrders)
            {
                if (item.Code.Contains(test) || test.Length == 0)
                {
                    CleanCode();
                    return true;
                }
            }
            purchaseOrder.Code = test;
            TestMinimalInformation();
            return false;
        }

        override public void SaveDocument()
        {
            purchaseOrder.CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID;
            purchaseOrder.ProviderID = provider.ProviderID;
            purchaseOrder.store = db.Stores.Where(s => s.StoreID == store.StoreID).First();
            db.PurchaseOrders.Add(purchaseOrder);
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
                    a.MainFrame.Content = new Purchases.Nodes.PurchaseOrders.PurchaseOrderMenu.Controller.CT_PurchaseOrderMenu();
                    break;
            }
        }
    }
}
