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

namespace GestCloudv2.Files.Nodes.ProductTypes.ProductTypeItem.ProductTypeItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_STR_Item_Load.xaml
    /// </summary>
    public partial class CT_PTY_Item_Load : Main.Controller.CT_Common
    {
        public ProductType productType;
        public SubmenuItems submenuItems;
        public TaxType purchaseTaxTypeSelected;
        public TaxType saleTaxTypeSelected;
        public List<ProductTypeTax> purchaseProductTypeTaxes;
        public List<ProductTypeTax> saleProductTypeTaxes;
        public List<ProductTypeTax> purchaseProductTypeSpecialTaxes;
        public List<ProductTypeTax> saleProductTypeSpecialTaxes;

        public CT_PTY_Item_Load(ProductType productType, int editable)
        {
            submenuItems = new SubmenuItems();

            purchaseTaxTypeSelected = GetTaxTypes().OrderByDescending(t => t.StartDate).First();
            saleTaxTypeSelected = GetTaxTypes().OrderByDescending(t => t.StartDate).First();

            purchaseProductTypeTaxes = db.ProductTypesTaxes.Where(pt => pt.Input == 1 && pt.ProductTypeID == productType.ProductTypeID && pt.tax.taxType.Name.Contains("IVA")).Include(c => c.tax).Include(d => d.tax.taxType).ToList();
            saleProductTypeTaxes = db.ProductTypesTaxes.Where(pt => pt.Input == 0 && pt.ProductTypeID == productType.ProductTypeID && pt.tax.taxType.Name.Contains("IVA")).Include(c => c.tax).Include(d => d.tax.taxType).ToList();
            purchaseProductTypeSpecialTaxes = db.ProductTypesTaxes.Where(pt => pt.Input == 1 && pt.ProductTypeID == productType.ProductTypeID && pt.tax.taxType.Name.Contains("ST")).Include(c => c.tax).Include(d => d.tax.taxType).ToList();
            saleProductTypeSpecialTaxes = db.ProductTypesTaxes.Where(pt => pt.Input == 0 && pt.ProductTypeID == productType.ProductTypeID && pt.tax.taxType.Name.Contains("ST")).Include(c => c.tax).Include(d => d.tax.taxType).ToList();

            Information.Add("editable", editable);
            Information.Add("old_editable", 0);
            Information.Add("minimalInformation", 0);
            Information.Add("external", 0);
            Information["entityValid"] = 1;

            Information["editable"] = editable;
            this.productType = productType;
        }

        public CT_PTY_Item_Load(ProductType productType, int editable, int external):base(external)
        {
            submenuItems = new SubmenuItems();
            Information.Add("editable", editable);
            Information.Add("old_editable", 0);
            Information.Add("minimalInformation", 0);
            Information.Add("external", 1);
            Information["entityValid"] = 1;

            Information["editable"] = editable;
            this.productType = productType;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public List<ProductType> GetProductTypes()
        {
            return db.ProductTypes.ToList();
        }

        public List<TaxType> GetTaxTypes()
        {
            return db.TaxTypes.Where(t => t.StartDate >= ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.fiscalYear.StartDate && t.EndDate <= ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.fiscalYear.EndDate
            && t.company.CompanyID == ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID && t.Name.Contains("IVA")).ToList();
        }

        public List<Tax> GetTaxes()
        {
            return db.Taxes.Where(t => t.TaxTypeID == purchaseTaxTypeSelected.TaxTypeID).ToList();
        }

        public List<Tax> GetEquiSurs()
        {
            TaxType taxType = db.TaxTypes.Where(t => t.StartDate == purchaseTaxTypeSelected.StartDate && t.EndDate == purchaseTaxTypeSelected.EndDate && t.CompanyID == purchaseTaxTypeSelected.CompanyID && t.Name.Contains("RE")).First();
            return db.Taxes.Where(t => t.TaxTypeID == taxType.TaxTypeID).ToList();
        }

        public List<Tax> GetSpecTaxes()
        {
            TaxType taxType = db.TaxTypes.Where(t => t.StartDate == purchaseTaxTypeSelected.StartDate && t.EndDate == purchaseTaxTypeSelected.EndDate && t.CompanyID == purchaseTaxTypeSelected.CompanyID && t.Name.Contains("ST")).First();
            return db.Taxes.Where(t => t.TaxTypeID == taxType.TaxTypeID).ToList();
        }

        public void SetProductTypeName(string name)
        {
            productType.Name = name;
            TestMinimalInformation();
        }

        public void SetPurchaseTaxTypeSelected(int num)
        {
            purchaseTaxTypeSelected = db.TaxTypes.Where(t => t.TaxTypeID == num).First();
        }

        public void SetSaleTaxTypeSelected(int num)
        {
            saleTaxTypeSelected = db.TaxTypes.Where(t => t.TaxTypeID == num).First();
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
            productType.Name = "";
            TestMinimalInformation();
        }

        public void CleanCode()
        {
            productType.Code = 0;
            TestMinimalInformation();
        }

        private void TestMinimalInformation()
        {
            if(!string.IsNullOrWhiteSpace(productType.Name) && productType.Code > 0 && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            if (Information["editable"] == 0)
                TS_Page = new View.TS_PTY_Item_Load(Information["minimalInformation"], Information["external"]);
            else
                TS_Page = new View.TS_PTY_Item_Load_Edit(Information["minimalInformation"], Information["external"]);
            LeftSide.Content = TS_Page;
        }

        public Boolean ProductTypeControlExist(string name)
        {
            List<ProductType> productTypes = db.ProductTypes.ToList();
            foreach (var item in productTypes)
            {
                if ((item.Name.ToLower() == name.ToLower() && productType.Name.ToLower() != name.ToLower()) || name.Length == 0)
                {
                    CleanName();
                    return true;
                }
            }
            productType.Name = name;
            TestMinimalInformation();
            return false;
        }

        public void SaveNewStore()
        {
            /*Store store1 = db.Stores.Where(c => c.StoreID == store.StoreID).First();
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
            CT_Menu();*/
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
                    NV_Page = new View.NV_PTY_Item_Load(Information["external"]);
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_PTY_Item_Load(Information["minimalInformation"], Information["external"]);
                    else
                        TS_Page = new View.TS_PTY_Item_Load_Edit(Information["minimalInformation"], Information["external"]);
                    MC_Page = new View.MC_PTY_Item_Load_ProductType(Information["external"]);
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new View.NV_PTY_Item_Load(Information["external"]);
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_PTY_Item_Load(Information["minimalInformation"], Information["external"]);
                    else
                        TS_Page = new View.TS_PTY_Item_Load_Edit(Information["minimalInformation"], Information["external"]);
                    MC_Page = new View.MC_PTY_Item_Load_ProductType_Taxes(Information["external"]);
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
                    a.MainFrame.Content = new ProductTypeMenu.Controller.CT_ProductTypeMenu();
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