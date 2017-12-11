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
    public partial class CT_SOR_Item_New : Main.Controller.CT_Common
    {
        public int lastSaleOrderCod;
        public Movement movementSelected;
        public MovementsView movementsView;
        public Store store;
        public ClientsView clientsView;
        public SaleOrder saleOrder;
        public Client client;

        public CT_SOR_Item_New()
        {
            saleOrder = new SaleOrder();
            client = new Client();
            movementsView = new MovementsView();
            clientsView = new ClientsView();
            Information.Add("minimalInformation", 0);
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public override void SetSubmenu(int option)
        {
            switch (option)
            {
                case 4:
                    CT_Submenu = new Model.CT_Submenu(store, option);
                    break;

                case 6:
                    CT_Submenu = new Model.CT_Submenu(client, option);
                    break;
            }

            NV_Page = new View.NV_SOR_Item_New_SaleOrder();
            TopSide.Content = NV_Page;
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

        public void CleanSaleOrderCode()
        {
            saleOrder.Code = "";
            TestMinimalInformation();
        }

        public void SetMovementSelected(int num)
        {
            movementSelected = movementsView.movements.Where(u => u.MovementID == num).First();
            if (Information["mode"] == 1)
                TS_Page = new View.TS_SOR_Item_New_SaleOrder(Information["minimalInformation"]);

            if (Information["mode"] == 2)
                TS_Page = new View.TS_SOR_Item_New_SaleOrder_Movements(Information["minimalInformation"]);

            LeftSide.Content = TS_Page;
        }


        public void SetStore(int num)
        {
            store = db.Stores.Where(s => s.StoreID == num).First();
            TestMinimalInformation();
        }

        public void EV_ProductsSelect(object sender, RoutedEventArgs e)
        {

        }

        public void SetAdjustDate(DateTime date)
        {
            saleOrder.Date = date;
            TestMinimalInformation();
        }

        public int LastSaleOrderCod()
        {
            if (db.SaleOrders.ToList().Count > 0)
            {
                lastSaleOrderCod = db.SaleOrders.OrderBy(u => u.SaleOrderID).Last().SaleOrderID+ 1;
                saleOrder.Code = lastSaleOrderCod.ToString();
                return lastSaleOrderCod;
            }
            else
            {
                saleOrder.Code = $"1";
                lastSaleOrderCod = 1;
                return lastSaleOrderCod;
            }
        }

        public void MD_ClientSelect()
        {
            View.FW_SOR_Item_New_SelectClient floatWindow = new View.FW_SOR_Item_New_SelectClient(1, clientsView.clients);
            floatWindow.Show();
        }

        public override void EV_SetClient(int num)
        {
            client = db.Clients.Where(p => p.ClientID == num).Include(e => e.entity).First();
            EV_UpdateSubMenu(6);
            MC_Page = new View.MC_SOR_Item_New_SaleOrder();
            MainContent.Content = MC_Page;
        }

        public void MD_StoredStock_Increase()
        {
            View.FW_SOR_Item_New_IncreaseStock floatWindow = new View.FW_SOR_Item_New_IncreaseStock(1, movementsView.movements);
            floatWindow.Show();
        }

        /* public void MD_StoredStock_Remove()
        {
            movementsView.MovementDelete(movementSelected.MovementID);
            movementSelected = null;
            UpdateComponents();
        }

       public void MD_StoredStock_Edit()
        {
            View.FW_POR_Item_New_IncreaseStock floatWindow = new View.FW_POR_Item_New_IncreaseStock(1, movementsView.movements, movementSelected.MovementID);
            floatWindow.Show();
        }*/

        public override void EV_MovementAdd(Movement movement)
        {
            movement.MovementID = movementsView.MovementNextID();
            movementsView.MovementAdd(movement);
            movementSelected = null;
            UpdateComponents();
        }

        public Boolean SaleOrderExist(string saleOrders)
        {
            List<SaleOrder> orders = db.SaleOrders.ToList();
            foreach (var item in orders)
            {
                if (item.Code.Contains(saleOrders) || saleOrders.Length == 0)
                {
                    CleanSaleOrderCode();
                    return true;
                }
            }
            saleOrder.Code = saleOrders;
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
            if (saleOrder.Date != null && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            if (Information["mode"] == 1)
                TS_Page = new View.TS_SOR_Item_New_SaleOrder(Information["minimalInformation"]);

            if (Information["mode"] == 2)
                TS_Page = new View.TS_SOR_Item_New_SaleOrder_Movements(Information["minimalInformation"]);

            LeftSide.Content = TS_Page;
        }

        public void SaveNewStockAdjust()
        {
            saleOrder.CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID;
            saleOrder.ClientID = client.ClientID;
            db.SaleOrders.Add(saleOrder);
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
                    movement.DocumentTypeID = db.DocumentTypes.Where(c => c.Name == "Order" && c.Input == 0).First().DocumentTypeID;
                    movement.StoreID = store.StoreID;
                }

                else
                {
                    movement.DocumentTypeID = movement.documentType.DocumentTypeID;
                    movement.documentType = null;
                    if (movement.Quantity < 0)
                        movement.Quantity = movement.Quantity * -1;
                }

                movement.DocumentID = saleOrder.SaleOrderID;
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
                    NV_Page = new View.NV_SOR_Item_New_SaleOrder();
                    TS_Page = new View.TS_SOR_Item_New_SaleOrder(Information["minimalInformation"]);
                    MC_Page = new View.MC_SOR_Item_New_SaleOrder();
                    SC_Page = new View.SC_SOR_Item_New_SaleOrder();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new View.NV_SOR_Item_New_SaleOrder();
                    TS_Page = new View.TS_SOR_Item_New_SaleOrder_Movements(Information["minimalInformation"]);
                    MC_Page = new View.MC_SOR_Item_New_SaleOrder_Movements();
                    SC_Page = new View.SC_SOR_Item_New_SaleOrder();
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
                    a.MainFrame.Content = new Sales.Nodes.SaleOrders.SaleOrderMenu.Controller.CT_SaleOrderMenu();
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
