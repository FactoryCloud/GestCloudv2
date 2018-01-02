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

namespace GestCloudv2.Files.Nodes.Products.ProductItem.ProductItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_STR_Item_Load.xaml
    /// </summary>
    public partial class CT_PDT_Item_Load : Main.Controller.CT_Common
    {
        public Product product;
        public SubmenuItems submenuItems;
        public TaxType purchaseTaxTypeSelected;
        public TaxType saleTaxTypeSelected;
        public List<ProductTax> purchaseProductTaxes;
        public List<ProductTax> saleProductTaxes;
        public List<ProductTax> purchaseProductSpecialTaxes;
        public List<ProductTax> saleProductSpecialTaxes;

        public CT_PDT_Item_Load(Product product, int editable)
        {
            submenuItems = new SubmenuItems();

            purchaseTaxTypeSelected = GetTaxTypes().OrderByDescending(t => t.StartDate).First();
            saleTaxTypeSelected = GetTaxTypes().OrderByDescending(t => t.StartDate).First();

            purchaseProductTaxes = db.ProductsTaxes.Where(pt => pt.tax.taxType.CompanyID == ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID && pt.Input == 1 && pt.ProductID == product.ProductID && pt.tax.taxType.Name.Contains("IVA")).Include(c => c.tax).Include(d => d.tax.taxType).ToList();
            saleProductTaxes = db.ProductsTaxes.Where(pt => pt.tax.taxType.CompanyID == ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID && pt.Input == 0 && pt.ProductID == product.ProductID && pt.tax.taxType.Name.Contains("IVA")).Include(c => c.tax).Include(d => d.tax.taxType).ToList();
            purchaseProductSpecialTaxes = db.ProductsTaxes.Where(pt => pt.tax.taxType.CompanyID == ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID && pt.Input == 1 && pt.ProductID == product.ProductID && pt.tax.taxType.Name.Contains("ST")).Include(c => c.tax).Include(d => d.tax.taxType).ToList();
            saleProductSpecialTaxes = db.ProductsTaxes.Where(pt => pt.tax.taxType.CompanyID == ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID && pt.Input == 0 && pt.ProductID == product.ProductID && pt.tax.taxType.Name.Contains("ST")).Include(c => c.tax).Include(d => d.tax.taxType).ToList();

            Information.Add("editable", editable);
            Information.Add("old_editable", 0);
            Information.Add("minimalInformation", 0);
            Information.Add("external", 0);
            Information["entityValid"] = 1;

            Information["editable"] = editable;
            this.product = product;
        }

        public CT_PDT_Item_Load(Product product, int editable, int external):base(external)
        {
            submenuItems = new SubmenuItems();
            Information.Add("editable", editable);
            Information.Add("old_editable", 0);
            Information.Add("minimalInformation", 0);
            Information.Add("external", 1);
            Information["entityValid"] = 1;

            Information["editable"] = editable;
            this.product = product;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public List<Product> GetProducts()
        {
            return db.Products.ToList();
        }

        public List<TaxType> GetTaxTypes()
        {
            return db.TaxTypes.Where(t => t.StartDate >= ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedFiscalYear.StartDate && t.EndDate <= ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedFiscalYear.EndDate
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

        public List<Tax> GetSaleTaxes()
        {
            return db.Taxes.Where(t => t.TaxTypeID == saleTaxTypeSelected.TaxTypeID).ToList();
        }

        public List<Tax> GetSaleEquiSurs()
        {
            TaxType taxType = db.TaxTypes.Where(t => t.StartDate == saleTaxTypeSelected.StartDate && t.EndDate == saleTaxTypeSelected.EndDate && t.CompanyID == saleTaxTypeSelected.CompanyID && t.Name.Contains("RE")).First();
            return db.Taxes.Where(t => t.TaxTypeID == taxType.TaxTypeID).ToList();
        }

        public List<Tax> GetSaleSpecTaxes()
        {
            TaxType taxType = db.TaxTypes.Where(t => t.StartDate == saleTaxTypeSelected.StartDate && t.EndDate == saleTaxTypeSelected.EndDate && t.CompanyID == saleTaxTypeSelected.CompanyID && t.Name.Contains("ST")).First();
            return db.Taxes.Where(t => t.TaxTypeID == taxType.TaxTypeID).ToList();
        }

        public void SetProductTypeName(string name)
        {
            product.Name = name;
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

        public void SetPurchaseTax(int num)
        {
            Tax tax = db.Taxes.Where(t => t.TaxID == num).First();
            if (purchaseProductTaxes.Where(p => p.TaxID == num).Count() == 0)
            {
                purchaseProductTaxes.Add(new ProductTax
                {
                    TaxID = num,
                    tax = tax,
                    ProductID = product.ProductID,
                    Input = 1
                });

                if (purchaseProductTaxes.Where(p => p.tax.TaxTypeID == tax.TaxTypeID).Count() > 0)
                    purchaseProductTaxes.Remove(purchaseProductTaxes.Where(p => p.tax.TaxTypeID == tax.TaxTypeID).First());
            }
        }

        public void SetPurchaseSpecialTax(int num)
        {
            Tax tax = db.Taxes.Where(t => t.TaxID == num).First();
            if (purchaseProductSpecialTaxes.Where(p => p.TaxID == num).Count() == 0)
            {
                purchaseProductSpecialTaxes.Add(new ProductTax
                {
                    TaxID = num,
                    tax = tax,
                    ProductID = product.ProductID,
                    Input = 1
                });

                if (purchaseProductSpecialTaxes.Where(p => p.tax.TaxTypeID == tax.TaxTypeID).Count() > 0)
                    purchaseProductSpecialTaxes.Remove(purchaseProductSpecialTaxes.Where(p => p.tax.TaxTypeID == tax.TaxTypeID).First());
            }
        }

        public void SetSaleTax(int num)
        {
            Tax tax = db.Taxes.Where(t => t.TaxID == num).First();
            if (saleProductTaxes.Where(p => p.TaxID == num).Count() == 0)
            {
                saleProductTaxes.Add(new ProductTax
                {
                    TaxID = num,
                    tax = tax,
                    ProductID = product.ProductID,
                    Input = 0
                });

                if (saleProductTaxes.Where(p => p.tax.TaxTypeID == tax.TaxTypeID).Count() > 0)
                    saleProductTaxes.Remove(saleProductTaxes.Where(p => p.tax.TaxTypeID == tax.TaxTypeID).First());
            }
        }

        public void SetSaleSpecialTax(int num)
        {
            Tax tax = db.Taxes.Where(t => t.TaxID == num).First();
            if (saleProductSpecialTaxes.Where(p => p.TaxID == num).Count() == 0)
            {
                saleProductSpecialTaxes.Add(new ProductTax
                {
                    TaxID = num,
                    tax = tax,
                    ProductID = product.ProductID,
                    Input = 0
                });

                if (saleProductSpecialTaxes.Where(p => p.tax.TaxTypeID == tax.TaxTypeID).Count() > 0)
                    saleProductSpecialTaxes.Remove(saleProductSpecialTaxes.Where(p => p.tax.TaxTypeID == tax.TaxTypeID).First());
            }
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
            product.Name = "";
            TestMinimalInformation();
        }

        public void CleanCode()
        {
            product.Code = 0;
            TestMinimalInformation();
        }

        private void TestMinimalInformation()
        {
            if(!string.IsNullOrWhiteSpace(product.Name) && product.Code > 0 && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            if (Information["editable"] == 0)
                TS_Page = new View.TS_PDT_Item_Load(Information["minimalInformation"], Information["external"]);
            else
                TS_Page = new View.TS_PDT_Item_Load_Edit(Information["minimalInformation"], Information["external"]);
            LeftSide.Content = TS_Page;
        }

        public Boolean ProductControlExist(string name)
        {
            List<Product> products = db.Products.ToList();
            foreach (var item in products)
            {
                if ((item.Name.ToLower() == name.ToLower() && product.Name.ToLower() != name.ToLower()) || name.Length == 0)
                {
                    CleanName();
                    return true;
                }
            }
            product.Name = name;
            TestMinimalInformation();
            return false;
        }

        public void SaveLoadProduct()
        {
            Product productFinal = db.Products.Where(p => p.ProductID == product.ProductID).First();
            productFinal.Code = product.Code;
            productFinal.Name = product.Name;

            db.Products.Update(productFinal);

            foreach(ProductTax item in purchaseProductTaxes)
            {
                if(item.ProductTaxID <= 0)
                {
                    if (db.ProductsTaxes.Where(pt => pt.Input == 1 && pt.ProductID == item.ProductID && pt.tax.TaxTypeID == item.tax.TaxTypeID).Count() > 0)
                        db.ProductsTaxes.Remove(db.ProductsTaxes.Where(pt => pt.Input == 1 && pt.ProductID == product.ProductID && pt.tax.TaxTypeID == item.tax.TaxTypeID).First());

                    db.ProductsTaxes.Add(new ProductTax
                    {
                        Input = 1,
                        TaxID = item.TaxID,
                        ProductID = item.ProductID
                    });
                }
            }

            foreach (ProductTax item in purchaseProductSpecialTaxes)
            {
                if (item.ProductTaxID <= 0)
                {
                    if (db.ProductsTaxes.Where(pt => pt.Input == 1 && pt.ProductID == item.ProductID && pt.tax.TaxTypeID == item.tax.TaxTypeID).Count() > 0)
                        db.ProductsTaxes.Remove(db.ProductsTaxes.Where(pt => pt.Input == 1 && pt.ProductID == product.ProductID && pt.tax.TaxTypeID == item.tax.TaxTypeID).First());

                    db.ProductsTaxes.Add(new ProductTax
                    {
                        Input = 1,
                        TaxID = item.TaxID,
                        ProductID = item.ProductID
                    });
                }
            }

            foreach (ProductTax item in saleProductTaxes)
            {
                if (item.ProductTaxID <= 0)
                {
                    if (db.ProductsTaxes.Where(pt => pt.Input == 0 && pt.ProductID == item.ProductID && pt.tax.TaxTypeID == item.tax.TaxTypeID).Count() > 0)
                        db.ProductsTaxes.Remove(db.ProductsTaxes.Where(pt => pt.Input == 0 && pt.ProductID == product.ProductID && pt.tax.TaxTypeID == item.tax.TaxTypeID).First());

                    db.ProductsTaxes.Add(new ProductTax
                    {
                        Input = 0,
                        TaxID = item.TaxID,
                        ProductID = item.ProductID
                    });
                }
            }

            foreach (ProductTax item in saleProductSpecialTaxes)
            {
                if (item.ProductTaxID <= 0)
                {
                    if (db.ProductsTaxes.Where(pt => pt.Input == 0 && pt.ProductID == item.ProductID && pt.tax.TaxTypeID == item.tax.TaxTypeID).Count() > 0)
                        db.ProductsTaxes.Remove(db.ProductsTaxes.Where(pt => pt.Input == 0 && pt.ProductID == product.ProductID && pt.tax.TaxTypeID == item.tax.TaxTypeID).First());

                    db.ProductsTaxes.Add(new ProductTax
                    {
                        Input = 0,
                        TaxID = item.TaxID,
                        ProductID = item.ProductID
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

        override public void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new View.NV_PDT_Item_Load(Information["external"]);
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_PDT_Item_Load(Information["minimalInformation"], Information["external"]);
                    else
                        TS_Page = new View.TS_PDT_Item_Load_Edit(Information["minimalInformation"], Information["external"]);
                    MC_Page = new View.MC_PDT_Item_Load_Product(Information["external"]);
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new View.NV_PDT_Item_Load(Information["external"]);
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_PDT_Item_Load(Information["minimalInformation"], Information["external"]);
                    else
                        TS_Page = new View.TS_PDT_Item_Load_Edit(Information["minimalInformation"], Information["external"]);
                    MC_Page = new View.MC_PDT_Item_Load_Product_Taxes(Information["external"]);
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
                    a.MainFrame.Content = new ProductMenu.Controller.CT_ProductMenu();
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