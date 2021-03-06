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

namespace GestCloudv2.Files.Nodes.Companies.CompanyItem.CompanyItem_New.Controller
{
    /// <summary>
    /// Interaction logic for CT_USR_Item_New.xaml
    /// </summary>
    public partial class CT_CPN_Item_New : Main.Controller.CT_Common
    {
        public Configuration ConfigSelected;
        public Company company;
        public FiscalYear fiscalYear;
        public List<Store> stores;
        public List<PaymentMethod> paymentMethods;
        public List<Tax> taxes;
        public List<Tax> equiSurs;
        public List<Tax> specTaxes;
        public List<TaxType> taxTypes;
        public int startDayDate;
        public int startMonthDate;
        Dictionary<int, int> Configurations;

        public CT_CPN_Item_New()
        {
            stores = new List<Store>();
            taxTypes = new List<TaxType>();
            taxes = new List<Tax>();
            equiSurs = new List<Tax>();
            specTaxes = new List<Tax>();
            paymentMethods= new List<PaymentMethod>();
            company = new Company();
            fiscalYear = new FiscalYear();
            Configurations = new Dictionary<int, int>();
            startDayDate = 1;
            startMonthDate = 1;

            taxes.AddRange(new List<Tax>{new Tax
            {
                Type = 1,
                Percentage = 0.00M
            }, new Tax
            {
                Type = 2,
                Percentage = 4.00M
            } ,new Tax
            {
                Type = 3,
                Percentage = 10.00M
            }, new Tax
            {
                Type = 4,
                Percentage = 21.00M
            }});

            equiSurs.AddRange(new List<Tax>{new Tax
            {
                Type = 1,
                Percentage = 0.00M
            }, new Tax
            {
                Type = 2,
                Percentage = 0.50M
            } ,new Tax
            {
                Type = 3,
                Percentage = 1.40M
            }, new Tax
            {
                Type = 4,
                Percentage = 5.20M
            }});

            taxTypes.Add(new TaxType
            {
                Name = "IVA",
            });

            taxTypes.Add(new TaxType
            {
                Name = "RE",
            });

            taxTypes.Add(new TaxType
            {
                Name = "ST",
            });

            List<Configuration> AllConfigurations = db.Configurations.ToList();

            foreach (Configuration item in AllConfigurations)
            {
                    Configurations.Add(item.ConfigurationID, -1);
            }

            Information.Add("minimalInformation", 0);
            Information["entityValid"] = 1;
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

        public List<PaymentMethod> GetPaymentMethods()
        {
            return db.PaymentMethods.ToList();
        }

        public Configuration GetConfiguration()
        {
            return db.Configurations.Where(c => c.ConfigurationID == ConfigSelected.ConfigurationID).First();
        }

        public int GetConfigurationValue()
        {
            if (Configurations[ConfigSelected.ConfigurationID] != -1)
                return Configurations[ConfigSelected.ConfigurationID];

            else
                return ConfigSelected.DefaultValue;
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

        public void SetCompanyPeriodOption(int num)
        {
            company.PeriodOption = num;
        }

        public void SetConfiguration(int num)
        {
            ConfigSelected = db.Configurations.Where(c => c.ConfigurationID == num).First();
        }

        public void SetConfigValue(int num)
        {
            Configurations[ConfigSelected.ConfigurationID] = num;
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

        public void UpdatePaymentMethod(int num)
        {
            if (paymentMethods.Contains(db.PaymentMethods.Where(s => s.PaymentMethodID == num).First()))
            {
                paymentMethods.Remove(db.PaymentMethods.Where(c => c.PaymentMethodID== num).First());
            }

            else
            {
                paymentMethods.Add(db.PaymentMethods.Where(c => c.PaymentMethodID== num).First());
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

        public void CleanCIF()
        {
            company.CIF = "";
            TestMinimalInformation();
        }

        public void CleanPhone1()
        {
            company.Phone1 = 0;
            TestMinimalInformation();
        }

        public void CleanPhone2()
        {
            company.Phone2 = 0;
            TestMinimalInformation();
        }

        public void CleanFax()
        {
            company.Fax = 0;
            TestMinimalInformation();
        }

        public void SetPhone1(string num)
        {
            if (num.Length > 0)
            {
                company.Phone1 = Convert.ToInt32(num);
            }
            else
            {
                company.Phone1 = 0;
            }
        }

        public void SetPhone2(string num)
        {
            if (num.Length > 0)
            {
                company.Phone2 = Convert.ToInt32(num);
            }
            else
            {
                company.Phone2 = 0;
            }
        }

        public void SetFax(string num)
        {
            if (num.Length > 0)
            {
                company.Fax = Convert.ToInt32(num);
            }
            else
            {
                company.Fax = 0;
            }
        }

        public void SetCompanyCif(string num)
        {
            if (num.Length > 0)
            {
                company.CIF = num;
            }
            else
            {
                company.CIF = "";
            }
        }

        public void SetCompanyAddress(string num)
        {
            if (num.Length > 0)
            {
                company.Address = num;
            }
            else
            {
                company.Address = "";
            }
        }

        public Boolean CompanyControlExist(string name)
        {
            List<Company> companies = db.Companies.ToList();
            foreach (var item in companies)
            {
                if (item.Name.ToLower() == name.ToLower() || name.Length == 0)
                {
                    CleanName();
                    return true;
                }
            }
            company.Name = name;
            TestMinimalInformation();
            return false;
        }

        public Boolean CompanyControlExistCIF(string cif)
        {
            List<Company> companies = db.Companies.ToList();
            foreach (var item in companies)
            {
                if (item.CIF.ToLower() == cif.ToLower() || cif.Length == 0)
                {
                    CleanCIF();
                    return true;
                }
            }
            company.CIF = cif;
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

            TS_Page = new View.TS_CPN_Item_New(Information["minimalInformation"]);
            LeftSide.Content = TS_Page;
        }

        public void SaveNewCompany()
        {
            int year = Convert.ToInt16(DateTime.Today.ToString("yyyy"));
            DateTime startDate = new DateTime(year, startMonthDate, startDayDate);
            DateTime endDate = new DateTime();

            switch(company.PeriodOption)
            {
                case 1:
                    endDate = startDate.AddMonths(12).AddDays(-1);
                    break;

                case 2:
                    endDate = startDate.AddMonths(6).AddDays(-1);
                    break;

                case 3:
                    endDate = startDate.AddMonths(3).AddDays(-1);
                    break;

                case 4:
                    endDate = startDate.AddMonths(1).AddDays(-1);
                    break;
            }

            //company.FiscalYearID = null;

            db.Companies.Add(company);

            foreach (Store store in stores)
            {
                db.CompaniesStores.Add(new CompanyStore
                {
                    store = store,
                    company = company
                });
            }

            foreach (PaymentMethod paymentMethod in paymentMethods)
            {
                db.CompaniesPaymentMethods.Add(new CompanyPaymentMethod
                {
                    paymentMethod = paymentMethod,
                    company = company
                });
            }

            if (startDayDate == 1 && startMonthDate == 1)
            {
                db.FiscalYears.Add(new FiscalYear
                {
                    Name = $"{year}",
                    StartDate = startDate,
                    EndDate = endDate,
                    company = company
                });
            }
            else
            {
                db.FiscalYears.Add(new FiscalYear
                {
                    Name = $"{year}-{year+1}",
                    StartDate = startDate,
                    EndDate = endDate,
                    company = company
                });
            }

            foreach (TaxType taxtype in taxTypes)
            {
                taxtype.company = company;
                taxtype.StartDate = startDate;
                taxtype.EndDate = endDate;
            }

            db.TaxTypes.AddRange(taxTypes);

            foreach (Tax tax in taxes)
            {
                tax.taxType = taxTypes.Where(t => t.Name.Contains("IVA")).First();
            }

            db.Taxes.AddRange(taxes);

            foreach (Tax tax in equiSurs)
            {
                tax.taxType = taxTypes.Where(t => t.Name.Contains("RE")).First();
            }

            db.Taxes.AddRange(equiSurs);

            foreach (Tax tax in specTaxes)
            {
                tax.taxType = taxTypes.Where(t => t.Name.Contains("ST")).First();
            }

            db.Taxes.AddRange(specTaxes);

            List<Configuration> AllConfigurations = db.Configurations.ToList();

            foreach (Configuration item in AllConfigurations)
            {
                db.ConfigurationsCompanies.Add(new ConfigurationCompany
                {
                    CompanyID = company.CompanyID,
                    ConfigurationID = item.ConfigurationID,
                    Value = Configurations[item.ConfigurationID],
                });
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

            MC_Page = new View.MC_CPN_Item_New_Company_Stores();
            MainContent.Content = MC_Page;
        }

        public void MD_PaymentMethodsChange(int num)
        {
            if (num == 0)
                paymentMethods = new List<PaymentMethod>();

            else
                paymentMethods = db.PaymentMethods.ToList();

            MC_Page = new View.MC_CPN_Item_New_Company_PaymentMethods();
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
                    NV_Page = new View.NV_CPN_Item_New();
                    TS_Page = new View.TS_CPN_Item_New(Information["minimalInformation"]);
                    MC_Page = new View.MC_CPN_Item_New_Company();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new View.NV_CPN_Item_New();
                    TS_Page = new View.TS_CPN_Item_New(Information["minimalInformation"]);
                    MC_Page = new View.MC_CPN_Item_New_Company_Stores();
                    ChangeComponents();
                    break;

                case 3:
                    NV_Page = new View.NV_CPN_Item_New();
                    TS_Page = new View.TS_CPN_Item_New(Information["minimalInformation"]);
                    MC_Page = new View.MC_CPN_Item_New_Company_Taxes();
                    ChangeComponents();
                    break;

                case 4:
                    NV_Page = new View.NV_CPN_Item_New();
                    TS_Page = new View.TS_CPN_Item_New(Information["minimalInformation"]);
                    MC_Page = new View.MC_CPN_Item_New_Company_PaymentMethods();
                    ChangeComponents();
                    break;

                case 5:
                    NV_Page = new View.NV_CPN_Item_New();
                    TS_Page = new View.TS_CPN_Item_New(Information["minimalInformation"]);
                    MC_Page = new View.MC_CPN_Item_New_Company_Configuration();
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