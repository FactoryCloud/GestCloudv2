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

namespace GestCloudv2.Files.Nodes.ProductTypes.ProductTypeItem.ProductTypeItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_Load_Company.xaml
    /// </summary>
    public partial class MC_PTY_Item_Load_ProductType_Taxes : Page
    {
        int external;
        public MC_PTY_Item_Load_ProductType_Taxes(int external)
        {
            InitializeComponent();
            this.external = external;

            CB_PurchaseTaxPeriod.SelectionChanged += new SelectionChangedEventHandler(EV_CB_PurchasePeriod);
            CB_SaleTaxPeriod.SelectionChanged += new SelectionChangedEventHandler(EV_CB_SalePeriod);
            this.Loaded += new RoutedEventHandler(EV_Start);  
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_PurchaseDiscount1.Text = $"{GetController().productType.PurchaseDiscount1}";
            TB_PurchaseDiscount2.Text = $"{GetController().productType.PurchaseDiscount2}";
            TB_SaleDiscount1.Text = $"{GetController().productType.SaleDiscount1}";
            TB_SaleDiscount2.Text = $"{GetController().productType.SaleDiscount2}";

            TB_PurchasePrice1.Text = $"{GetController().productType.PurchasePrice1}";
            TB_PurchasePrice2.Text = $"{GetController().productType.PurchasePrice2}";
            TB_SalePrice1.Text = $"{GetController().productType.SalePrice1}";
            TB_SalePrice2.Text = $"{GetController().productType.SalePrice2}";

            List<TaxType> taxTypes = GetController().GetTaxTypes();
            foreach (TaxType tx in taxTypes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{tx.StartDate.ToString("dd/MM/yyyy")} - {tx.EndDate.ToString("dd/MM/yyyy")}";
                temp.Name = $"PurchaseTaxType{tx.TaxTypeID}";
                CB_PurchaseTaxPeriod.Items.Add(temp);
            }

            foreach (TaxType tx in taxTypes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{tx.StartDate.ToString("dd/MM/yyyy")} - {tx.EndDate.ToString("dd/MM/yyyy")}";
                temp.Name = $"SaleTaxType{tx.TaxTypeID}";
                CB_SaleTaxPeriod.Items.Add(temp);
            }

            foreach (ComboBoxItem item in CB_PurchaseTaxPeriod.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("PurchaseTaxType", "")) == GetController().purchaseTaxTypeSelected.TaxTypeID)
                {
                    CB_PurchaseTaxPeriod.SelectedValue = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in CB_SaleTaxPeriod.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("SaleTaxType", "")) == GetController().saleTaxTypeSelected.TaxTypeID)
                {
                    CB_SaleTaxPeriod.SelectedValue = item;
                    break;
                }
            }

            if (GetController().Information["editable"] == 0)
            {
                TB_SalePrice1.IsReadOnly = true;
                TB_SalePrice2.IsReadOnly = true;
                TB_PurchasePrice1.IsReadOnly = true;
                TB_PurchasePrice2.IsReadOnly = true;

                TB_SaleDiscount1.IsReadOnly = true;
                TB_SaleDiscount2.IsReadOnly = true;
                TB_PurchaseDiscount1.IsReadOnly = true;
                TB_PurchaseDiscount2.IsReadOnly = true;

                CB_PurchaseTaxPeriod.SelectedItem = GetController().GetProductTypes();
            }
        }

        private void EV_CB_PurchasePeriod(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_PurchaseTaxPeriod.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetPurchaseTaxTypeSelected(Convert.ToInt32(temp1.Name.Replace("PurchaseTaxType", "")));
            }

            List<Tax> taxes = GetController().GetTaxes();
            List<Tax> equiSurs = GetController().GetEquiSurs();
            List<Tax> specTaxes = GetController().GetSpecTaxes();
            foreach (Tax tx in taxes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                if (equiSurs.Where(t => t.Type == tx.Type).Count() >= 1)
                {
                    temp.Content = $"Tipo {tx.Type}: IVA {tx.Percentage.ToString("0.##")}% - RE {equiSurs.Where(t => t.Type == tx.Type).First().Percentage.ToString("0.##")}%";
                }
                else
                {
                    temp.Content = $"Tipo {tx.Type}: IVA {tx.Percentage.ToString("0.##")}%";
                }
                temp.Name = $"PurchaseTax{tx.TaxID}";
                CB_PurchaseTax.Items.Add(temp);
            }
            foreach (Tax tx in specTaxes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"Tipo {tx.Type}: {tx.Percentage.ToString("0.##")}%";
                temp.Name = $"PurchaseSpecialTax{tx.TaxID}";
                CB_PurchaseSpecialTax.Items.Add(temp);
            }

            ProductTypeTax purchaseTax = GetController().purchaseProductTypeTaxes.Where(c => c.tax.taxType.StartDate == GetController().purchaseTaxTypeSelected.StartDate && c.tax.taxType.EndDate== GetController().purchaseTaxTypeSelected.EndDate).First();
            foreach (ComboBoxItem item in CB_PurchaseTax.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("PurchaseTax", "")) == purchaseTax.tax.TaxID)
                {
                    CB_PurchaseTax.SelectedValue = item;
                    break;
                }
            }

            ProductTypeTax purchaseSpecialTax = GetController().purchaseProductTypeSpecialTaxes.Where(c => c.tax.taxType.StartDate == GetController().purchaseTaxTypeSelected.StartDate && c.tax.taxType.EndDate == GetController().purchaseTaxTypeSelected.EndDate).First();
            foreach (ComboBoxItem item in CB_PurchaseSpecialTax.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("PurchaseSpecialTax", "")) == purchaseSpecialTax.tax.TaxID)
                {
                    CB_PurchaseSpecialTax.SelectedValue = item;
                    break;
                }
            }
        }


        private void EV_CB_SalePeriod(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_SaleTaxPeriod.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetSaleTaxTypeSelected(Convert.ToInt32(temp1.Name.Replace("SaleTaxType", "")));
            }

            List<Tax> taxes = GetController().GetTaxes();
            List<Tax> equiSurs = GetController().GetEquiSurs();
            List<Tax> specTaxes = GetController().GetSpecTaxes();
            foreach (Tax tx in taxes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                if (equiSurs.Where(t => t.Type == tx.Type).Count() >= 1)
                {
                    temp.Content = $"Tipo {tx.Type}: IVA {tx.Percentage.ToString("0.##")}% - RE {equiSurs.Where(t => t.Type == tx.Type).First().Percentage.ToString("0.##")}%";
                }
                else
                {
                    temp.Content = $"Tipo {tx.Type}: IVA {tx.Percentage.ToString("0.##")}%";
                }
                temp.Name = $"SaleTax{tx.TaxID}";
                CB_SaleTax.Items.Add(temp);
            }
            foreach (Tax tx in specTaxes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"Tipo {tx.Type}: {tx.Percentage.ToString("0.##")}%";
                temp.Name = $"SaleSpecialTax{tx.TaxID}";
                CB_SaleSpecialTax.Items.Add(temp);
            }

            ProductTypeTax saleTax = GetController().saleProductTypeTaxes.Where(c => c.tax.taxType.StartDate == GetController().saleTaxTypeSelected.StartDate && c.tax.taxType.EndDate == GetController().saleTaxTypeSelected.EndDate).First();
            foreach (ComboBoxItem item in CB_SaleTax.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("SaleTax", "")) == saleTax.tax.TaxID)
                {
                    CB_SaleTax.SelectedValue = item;
                    break;
                }
            }

            ProductTypeTax saleSpecialTax = GetController().saleProductTypeSpecialTaxes.Where(c => c.tax.taxType.StartDate == GetController().saleTaxTypeSelected.StartDate && c.tax.taxType.EndDate == GetController().saleTaxTypeSelected.EndDate).First();
            foreach (ComboBoxItem item in CB_SaleSpecialTax.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("SaleSpecialTax", "")) == saleSpecialTax.tax.TaxID)
                {
                    CB_SaleSpecialTax.SelectedValue = item;
                    break;
                }
            }
        }

        private Controller.CT_PTY_Item_Load GetController()
        {
            if (external == 0)
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = (Main.View.MainWindow)mainWindow;
                return (Controller.CT_PTY_Item_Load)a.MainFrame.Content;
            }

            else
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = ((Main.Controller.CT_Common)((Main.View.MainWindow)mainWindow).MainFrame.Content);
                return (Controller.CT_PTY_Item_Load)a.CT_Submenu.Subcontroller;
            }
        }
    }
}
