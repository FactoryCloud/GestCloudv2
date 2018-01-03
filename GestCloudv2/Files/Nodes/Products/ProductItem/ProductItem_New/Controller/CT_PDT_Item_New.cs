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

namespace GestCloudv2.Files.Nodes.Products.ProductItem.ProductItem_New.Controller
{
    /// <summary>
    /// Interaction logic for CT_STR_Item_New.xaml
    /// </summary>
    public partial class CT_PDT_Item_New : Main.Controller.CT_Common
    {
        public int lastProduct;
        public Product product;
        public SubmenuItems submenuItems;
        public Tax tax;
        public ProductType productTypeSelected;
        public TaxType purchaseTaxTypeSelected;
        public TaxType saleTaxTypeSelected;
        public Tax purchaseTaxSelected;
        public Tax purchaseSpecialTaxSelected;
        public Tax saleTaxSelected;
        public Tax saleSpecialTaxSelected;

        public CT_PDT_Item_New()
        {
            productTypeSelected = GetProductTypes().OrderByDescending(t => t.Code).First();
            purchaseTaxTypeSelected = GetTaxTypes().OrderByDescending(t => t.StartDate).First();
            saleTaxTypeSelected = GetTaxTypes().OrderByDescending(t => t.StartDate).First();
            submenuItems = new SubmenuItems();
            product = new Product();
            product.Code = LastProduct();
            Information.Add("minimalInformation", 0);
            Information["entityValid"] = 1;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SetProductTypeSelected(int num)
        {
            productTypeSelected = db.ProductTypes.Where(t => t.ProductTypeID == num).First();
        }

        public void SetPurchaseTaxSelected(int num)
        {
            purchaseTaxSelected = db.Taxes.Where(t => t.TaxID == num).First();
        }

        public void SetPurchaseSpecialTaxSelected(int num)
        {
            purchaseSpecialTaxSelected = db.Taxes.Where(t => t.TaxID == num).First();
        }

        public void SetSaleTaxSelected(int num)
        {
            saleTaxSelected = db.Taxes.Where(t => t.TaxID == num).First();
        }

        public void SetSaleSpecialTaxSelected(int num)
        {
            saleSpecialTaxSelected = db.Taxes.Where(t => t.TaxID == num).First();
        }

        public void SetPurchaseTaxTypeSelected(int num)
        {
            purchaseTaxTypeSelected = db.TaxTypes.Where(t => t.TaxTypeID == num).First();
        }

        public void SetSaleTaxTypeSelected(int num)
        {
            saleTaxTypeSelected = db.TaxTypes.Where(t => t.TaxTypeID == num).First();
        }

        public void SetProductName(string name)
        {
            product.Name = name;
            TestMinimalInformation();
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

        public int GetProductCode()
        {
            return Convert.ToInt32(product.Code);
        }

        public string GetProductName()
        {
            return product.Name;
        }

        public decimal GetProductPurchasePrice1()
        {
            if (product.PurchasePrice1 == null)
            {
                return 0.00M;
            }
            return (decimal)product.PurchasePrice1;
        }

        public decimal GetProductPurchasePrice2()
        {
            if (product.PurchasePrice2 == null)
            {
                return 0.00M;
            }
            return (decimal)product.PurchasePrice2;
        }

        public decimal GetProductPurchaseDiscount1()
        {
            if (product.PurchaseDiscount1 == null)
            {
                return 0.00M;
            }
            return (decimal)product.PurchaseDiscount1;
        }

        public decimal GetProductPurchaseDiscount2()
        {
            if (product.PurchaseDiscount2 == null)
            {
                return 0.00M;
            }
            return (decimal)product.PurchaseDiscount2;
        }

        public decimal GetProductSalePrice1()
        {
            if (product.SalePrice1 == null)
            {
                return 0.00M;
            }
            return (decimal)product.SalePrice1;
        }

        public decimal GetProductSalePrice2()
        {
            if (product.SalePrice2 == null)
            {
                return 0.00M;
            }
            return (decimal)product.SalePrice2;
        }

        public decimal GetProductSaleDiscount1()
        {
            if (product.SaleDiscount1 == null)
            {
                return 0.00M;
            }
            return (decimal)product.SaleDiscount1;
        }

        public decimal GetProductSaleDiscount2()
        {
            if (product.SaleDiscount2 == null)
            {
                return 0.00M;
            }
            return (decimal)product.SaleDiscount2;
        }

        public int GetPurchaseTaxSelected()
        {
            if (purchaseTaxSelected == null)
            {
                return 0;
            }
            return purchaseTaxSelected.TaxID;
        }

        public int GetPurchaseSpecialTaxSelected()
        {
            if (purchaseSpecialTaxSelected == null)
            {
                return 0;
            }
            return purchaseSpecialTaxSelected.TaxID;
        }

        public int GetSaleTaxSelected()
        {
            if (saleTaxSelected == null)
            {
                return 0;
            }
            return saleTaxSelected.TaxID;
        }

        public int GetSaleSpecialTaxSelected()
        {
            if (saleSpecialTaxSelected == null)
            {
                return 0;
            }
            return saleSpecialTaxSelected.TaxID;
        }

        public void SetProductCode(int code)
        {
            product.Code = code;
            TestMinimalInformation();
        }

        public void SetPurchasePrice1(decimal num)
        {
            product.PurchasePrice1 = num;
        }

        public void SetPurchasePrice2(decimal num)
        {
            product.PurchasePrice2 = num;
        }

        public void SetSalePrice1(decimal num)
        {
            product.SalePrice1 = num;
        }

        public void SetSalePrice2(decimal num)
        {
            product.SalePrice2 = num;
        }

        public void SetPurchaseDiscount1(decimal num)
        {
            product.PurchaseDiscount1 = num;
        }

        public void SetPurchaseDiscount2(decimal num)
        {
            product.PurchaseDiscount2= num;
        }

        public void SetSaleDiscount1(decimal num)
        {
            product.SaleDiscount1 = num;
        }

        public void SetSaleDiscount2(decimal num)
        {
            product.SaleDiscount2 = num;
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

        public bool EV_CodeValid(int num)
        {
            return db.Products.Where(pt => pt.Code == num).Count() == 0;
        }

        public int LastProduct()
        {
            /*if (db.Products.ToList().Any())
            {
                lastProduct = db.Products.OrderBy(u => u.Code).Last().Code + 1;
                product.Code = lastProduct;
                return 1;
            }
            else
            {
                product.Code = 1;
                return lastProduct = 1;
            }*/
            return 50001;
        }

        public void CT_Menu()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        public void SaveNewProduct()
        {
            db.Products.Add(product);
           // db.SaveChanges();

            if (purchaseTaxSelected != null)
            {
                db.ProductsTaxes.Add(
                    new ProductTax
                    {
                        product = product,
                        TaxID = purchaseTaxSelected.TaxID,
                        Input = 1
                    });
            }

            if (purchaseSpecialTaxSelected != null)
            {
                db.ProductsTaxes.Add(
                    new ProductTax
                    {
                        product = product,
                        TaxID = purchaseSpecialTaxSelected.TaxID,
                        Input = 1
                    });
            }

            if (saleTaxSelected != null)
            {
                db.ProductsTaxes.Add(
                    new ProductTax
                    {
                        product = product,
                        TaxID = saleTaxSelected.TaxID,
                        Input = 0
                    });
            }

            if (saleSpecialTaxSelected != null)
            {
                db.ProductsTaxes.Add(
                    new ProductTax
                    {
                        product = product,
                        TaxID = saleSpecialTaxSelected.TaxID,
                        Input = 0
                    });
            }

            db.SaveChanges();
            MessageBox.Show("Datos guardados correctamente");

            Information["fieldEmpty"] = 0;
            CT_Menu();
        }

        private void TestMinimalInformation()
        {
            if (!string.IsNullOrWhiteSpace(product.Name) && product.Code > 0 && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            TS_Page = new View.TS_PDT_Item_New(Information["minimalInformation"]);
            LeftSide.Content = TS_Page;
        }

        override public void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new View.NV_PDT_Item_New();
                    TS_Page = new View.TS_PDT_Item_New(Information["minimalInformation"]);
                    MC_Page = new View.MC_PDT_Item_New_Product();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new View.NV_PDT_Item_New();
                    TS_Page = new View.TS_PDT_Item_New(Information["minimalInformation"]);
                    MC_Page = new View.MC_PDT_Item_New_Product_Taxes();
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