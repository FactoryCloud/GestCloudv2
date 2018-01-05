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

        public override void SetSC()
        {
            SC_Page = new View.SC_SDE_Item_New_SaleDelivery();
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

        override public void MD_ClientSelect()
        {
            View.FW_SDE_Item_New_SelectClient floatWindow = new View.FW_SDE_Item_New_SelectClient(1);
            floatWindow.Show();
        }

        override public void MD_MovementAdd()
        {
            View.FW_SDE_Item_New_MovementAdd floatWindow = new View.FW_SDE_Item_New_MovementAdd(1, movementsView.movements);
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

        override public void TestMinimalInformation()
        {
            if (saleDelivery.Date != null && client.ClientID > 0 && store.StoreID > 0)
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
            saleDelivery.CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID;
            saleDelivery.ClientID = client.ClientID;
            db.SaleDeliveries.Add(saleDelivery);
            db.SaveChanges();

            foreach (Movement movement in movementsView.movements)
            {
                movement.MovementID = 0;
                movement.ConditionID = movement.condition.ConditionID;
                movement.condition = null;
                movement.ProductID = movement.product.ProductID;
                movement.product = null;
                movement.DocumentTypeID = db.DocumentTypes.Where(c => c.Name == "Delivery" && c.Input == 0).First().DocumentTypeID;
                movement.StoreID = store.StoreID;

                movement.DocumentID = saleDelivery.SaleDeliveryID;
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
                    a.MainFrame.Content = new SaleDeliveryMenu.Controller.CT_SaleDeliveryMenu();
                    break;
            }
        }
    }
}
