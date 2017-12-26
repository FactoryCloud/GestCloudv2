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

namespace GestCloudv2.Files.Nodes.ProductTypes.ProductTypeItem.ProductTypeItem_New.Controller
{
    /// <summary>
    /// Interaction logic for CT_STR_Item_New.xaml
    /// </summary>
    public partial class CT_PTY_Item_New : Main.Controller.CT_Common
    {
        public int lastProductType;
        public ProductType productType;
        public SubmenuItems submenuItems;
        public Tax tax;
        public TaxType purchaseTaxTypeSelected;
        public TaxType saleTaxTypeSelected;
        public Tax purchaseTaxSelected;
        public Tax purchaseSpecialTaxSelected;
        public Tax saleTaxSelected;
        public Tax saleSpecialTaxSelected;

        public CT_PTY_Item_New()
        {
            purchaseTaxTypeSelected = GetTaxTypes().OrderByDescending(t => t.StartDate).First();
            saleTaxTypeSelected = GetTaxTypes().OrderByDescending(t => t.StartDate).First();
            submenuItems = new SubmenuItems();
            productType = new ProductType();
            productType.Code = LastProductType();
            Information.Add("minimalInformation", 0);
            Information["entityValid"] = 1;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
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

        public void SetProductTypeName(string name)
        {
            productType.Name = name;
            TestMinimalInformation();
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

        public int GetProductTypeCode()
        {
            return productType.Code;
        }

        public string GetProductTypeName()
        {
            return productType.Name;
        }

        public decimal GetProductTypePurchasePrice1()
        {
            if (productType.PurchasePrice1 == null)
            {
                return 0.00M;
            }
            return (decimal)productType.PurchasePrice1;
        }

        public decimal GetProductTypePurchasePrice2()
        {
            if (productType.PurchasePrice2 == null)
            {
                return 0.00M;
            }
            return (decimal)productType.PurchasePrice2;
        }

        public decimal GetProductTypePurchaseDiscount1()
        {
            if (productType.PurchaseDiscount1 == null)
            {
                return 0.00M;
            }
            return (decimal)productType.PurchaseDiscount1;
        }

        public decimal GetProductTypePurchaseDiscount2()
        {
            if (productType.PurchaseDiscount2 == null)
            {
                return 0.00M;
            }
            return (decimal)productType.PurchaseDiscount2;
        }

        public decimal GetProductTypeSalePrice1()
        {
            if (productType.SalePrice1 == null)
            {
                return 0.00M;
            }
            return (decimal)productType.SalePrice1;
        }

        public decimal GetProductTypeSalePrice2()
        {
            if (productType.SalePrice2 == null)
            {
                return 0.00M;
            }
            return (decimal)productType.SalePrice2;
        }

        public decimal GetProductTypeSaleDiscount1()
        {
            if (productType.SaleDiscount1 == null)
            {
                return 0.00M;
            }
            return (decimal)productType.SaleDiscount1;
        }

        public decimal GetProductTypeSaleDiscount2()
        {
            if (productType.SaleDiscount2 == null)
            {
                return 0.00M;
            }
            return (decimal)productType.SaleDiscount2;
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

        public void SetProductTypeCode(int code)
        {
            productType.Code = code;
            TestMinimalInformation();
        }

        public void SetPurchasePrice1(decimal num)
        {
            productType.PurchasePrice1 = num;
        }

        public void SetPurchasePrice2(decimal num)
        {
            productType.PurchasePrice2 = num;
        }

        public void SetSalePrice1(decimal num)
        {
            productType.SalePrice1 = num;
        }

        public void SetSalePrice2(decimal num)
        {
            productType.SalePrice2 = num;
        }

        public void SetPurchaseDiscount1(decimal num)
        {
            productType.PurchaseDiscount1 = num;
        }

        public void SetPurchaseDiscount2(decimal num)
        {
            productType.PurchaseDiscount2= num;
        }

        public void SetSaleDiscount1(decimal num)
        {
            productType.SaleDiscount1 = num;
        }

        public void SetSaleDiscount2(decimal num)
        {
            productType.SaleDiscount2 = num;
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

        public bool EV_CodeValid(int num)
        {
            return db.ProductTypes.Where(pt => pt.Code == num).Count() == 0;
        }

        public int LastProductType()
        {
            if (db.ProductTypes.ToList().Count > 0)
            {
                lastProductType = db.ProductTypes.OrderBy(u => u.Code).Last().Code + 1;
                productType.Code = lastProductType;
                return lastProductType;
            }
            else
            {
                productType.Code = 1;
                return lastProductType = 1;
            }
        }

        public void CT_Menu()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        public void SaveNewProductType()
        {
            db.ProductTypes.Add(productType);

            if (purchaseTaxSelected != null)
            {
                db.ProductTypesTaxes.Add(
                    new ProductTypeTax
                    {
                        productType = productType,
                        TaxID = purchaseTaxSelected.TaxID,
                        Input = 1
                    });
            }

            if (purchaseSpecialTaxSelected != null)
            {
                db.ProductTypesTaxes.Add(
                    new ProductTypeTax
                    {
                        productType = productType,
                        TaxID = purchaseSpecialTaxSelected.TaxID,
                        Input = 1
                    });
            }

            if (saleTaxSelected != null)
            {
                db.ProductTypesTaxes.Add(
                    new ProductTypeTax
                    {
                        productType = productType,
                        TaxID = saleTaxSelected.TaxID,
                        Input = 0
                    });
            }

            if (saleSpecialTaxSelected != null)
            {
                db.ProductTypesTaxes.Add(
                    new ProductTypeTax
                    {
                        productType = productType,
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
            if (!string.IsNullOrWhiteSpace(productType.Name) && productType.Code > 0 && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            TS_Page = new View.TS_PTY_Item_New(Information["minimalInformation"]);
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
                    NV_Page = new View.NV_PTY_Item_New();
                    TS_Page = new View.TS_PTY_Item_New(Information["minimalInformation"]);
                    MC_Page = new View.MC_PTY_Item_New_ProductType();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new View.NV_PTY_Item_New();
                    TS_Page = new View.TS_PTY_Item_New(Information["minimalInformation"]);
                    MC_Page = new View.MC_PTY_Item_New_ProductType_Taxes();
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