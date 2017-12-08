using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace GestCloudv2.Files.Nodes.Stores.StoreItem.StoreItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_STR_Item_Load.xaml
    /// </summary>
    public partial class CT_STR_Item_Load : Main.Controller.CT_Common
    {
        public Store store;
        public List<Company> companies;
        public SubmenuItems submenuItems;

        public CT_STR_Item_Load(Store store, int editable)
        {
            submenuItems = new SubmenuItems();
            companies = new List<Company>();
            Information.Add("editable", editable);
            Information.Add("old_editable", 0);
            Information.Add("minimalInformation", 0);
            Information.Add("external", 0);
            Information["entityValid"] = 1;

            foreach (CompanyStore compsto in db.CompaniesStores.Where(c => c.StoreID == store.StoreID).Include(c => c.company))
            {
                companies.Add(compsto.company);
            }

            Information["editable"] = editable;
            this.store = store;
        }

        public CT_STR_Item_Load(Store store, int editable, int external):base(external)
        {
            submenuItems = new SubmenuItems();
            companies = new List<Company>();
            Information.Add("editable", editable);
            Information.Add("old_editable", 0);
            Information.Add("minimalInformation", 0);
            Information.Add("external", 1);
            Information["entityValid"] = 1;

            foreach (CompanyStore compsto in db.CompaniesStores.Where(c => c.StoreID == store.StoreID).Include(c => c.company))
            {
                companies.Add(compsto.company);
            }

            Information["editable"] = editable;
            this.store = store;
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
            return db.Stores.ToList();
        }

        public void SetStoreName(string name)
        {
            store.Name = name;
            TestMinimalInformation();
        }

        public void SetStoreCode(int code)
        {
            store.Code = code;
            TestMinimalInformation();
        }

        public void UpdateCompanies(int num)
        {
            if(companies.Contains(db.Companies.Where(s => s.CompanyID == num).First()))
            {
                companies.Remove(db.Companies.Where(c => c.CompanyID == num).Include(c => c.CompaniesStores).First());
            }

            else
            {
                companies.Add(db.Companies.Where(c => c.CompanyID == num).Include(c => c.CompaniesStores).First());
            }
            TestMinimalInformation();
        }

        public override void EV_ActivateSaveButton(bool verificated)
        {
            if(verificated)
            {
                Information["entityValid"] = 1;
            }

            else
            {
                Information["entityValid"] = 0;
            }

            TestMinimalInformation();
        }

        public void CleanName()
        {
            store.Name = "";
            TestMinimalInformation();
        }

        public Boolean CompanyControlExist(string name)
        {
            List<Company> companies = db.Companies.ToList();
            foreach (var item in companies)
            {
                if ((item.Name.ToLower() == name.ToLower() && store.Name.ToLower() != name.ToLower()) || name.Length == 0)
                {
                    CleanName();
                    return true;
                }
            }
            store.Name = name;
            TestMinimalInformation();
            return false;
        }

        private void TestMinimalInformation()
        {
            if(store.Name.Length > 0 && store.Code > 0 && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            if (Information["editable"] == 0)
                TS_Page = new View.TS_STR_Item_Load(Information["minimalInformation"], Information["external"]);
            else
                TS_Page = new View.TS_STR_Item_Load_Edit(Information["minimalInformation"], Information["external"]);
            LeftSide.Content = TS_Page;
        }

        public void SaveNewStore()
        {
            Store store1 = db.Stores.Where(c => c.StoreID == store.StoreID).First();
            store1.Code = store.Code;
            store1.Name = store.Name;
            db.Stores.Update(store1);

            List<CompanyStore> companyStores = db.CompaniesStores.Where(c => c.StoreID == store.StoreID).Include(c => c.company).ToList();
            foreach (CompanyStore companyStore in companyStores)
            {
                if (!companies.Contains(companyStore.company))
                {
                    db.CompaniesStores.Remove(db.CompaniesStores.Where(c => c.CompanyStoreID == companyStore.CompanyStoreID).First());
                }
            }

            foreach (Company company in companies)
            {
                if (db.CompaniesStores.Where(c => c.CompanyID == company.CompanyID && c.StoreID == store.StoreID).ToList().Count == 0)
                {
                    db.CompaniesStores.Add(new CompanyStore
                    {
                        store = store,
                        company = company
                    });
                }
            }

            db.SaveChanges();
            MessageBox.Show("Datos guardados correctamente");

            Information["fieldEmpty"] = 0;
            CT_Menu();
        }

        public void MD_StoresChange(int num)
        {
            if (num == 0)
                companies = new List<Company>();

            else
                companies = db.Companies.ToList();

            MC_Page = new View.MC_STR_Item_Load_Store_Companies(Information["external"]);
            MainContent.Content = MC_Page;
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
                    NV_Page = new View.NV_STR_Item_Load();
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_STR_Item_Load(Information["minimalInformation"], Information["external"]);
                    else
                        TS_Page = new View.TS_STR_Item_Load_Edit(Information["minimalInformation"], Information["external"]);
                    MC_Page = new View.MC_STR_Item_Load_Store(Information["external"]);
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new View.NV_STR_Item_Load();
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_STR_Item_Load(Information["minimalInformation"], Information["external"]);
                    else
                        TS_Page = new View.TS_STR_Item_Load_Edit(Information["minimalInformation"], Information["external"]);
                    MC_Page = new View.MC_STR_Item_Load_Store_Companies(Information["external"]);
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
                    a.MainFrame.Content = new StoreMenu.Controller.CT_StoreMenu();
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