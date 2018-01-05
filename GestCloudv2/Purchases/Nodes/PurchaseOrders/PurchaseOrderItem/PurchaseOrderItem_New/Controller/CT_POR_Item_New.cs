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
            View.FW_POR_Item_New_MovementAdd floatWindow = new View.FW_POR_Item_New_MovementAdd(1, movementsView.movements);
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

        override public void TestMinimalInformation()
        {
            if (purchaseOrder.Date != null && provider.ProviderID > 0 && store.StoreID > 0)
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
            purchaseOrder.CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID;
            purchaseOrder.ProviderID = provider.ProviderID;
            db.PurchaseOrders.Add(purchaseOrder);
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
                    movement.DocumentTypeID = db.DocumentTypes.Where(c => c.Name == "Order" && c.Input == 1).First().DocumentTypeID;
                    movement.StoreID = store.StoreID;
                }

                else
                {
                    movement.DocumentTypeID = movement.documentType.DocumentTypeID;
                    movement.documentType = null;
                    if (movement.Quantity < 0)
                        movement.Quantity = movement.Quantity * -1;
                }

                movement.DocumentID = purchaseOrder.PurchaseOrderID;
                db.Movements.Add(movement);
            }

            db.SaveChanges();
            MessageBox.Show("Datos guardados correctamente");

            Information["fieldEmpty"] = 0;
            CT_Menu();
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
