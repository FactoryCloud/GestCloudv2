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

namespace GestCloudv2.Files.Nodes.Providers.ProviderItem.ProviderItem_New.Controller
{
    /// <summary>
    /// Interaction logic for CT_PRO_Item_New.xaml
    /// </summary>
    public partial class CT_PRO_Item_New : Main.Controller.CT_Common
    {
        public Provider provider;
        public int lastProviderCod;
        public SubmenuItems submenuItems;
        public TaxType taxTypeSelected;
        public Dictionary<int, int> InformationTaxes;
        public Dictionary<int, int> InformationEquivalenceSurcharges;
        public Dictionary<int, int> InformationSpecialTaxes;

        public CT_PRO_Item_New()
        {
            List<TaxType> taxTypes = GetTaxTypes().OrderByDescending(t => t.StartDate).ToList();
            InformationTaxes = new Dictionary<int, int>();
            InformationEquivalenceSurcharges = new Dictionary<int, int>();
            InformationSpecialTaxes = new Dictionary<int, int>();
            taxTypeSelected = taxTypes.First();

            submenuItems = new SubmenuItems();
            entity = new Entity();
            provider = new Provider();
            Information.Add("minimalInformation", 0);

            foreach (TaxType tx in taxTypes)
            {
                InformationTaxes.Add(tx.TaxTypeID, 1);
                InformationEquivalenceSurcharges.Add(tx.TaxTypeID, 0);
                InformationSpecialTaxes.Add(tx.TaxTypeID, 0);
            }
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SaveNewProvider()
        {
            if (Information["entityLoaded"] == 2)
            {
                if (db.Providers.ToList().Count > 0)
                {
                    entity.Cod = db.Entities.OrderBy(u => u.Cod).Last().Cod + 1;
                }
                else
                {
                    entity.Cod = 1;
                }

                db.Entities.Add(entity);
            }

            provider.entity = entity;
            db.Providers.Add(provider);
            db.SaveChanges();

            List<TaxType> taxTypes = GetTaxTypes().OrderByDescending(t => t.StartDate).ToList();

            foreach (TaxType tx in taxTypes)
            {
                if (InformationTaxes[tx.TaxTypeID] == 1)
                {
                    List<Tax> taxes = db.Taxes.Where(t => t.TaxTypeID == tx.TaxTypeID).ToList();
                    foreach (Tax t in taxes)
                    {
                        db.ProvidersTaxes.Add(new ProviderTax
                        {
                            ProviderID = provider.ProviderID,
                            TaxID = t.TaxID
                        });
                    }
                }

                if (InformationEquivalenceSurcharges[tx.TaxTypeID] == 1)
                {
                    TaxType taxType = db.TaxTypes.Where(t => t.StartDate == taxTypeSelected.StartDate && t.EndDate == taxTypeSelected.EndDate && t.CompanyID == taxTypeSelected.CompanyID && t.Name.Contains("RE")).First();
                    List<Tax> taxes = db.Taxes.Where(t => t.TaxTypeID == taxType.TaxTypeID).ToList();
                    foreach (Tax t in taxes)
                    {
                        db.ProvidersTaxes.Add(new ProviderTax
                        {
                            ProviderID = provider.ProviderID,
                            TaxID = t.TaxID
                        });
                    }
                }

                if (InformationSpecialTaxes[tx.TaxTypeID] >= 1)
                {
                    db.ProvidersTaxes.Add(new ProviderTax
                    {
                        ProviderID = provider.ProviderID,
                        TaxID = InformationSpecialTaxes[tx.TaxTypeID]
                    });
                }
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

        public List<TaxType> GetTaxTypes()
        {
            return db.TaxTypes.Where(t => t.StartDate >= ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedFiscalYear.StartDate && t.EndDate <= ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedFiscalYear.EndDate
            && t.company.CompanyID == ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID && t.Name.Contains("IVA")).ToList();
        }

        public List<Tax> GetSpecTaxes()
        {
            TaxType taxType = db.TaxTypes.Where(t => t.StartDate == taxTypeSelected.StartDate && t.EndDate == taxTypeSelected.EndDate && t.CompanyID == taxTypeSelected.CompanyID && t.Name.Contains("ST")).First();
            return db.Taxes.Where(t => t.TaxTypeID == taxType.TaxTypeID).ToList();
        }

        public void SetTaxTypeSelected(int num)
        {
            taxTypeSelected = db.TaxTypes.Where(t => t.TaxTypeID == num).First();
        }

        override public void MD_EntityLoad()
        {
            View.FW_PRO_Item_Load_Entity floatWindow = new View.FW_PRO_Item_Load_Entity(4);
            floatWindow.Show();
        }

        override public void MD_EntityNew()
        {
            Information["entityLoaded"] = 2;
            MD_Change(3,0);
        }

        public override void MD_EntityLoaded()
        {
            MD_Change(4,0);
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

        public Boolean ProviderControlExist(int providerCod)
        {
            List<Provider> providers = db.Providers.ToList();
            foreach (var item in providers)
            {
                if (item.Code == providerCod)
                {
                    provider.Code = 0;
                    return true;
                }
            }
            provider.Code = providerCod;
            TestMinimalInformation();
            return false;
        }

        public int LastProviderCod()
        {
            if (db.Providers.ToList().Count > 0)
            {
                lastProviderCod = db.Providers.OrderBy(u => u.Code).Last().Code + 1;
                provider.Code = lastProviderCod;
                return lastProviderCod;
            }
            else
            {
                provider.Code = 1;
                return lastProviderCod = 1;

            }
        }

        public void TestMinimalInformation()
        {
            if (provider.Code > 0 && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            TS_Page = new View.TS_PRO_Item_New(Information["minimalInformation"]);
            LeftSide.Content = TS_Page;
        }

        override public void UpdateComponents()
        {
            if (Information["entityLoaded"] == 1 && Information["mode"] == 2)
                Information["mode"] = 4;

            if (Information["entityLoaded"] == 2 && Information["mode"] == 2)
                Information["mode"] = 3;

            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new ProviderItem_New.View.NV_PRO_Item_New();
                    TS_Page = new ProviderItem_New.View.TS_PRO_Item_New(Information["minimalInformation"]);
                    MC_Page = new ProviderItem_New.View.MC_PRO_Item_New_Provider();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new ProviderItem_New.View.NV_PRO_Item_New();
                    TS_Page = new ProviderItem_New.View.TS_PRO_Item_New(Information["minimalInformation"]);
                    MC_Page = new ProviderItem_New.View.MC_PRO_Item_Load_Entity_Select();
                    ChangeComponents();
                    break;

                case 3:
                    NV_Page = new ProviderItem_New.View.NV_PRO_Item_New();
                    TS_Page = new ProviderItem_New.View.TS_PRO_Item_New(Information["minimalInformation"]);
                    MC_Page = new ProviderItem_New.View.MC_PRO_Item_New_Entity_New();
                    ChangeComponents();
                    break;

                case 4:
                    NV_Page = new ProviderItem_New.View.NV_PRO_Item_New();
                    TS_Page = new ProviderItem_New.View.TS_PRO_Item_New(Information["minimalInformation"]);
                    MC_Page = new ProviderItem_New.View.MC_PRO_Item_Load_Entity_Loaded();
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
                    a.MainFrame.Content = new ProviderMenu.Controller.CT_ProviderMenu();
                    break;
            }
        }
    }
}