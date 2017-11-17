﻿using System;
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
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace GestCloudv2.Files.Nodes.Companies.CompanyItem.CompanyItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_USR_Item_Load.xaml
    /// </summary>
    public partial class CT_CPN_Item_Load : Main.Controller.CT_Common
    {
        public Company company;
        public List<Store> stores;

        public CT_CPN_Item_Load(Company company, int editable)
        {
            stores = new List<Store>();
            Information.Add("editable", editable);
            Information.Add("old_editable", 0);
            Information.Add("minimalInformation", 0);
            Information["entityValid"] = 1;

            foreach(CompanyStore compsto in db.CompaniesStores.Where(c => c.CompanyID == company.CompanyID).Include(c=> c.store))
            {
                stores.Add(compsto.store);
            }

            Information["editable"] = editable;
            this.company = company;
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

        public void SetCompanyName(string name)
        {
            company.Name = name;
            TestMinimalInformation();
        }

        public void SetCompanyCode(int code)
        {
            company.Code = code;
            TestMinimalInformation();
        }

        public void UpdateStore(int num)
        {
            if(stores.Contains(db.Stores.Where(s => s.StoreID == num).First()))
            {
                stores.Remove(db.Stores.Where(c => c.StoreID == num).Include(c => c.CompaniesStores).First());
            }

            else
            {
                stores.Add(db.Stores.Where(c => c.StoreID == num).Include(c => c.CompaniesStores).First());
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
            company.Name = "";
            TestMinimalInformation();
        }

        public Boolean CompanyControlExist(string name)
        {
            List<Company> companies = db.Companies.ToList();
            foreach (var item in companies)
            {
                if ((item.Name.ToLower() == name.ToLower() && company.Name.ToLower() != name.ToLower()) || name.Length == 0)
                {
                    CleanName();
                    return true;
                }
            }
            company.Name = name;
            TestMinimalInformation();
            return false;
        }

        private void TestMinimalInformation()
        {
            if(company.Name.Length > 0 && company.Code > 0 && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            if(Information["editable"] == 0)
                TS_Page = new View.TS_CPN_Item_Load(Information["minimalInformation"]);
            else
                TS_Page = new View.TS_CPN_Item_Load_Edit(Information["minimalInformation"]);
            LeftSide.Content = TS_Page;
        }

        public void SaveNewCompany()
        {
            Company company1 = db.Companies.Where(c => c.CompanyID == company.CompanyID).First();
            company1.Code = company.Code;
            company1.Name = company.Name;
            db.Companies.Update(company1);

            List<CompanyStore> companyStores = db.CompaniesStores.Where(c => c.CompanyID == company.CompanyID).Include(c => c.store).ToList();
            foreach (CompanyStore companyStore in companyStores)
            {
                if (!stores.Contains(companyStore.store))
                {
                    db.CompaniesStores.Remove(db.CompaniesStores.Where(c => c.CompanyStoreID == companyStore.CompanyStoreID).First());
                }
            }

            foreach (Store store in stores)
            {
                if (db.CompaniesStores.Where(c=> c.StoreID == store.StoreID && c.CompanyID == company.CompanyID).ToList().Count == 0)
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
                stores = new List<Store>();

            else
                stores = db.Stores.ToList();

            MC_Page = new View.MC_CPN_Item_Load_Company_Stores();
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
                    NV_Page = new View.NV_CPN_Item_Load();
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_CPN_Item_Load(Information["minimalInformation"]);
                    else
                        TS_Page = new View.TS_CPN_Item_Load_Edit(Information["minimalInformation"]);
                    MC_Page = new View.MC_CPN_Item_Load_Company();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new View.NV_CPN_Item_Load();
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_CPN_Item_Load(Information["minimalInformation"]);
                    else
                        TS_Page = new View.TS_CPN_Item_Load_Edit(Information["minimalInformation"]);
                    MC_Page = new View.MC_CPN_Item_Load_Company_Stores();
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
                    a.MainFrame.Content = new Files.Nodes.Companies.CompanyMenu.Controller.CT_CompanyMenu();
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