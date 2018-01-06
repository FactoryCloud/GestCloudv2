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
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using FrameworkView.V1;

namespace GestCloudv2.Files.Nodes.Providers.ProviderItem.ProviderItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_CLI_Item_Load.xaml
    /// </summary>
    public partial class CT_PRO_Item_Load : Main.Controller.CT_Common
    {
        public Provider provider;
        public SubmenuItems submenuItems;
        public TaxType taxTypeSelected;
        public Dictionary<int, int> InformationTaxes;
        public Dictionary<int, int> InformationEquivalenceSurcharges;
        public Dictionary<int, int> InformationSpecialTaxes;

        public CT_PRO_Item_Load(Provider provider, int editable)
        {
            submenuItems = new SubmenuItems();
            List<TaxType> taxTypes = GetTaxTypes().OrderByDescending(t => t.StartDate).ToList();

            InformationTaxes = new Dictionary<int, int>();
            InformationEquivalenceSurcharges = new Dictionary<int, int>();
            InformationSpecialTaxes = new Dictionary<int, int>();
            taxTypeSelected = taxTypes.First();

            Information.Add("editable", editable);
            Information.Add("old_editable", 0);
            Information.Add("minimalInformation", 0);
            Information.Add("external", 0);
            this.provider = provider;
            Information["entityValid"] = 1;

            foreach (TaxType tx in taxTypes)
            {
                List<ProviderTax> providerTaxes = db.ProvidersTaxes.Where(c => c.ProviderID == provider.ProviderID && c.tax.TaxTypeID == tx.TaxTypeID).ToList();

                if (providerTaxes.Count > 0)
                {
                    InformationTaxes.Add(tx.TaxTypeID, 1);
                }

                else
                {
                    InformationTaxes.Add(tx.TaxTypeID, 0);
                }

                TaxType taxType = db.TaxTypes.Where(tt => tt.CompanyID == tx.CompanyID && tt.StartDate == tx.StartDate && tt.EndDate == tx.EndDate && tt.Name.Contains("RE")).First();
                List<ProviderTax> providerEquiSurs = db.ProvidersTaxes.Where(c => c.ProviderID == provider.ProviderID && c.tax.TaxTypeID == taxType.TaxTypeID).ToList();
                if (providerEquiSurs.Count > 0)
                {
                    InformationEquivalenceSurcharges.Add(tx.TaxTypeID, 1);
                }
                else
                {
                    InformationEquivalenceSurcharges.Add(tx.TaxTypeID, 0);
                }

                TaxType specialTaxType = db.TaxTypes.Where(tt => tt.CompanyID == tx.CompanyID && tt.StartDate == tx.StartDate && tt.EndDate == tx.EndDate && tt.Name.Contains("ST")).First();
                List<ProviderTax> providerSpecialTaxes = db.ProvidersTaxes.Where(c => c.ProviderID == provider.ProviderID && c.tax.TaxTypeID == specialTaxType.TaxTypeID).ToList();

                if (providerSpecialTaxes.Count > 0)
                {
                    InformationSpecialTaxes.Add(tx.TaxTypeID, Convert.ToInt32(providerSpecialTaxes.First().TaxID));
                }

                else
                {
                    InformationSpecialTaxes.Add(tx.TaxTypeID, 0);
                }
            }
        }

        public CT_PRO_Item_Load(Provider provider, int editable, int external) : base(external)
        {
            submenuItems = new SubmenuItems();
            Information.Add("editable", editable);
            Information.Add("old_editable", 0);
            Information.Add("minimalInformation", 0);
            Information.Add("external", 1);
            this.provider = provider;
            Information["entityValid"] = 1;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"{entity.EntityID}");
            entity = db.Entities.Where(u => u.EntityID == provider.EntityID).First();

            UpdateComponents();
        }

        public List<Provider> GetProviders()
        {
            return db.Providers.OrderBy(u => u.Cod).ToList();
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


        public void SetProviderCod(int num)
        {
            provider.Cod = num;
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

        public void CleanCod()
        {
            provider.Cod = 0;
            TestMinimalInformation();
        }

        public Boolean ProviderControlExist(int cod)
        {
            List<Provider> providers = db.Providers.ToList();
            foreach (var item in providers)
            {
                if ((item.Cod == cod && provider.Cod != cod)|| cod == 0)
                {
                    CleanCod();
                    return true;
                }
            }
            provider.Cod = cod;
            TestMinimalInformation();
            return false;
        }

        private void TestMinimalInformation()
        {
            if (provider.Cod > 0 && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }
            if (Information["editable"] != 0)
            {
                TS_Page = new View.TS_PRO_Item_Load_Editable(Information["minimalInformation"], Information["external"]);
                LeftSide.Content = TS_Page;
            }
        }

        public void SaveNewProvider()
        {
            if (Information["entityLoaded"] == 2)
            {
                db.Entities.Update(entity);
            }
            
            Provider provider1 = db.Providers.Where(u => u.ProviderID == provider.ProviderID).First();
            provider1.Cod= provider.Cod;
            provider1.EntityID = entity.EntityID;

            db.Providers.Update(provider1);
            db.SaveChanges();
            MessageBox.Show("Datos guardados correctamente");

            Information["fieldEmpty"] = 0;
            CT_Menu();
        }

        override public void MD_EntityEdit()
        {
            Information["entityLoaded"] = 2;
            MD_Change(3,0);
        }

        override public void MD_EntityLoad()
        {
            View.FW_PRO_Item_Load_Entity floatWindow = new View.FW_PRO_Item_Load_Entity(4);
            floatWindow.Show();
        }

        public override void MD_EntityLoaded()
        {
            MD_Change(4,0);
        }

        public void CT_Menu()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        override public void UpdateComponents()
        {
            if (Information["entityLoaded"] == 1 && Information["mode"] == 2)
                Information["mode"] = 4;

            if(Information["entityLoaded"] == 2 && Information["mode"] == 2)
                Information["mode"] = 3;

            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new View.NV_PRO_Item_Load(Information["external"]);
                    if(Information["editable"] == 0)
                        TS_Page = new View.TS_PRO_Item_Load(Information["minimalInformation"], Information["external"]);
                    else
                        TS_Page = new View.TS_PRO_Item_Load_Editable(Information["minimalInformation"], Information["external"]);
                    MC_Page = new View.MC_PRO_Item_Load_Provider(Information["external"]);
                    ChangeComponents();
                    break;

                case 2:
                    if (Information["editable"] == 0)
                    {
                        Information["mode"] = 4;
                        UpdateComponents();
                        break;
                    }

                    else
                    {
                        NV_Page = new View.NV_PRO_Item_Load(Information["external"]);
                        TS_Page = new View.TS_PRO_Item_Load_Editable(Information["minimalInformation"], Information["external"]);
                        MC_Page = new View.MC_PRO_Item_Load_Entity_Select();
                    }
                    ChangeComponents();
                    break;

                case 3:
                    NV_Page = new View.NV_PRO_Item_Load(Information["external"]);
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_PRO_Item_Load(Information["minimalInformation"], Information["external"]);
                    else
                        TS_Page = new View.TS_PRO_Item_Load_Editable(Information["minimalInformation"], Information["external"]);
                    MC_Page = new View.MC_PRO_Item_Load_Entity_Edit();
                    ChangeComponents();
                    break;

                case 4:
                    NV_Page = new View.NV_PRO_Item_Load(Information["external"]);
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_PRO_Item_Load(Information["minimalInformation"], Information["external"]);
                    else
                        TS_Page = new View.TS_PRO_Item_Load_Editable(Information["minimalInformation"], Information["external"]);
                    MC_Page = new View.MC_PRO_Item_Load_Entity_Loaded(Information["external"]);
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
                    a.MainFrame.Content = new Files.Nodes.Providers.ProviderMenu.Controller.CT_ProviderMenu();
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