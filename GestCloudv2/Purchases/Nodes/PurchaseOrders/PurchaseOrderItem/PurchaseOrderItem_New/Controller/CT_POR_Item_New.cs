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
    public partial class CT_POR_Item_New : Main.Controller.CT_Common
    {
        public PurchaseOrder purchaseOrder;
        public int lastPurchaseOrderCode;
        public Movement movementSelected;
        public MovementsView movementsView;
        public Store store;
        public Provider provider;

        public Model.CT_Submenu CT_Submenu;

        public CT_POR_Item_New()
        {
            provider = new Provider();
            purchaseOrder = new PurchaseOrder();
            movementsView = new MovementsView();
            Information.Add("minimalInformation", 0);
            Information.Add("submenu", 0);
            Information.Add("submode", 0);
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public List<Company> GetCompanies()
        {
            return db.Companies.ToList();
        }

        public List<Store> GetStores()
        {
            List<Store> stores = new List<Store>();
            List<CompanyStore> companyStores = db.CompaniesStores.Where(c => c.CompanyID == ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID).Include(z => z.store).ToList();
            foreach (CompanyStore e in companyStores)
            {
                stores.Add(e.store);
            }
            return stores;
        }

        public void CleanOrderCode()
        {
            purchaseOrder.Code = "";
            TestMinimalInformation();
        }

        public void SetMovementSelected(int num)
        {
            movementSelected = movementsView.movements.Where(u => u.MovementID == num).First();
            UpdateComponents();
        }

        public void SetStore(int num)
        {
            store = db.Stores.Where(s => s.StoreID == num).First();
            TestMinimalInformation();
        }

        public void EV_ProductsSelect(object sender, RoutedEventArgs e)
        {

        }

        public void SetOrderDate(DateTime date)
        {
            purchaseOrder.Date = date;
            TestMinimalInformation();
        }


        public string GetLastOrderCode()
        {
            if (db.PurchaseOrders.ToList().Count > 0)
            {
                lastPurchaseOrderCode = db.PurchaseOrders.OrderBy(u => u.PurchaseOrderID).Last().PurchaseOrderID + 1;
            }
            else
            {
                lastPurchaseOrderCode = 1;
            }

            purchaseOrder.Code = lastPurchaseOrderCode.ToString();
            return lastPurchaseOrderCode.ToString();
        }

        public void EV_UpdateSubMenu(int num)
        {
            if (num == 0)
            {
                Information["submenu"] = 0;

                NV_Page = new View.NV_POR_Item_New_PurchaseOrder();
            }

            else
            {
                Information["submenu"] = 1;
                Information["submode"] = num;

                NV_Page = new View.NV_POR_Item_New_PurchaseOrder_Submenu();
            }

            TopSide.Content = NV_Page;
        }

        public void MD_ProviderSelect()
        {
            View.FW_POR_Item_New_SelectProvider floatWindow = new View.FW_POR_Item_New_SelectProvider();
            floatWindow.Show();
        }

        /*public void MD_StoredStock_Reduce()
        {
            View.FW_POR_Item_New_ReduceStock floatWindow = new View.FW_POR_Item_New_ReduceStock(1, movementsView.movements);
            floatWindow.Show();
        }*/

        public void MD_StoredStock_Increase()
        {
            View.FW_POR_Item_New_IncreaseStock floatWindow = new View.FW_POR_Item_New_IncreaseStock(1, movementsView.movements);
            floatWindow.Show();
        }

        public void MD_StoredStock_Remove()
        {
            movementsView.MovementDelete(movementSelected.MovementID);
            movementSelected = null;
            UpdateComponents();
        }

        public override void EV_SetProvider(int num)
        {
            provider = db.Providers.Where(p => p.ProviderID == num).Include(e => e.entity).First();
            MC_Page = new View.MC_POR_Item_New_PurchaseOrder();
            MainContent.Content = MC_Page;

            EV_UpdateSubMenu(4);
        }

        public override void EV_MovementAdd(Movement movement)
        {
            movement.MovementID = movementsView.MovementNextID();
            movementsView.MovementAdd(movement);
            movementSelected = null;
            UpdateComponents();
        }

        public Boolean PurchaseOrderExist(string test)
        {
            List<PurchaseOrder> purchaseOrders = db.PurchaseOrders.ToList();
            foreach (var item in purchaseOrders)
            {
                if (item.Code.Contains(test) || test.Length == 0)
                {
                    CleanOrderCode();
                    return true;
                }
            }
            purchaseOrder.Code = test;
            TestMinimalInformation();
            return false;
        }

        public override void EV_ActivateSaveButton(bool verificated)
        {
            if (verificated)
            {
                Information["entityValid"] = 1;
            }

            else
            {
                Information["entityValid"] = 0;
            }

            TestMinimalInformation();
        }

        private void TestMinimalInformation()
        {
            if (purchaseOrder.Date != null && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }


            if (Information["mode"] == 1)
                TS_Page = new View.TS_POR_Item_New_PurchaseOrder(Information["minimalInformation"]);

            if (Information["mode"] == 2)
                TS_Page = new View.TS_POR_Item_New_PurchaseOrder_Movements(Information["minimalInformation"]);

            LeftSide.Content = TS_Page;
        }

        public void SaveNewStockAdjust()
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

        public void CT_Menu()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        override public void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    if(Information["submenu"] == 0)
                        NV_Page = new View.NV_POR_Item_New_PurchaseOrder();
                    else
                        NV_Page = new View.NV_POR_Item_New_PurchaseOrder_Submenu();
                    TS_Page = new View.TS_POR_Item_New_PurchaseOrder(Information["minimalInformation"]);
                    MC_Page = new View.MC_POR_Item_New_PurchaseOrder();
                    ChangeComponents();
                    break;

                case 2:
                    if (Information["submenu"] == 0)
                        NV_Page = new View.NV_POR_Item_New_PurchaseOrder();
                    else
                        NV_Page = new View.NV_POR_Item_New_PurchaseOrder_Submenu();
                    TS_Page = new View.TS_POR_Item_New_PurchaseOrder_Movements(Information["minimalInformation"]);
                    MC_Page = new View.MC_POR_Item_New_PurchaseOrder_Movements();
                    ChangeComponents();
                    break;
            }
        }

        private void ChangeController()
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

                case 1:
                    /*MainWindow b = (MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new Main.Controller.MainController();*/
                    break;
            }
        }

        public void ControlFieldChangeButton(bool verificated)
        {
            TestMinimalInformation();
        }
    }
}
