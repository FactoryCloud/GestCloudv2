using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Documents.DCM_Items.DCM_Item_New.Controller
{
    public partial class CT_DCM_Item_New : Main.Controller.CT_Common
    {
        public int lastCode;
        public Movement movementSelected;
        public MovementsView movementsView;
        public Store store;
        public Provider provider;

        public CT_DCM_Item_New()
        {
            provider = new Provider();
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

            SetNV();
            TopSide.Content = NV_Page;
        }

        virtual public List<Store> GetStores()
        {
            List<Store> stores = new List<Store>();
            List<CompanyStore> companyStores = db.CompaniesStores.Where(c => c.CompanyID == ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID).Include(z => z.store).ToList();
            foreach (CompanyStore e in companyStores)
            {
                stores.Add(e.store);
            }
            return stores;
        }

        virtual public DateTime GetDate()
        {
           return DateTime.Today;
        }

        virtual public string GetCode()
        {
            return "0";
        }

        virtual public void CleanCode()
        {
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

        virtual public void SetDate(DateTime date)
        {
            TestMinimalInformation();
        }


        virtual public void GetLastCode()
        {
        }

        virtual public void MD_ProviderSelect()
        {
        }

        virtual public void MD_MovementAdd()
        {
        }

        public void MD_MovementDelete()
        {
            movementsView.MovementDelete(movementSelected.MovementID);
            movementSelected = null;
            UpdateComponents();
        }

        public override void EV_SetProvider(int num)
        {
            provider = db.Providers.Where(p => p.ProviderID == num).Include(e => e.entity).First();
            EV_UpdateSubMenu(7);
            SetMC(1);
            MainContent.Content = MC_Page;
        }

        public override void EV_MovementAdd(Movement movement)
        {
            movement.MovementID = movementsView.MovementNextID();
            movementsView.MovementAdd(movement);
            movementSelected = null;
            UpdateComponents();
        }

        public virtual Boolean CodeExist(string test)
        {
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

        public virtual void TestMinimalInformation()
        {
            SetTS();

            LeftSide.Content = TS_Page;
        }

        public virtual void SaveDocument()
        {
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

        public virtual void SetMC(int i)
        {

        }

        public virtual void SetTS()
        {

        }

        public virtual void SetNV()
        {

        }

        override public void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    SetNV();
                    SetTS();
                    SetMC(1);
                    ChangeComponents();
                    break;

                case 2:
                    SetNV();
                    SetTS();
                    SetMC(2);
                    ChangeComponents();
                    break;
            }
        }

        virtual public void ChangeController()
        {
        }

        public void ControlFieldChangeButton(bool verificated)
        {
            TestMinimalInformation();
        }
    }
}
