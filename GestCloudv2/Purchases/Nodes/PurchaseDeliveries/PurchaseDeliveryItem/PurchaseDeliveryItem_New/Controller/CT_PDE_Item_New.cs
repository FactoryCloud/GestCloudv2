using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryItem.PurchaseDeliveryItem_New.Controller
{
    public partial class CT_PDE_Item_New : Main.Controller.CT_Common
    {
        public PurchaseDelivery purchaseDelivery;
        public int lastPurchaseDeliveryCode;
        public Movement movementSelected;
        public MovementsView movementsView;
        public Store store;
        public Provider provider;

        public CT_PDE_Item_New()
        {
            provider = new Provider();
            purchaseDelivery = new PurchaseDelivery();
            movementsView = new MovementsView();
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

                case 7:
                    CT_Submenu = new Model.CT_Submenu(provider, option);
                    break;
            }

            NV_Page = new View.NV_PDE_Item_New_PurchaseDelivery();
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

        public void CleanPurchaseCode()
        {
            purchaseDelivery.Code = "";
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

        public void SetDeliveryDate(DateTime date)
        {
            purchaseDelivery.Date = date;
            TestMinimalInformation();
        }


        public string GetLastDeliveryCode()
        {
            if (db.PurchaseDeliveries.ToList().Count > 0)
            {
                lastPurchaseDeliveryCode = db.PurchaseDeliveries.OrderBy(u => u.PurchaseDeliveryID).Last().PurchaseDeliveryID + 1;
            }
            else
            {
                lastPurchaseDeliveryCode = 1;
            }

            purchaseDelivery.Code = lastPurchaseDeliveryCode.ToString();
            return lastPurchaseDeliveryCode.ToString();
        }

        public void MD_ProviderSelect()
        {
            View.FW_PDE_Item_New_SelectProvider floatWindow = new View.FW_PDE_Item_New_SelectProvider();
            floatWindow.Show();
        }

        public void MD_StoredStock_Increase()
        {
            View.FW_PDE_Item_New_IncreaseStock floatWindow = new View.FW_PDE_Item_New_IncreaseStock(1, movementsView.movements);
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
            EV_UpdateSubMenu(7);
            MC_Page = new View.MC_PDE_Item_New_PurchaseDelivery();
            MainContent.Content = MC_Page;
        }

        public override void EV_MovementAdd(Movement movement)
        {
            movement.MovementID = movementsView.MovementNextID();
            movementsView.MovementAdd(movement);
            movementSelected = null;
            UpdateComponents();
        }

        public Boolean PurchaseDeliveryExist(string test)
        {
            List<PurchaseDelivery> purchaseDeliveries = db.PurchaseDeliveries.ToList();
            foreach (var item in purchaseDeliveries)
            {
                if (item.Code.Contains(test) || test.Length == 0)
                {
                    CleanPurchaseCode();
                    return true;
                }
            }
            purchaseDelivery.Code = test;
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
            if (purchaseDelivery.Date != null && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }


            if (Information["mode"] == 1)
                TS_Page = new View.TS_PDE_Item_New_PurchaseDelivery(Information["minimalInformation"]);

            if (Information["mode"] == 2)
                TS_Page = new View.TS_PDE_Item_New_PurchaseDelivery_Movements(Information["minimalInformation"]);

            LeftSide.Content = TS_Page;
        }

        public void SaveNewPurchaseDelivery()
        {
            purchaseDelivery.CompanyID = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID;
            purchaseDelivery.ProviderID = provider.ProviderID;
            db.PurchaseDeliveries.Add(purchaseDelivery);
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
                    movement.DocumentTypeID = db.DocumentTypes.Where(c => c.Name == "Delivery" && c.Input == 1).First().DocumentTypeID;
                    movement.StoreID = store.StoreID;
                }

                else
                {
                    movement.DocumentTypeID = movement.documentType.DocumentTypeID;
                    movement.documentType = null;
                    if (movement.Quantity < 0)
                        movement.Quantity = movement.Quantity * -1;
                }

                movement.DocumentID = purchaseDelivery.PurchaseDeliveryID;
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
                    NV_Page = new View.NV_PDE_Item_New_PurchaseDelivery();
                    TS_Page = new View.TS_PDE_Item_New_PurchaseDelivery(Information["minimalInformation"]);
                    MC_Page = new View.MC_PDE_Item_New_PurchaseDelivery();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new View.NV_PDE_Item_New_PurchaseDelivery();
                    TS_Page = new View.TS_PDE_Item_New_PurchaseDelivery_Movements(Information["minimalInformation"]);
                    MC_Page = new View.MC_PDE_Item_New_PurchaseDelivery_Movements();
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
                    a.MainFrame.Content = new Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryMenu.Controller.CT_PurchaseDeliveryMenu();
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
