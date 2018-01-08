using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Sales.Nodes.SaleOrders.SaleOrderItem.SaleOrderItem_New.Controller
{
    public partial class CT_SOR_Item_New : Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New
    {
        public SaleOrder saleOrder;

        public CT_SOR_Item_New():base()
        {
            saleOrder = new SaleOrder();
            Information["operationType"] = 2;
            GetLastCode();
        }

        public void CleanPurchaseCode()
        {
            saleOrder.Code = "";
            TestMinimalInformation();
        }

        override public void SetDate(DateTime date)
        {
            saleOrder.Date = date;
            base.SetDate(date);
        }

        public override void SetMC(int i)
        {
            switch (i)
            {
                case 1:
                    MC_Page = new View.MC_SOR_Item_New_SaleOrder();
                    break;

                case 2:
                    MC_Page = new View.MC_SOR_Item_New_Movements();
                    break;
            }
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_SOR_Item_New_SaleOrder();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_SOR_Item_New_SaleOrder();
        }

        public override void SetSC()
        {
            SC_Page = new View.SC_SOR_Item_New_SaleOrder();
        }

        public override string GetCode()
        {
            return saleOrder.Code;
        }

        override public void GetLastCode()
        {
            if (db.SaleOrders.ToList().Count > 0)
            {
                lastCode = db.SaleOrders.OrderBy(u => u.SaleOrderID).Last().SaleOrderID + 1;
            }
            else
            {
                lastCode = 1;
            }

            saleOrder.Code = lastCode.ToString();
        }

        override public void MD_ClientSelect()
        {
            View.FW_SOR_Item_New_SelectClient floatWindow = new View.FW_SOR_Item_New_SelectClient(1);
            floatWindow.Show();
        }

        override public void MD_MovementAdd()
        {
            View.FW_SOR_Item_New_Movements floatWindow = new View.FW_SOR_Item_New_Movements();
            floatWindow.Show();
        }

        override public void MD_MovementEdit()
        {
            View.FW_SOR_Item_New_Movements floatWindow = new View.FW_SOR_Item_New_Movements(new Movement(movementSelected));
            floatWindow.Show();
        }

        override public Boolean CodeExist(string code)
        {
            List<SaleOrder> purchaseDeliveries = db.SaleOrders.ToList();
            foreach (var item in purchaseDeliveries)
            {
                if (item.Code.Contains(code) || code.Length == 0)
                {
                    CleanPurchaseCode();
                    return true;
                }
            }
            saleOrder.Code = code;
            base.CodeExist(code);
            return false;
        }

        override public void TestMinimalInformation()
        {
            if (saleOrder.Date != null && client.ClientID > 0 && store.StoreID > 0)
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
            saleOrder.CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID;
            saleOrder.ClientID = client.ClientID;
            saleOrder.store = db.Stores.Where(s => s.StoreID == store.StoreID).First();
            db.SaleOrders.Add(saleOrder);
            db.SaveChanges();

            foreach (Movement movement in movementsView.movements)
            {
                movement.MovementID = 0;
                movement.ConditionID = movement.condition.ConditionID;
                movement.condition = null;
                movement.ProductID = movement.product.ProductID;
                movement.product = null;
                movement.DocumentTypeID = db.DocumentTypes.Where(c => c.Name == "Order" && c.Input == 0).First().DocumentTypeID;
                movement.StoreID = store.StoreID;

                movement.DocumentID = saleOrder.SaleOrderID;
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
                    a.MainFrame.Content = new SaleOrderMenu.Controller.CT_SaleOrderMenu();
                    break;
            }
        }
    }
}
