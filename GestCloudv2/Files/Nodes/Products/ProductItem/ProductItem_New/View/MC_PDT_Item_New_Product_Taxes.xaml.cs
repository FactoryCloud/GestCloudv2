using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace GestCloudv2.Files.Nodes.Products.ProductItem.ProductItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_New_Company.xaml
    /// </summary>
    public partial class MC_PDT_Item_New_Product_Taxes : Page
    {
        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.LastIndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        public MC_PDT_Item_New_Product_Taxes()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);

            CB_PurchaseTaxPeriod.SelectionChanged += new SelectionChangedEventHandler(EV_CB_PurchasePeriod);
            CB_PurchaseTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_PurchaseTaxUpdate);
            CB_PurchaseSpecialTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_PurchaseSpecialTaxUpdate);
            CB_SaleTaxPeriod.SelectionChanged += new SelectionChangedEventHandler(EV_CB_SalePeriod);
            CB_SaleTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_SaleTaxUpdate);
            CB_SaleSpecialTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_SaleSpecialTaxUpdate);

            TB_PurchasePrice1.KeyUp += new KeyEventHandler(EV_TB_PriceUpdate);
            TB_PurchasePrice2.KeyUp += new KeyEventHandler(EV_TB_PriceUpdate);
            TB_SalePrice1.KeyUp += new KeyEventHandler(EV_TB_PriceUpdate);
            TB_SalePrice2.KeyUp += new KeyEventHandler(EV_TB_PriceUpdate);

            TB_PurchaseDiscount1.KeyUp += new KeyEventHandler(EV_TB_DiscountUpdate);
            TB_PurchaseDiscount2.KeyUp += new KeyEventHandler(EV_TB_DiscountUpdate);
            TB_SaleDiscount1.KeyUp += new KeyEventHandler(EV_TB_DiscountUpdate);
            TB_SaleDiscount2.KeyUp += new KeyEventHandler(EV_TB_DiscountUpdate);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_PurchasePrice1.Text = $"{GetController().GetProductPurchasePrice1()}";
            TB_PurchasePrice2.Text = $"{GetController().GetProductPurchasePrice2()}";
            TB_SalePrice1.Text = $"{GetController().GetProductSalePrice1()}";
            TB_SalePrice2.Text = $"{GetController().GetProductSalePrice2()}";

            TB_PurchaseDiscount1.Text = $"{GetController().GetProductPurchaseDiscount1()}";
            TB_PurchaseDiscount2.Text = $"{GetController().GetProductPurchaseDiscount2()}";
            TB_SaleDiscount1.Text = $"{GetController().GetProductSaleDiscount1()}";
            TB_SaleDiscount2.Text = $"{GetController().GetProductSaleDiscount2()}";

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

            foreach (ComboBoxItem item in CB_PurchaseTax.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("PurchaseTax", "")) == GetController().GetPurchaseTaxSelected())
                {
                    CB_PurchaseTax.SelectedValue = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in CB_PurchaseSpecialTax.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("PurchaseSpecialTax", "")) == GetController().GetPurchaseSpecialTaxSelected())
                {
                    CB_PurchaseSpecialTax.SelectedValue = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in CB_SaleTax.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("SaleTax", "")) == GetController().GetSaleTaxSelected())
                {
                    CB_SaleTax.SelectedValue = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in CB_SaleSpecialTax.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("SaleSpecialTax", "")) == GetController().GetSaleSpecialTaxSelected())
                {
                    CB_SaleSpecialTax.SelectedValue = item;
                    break;
                }
            }
        }

        private void EV_TB_PriceUpdate(object sender, KeyEventArgs e)
        {
            EV_DecimalValid(((TextBox)sender));
            if (((TextBox)sender).Text.Length > 0)
            {
                switch (Convert.ToInt16(((TextBox)sender).Tag))
                {
                    case 1:
                        GetController().SetPurchasePrice1(Convert.ToDecimal(((TextBox)sender).Text));
                        break;

                    case 2:
                        GetController().SetPurchasePrice2(Convert.ToDecimal(((TextBox)sender).Text));
                        break;

                    case 3:
                        GetController().SetSalePrice1(Convert.ToDecimal(((TextBox)sender).Text));
                        break;

                    case 4:
                        GetController().SetSalePrice2(Convert.ToDecimal(((TextBox)sender).Text));
                        break;
                }
            }
        }

        private void EV_TB_DiscountUpdate(object sender, KeyEventArgs e)
        {
            EV_DecimalValid(((TextBox)sender));
            if (((TextBox)sender).Text.Length > 0)
            {
                switch (Convert.ToInt16(((TextBox)sender).Tag))
                {
                    case 1:
                        GetController().SetPurchaseDiscount1(Convert.ToDecimal(((TextBox)sender).Text));
                        break;

                    case 2:
                        GetController().SetPurchaseDiscount2(Convert.ToDecimal(((TextBox)sender).Text));
                        break;

                    case 3:
                        GetController().SetSaleDiscount1(Convert.ToDecimal(((TextBox)sender).Text));
                        break;

                    case 4:
                        GetController().SetSaleDiscount2(Convert.ToDecimal(((TextBox)sender).Text));
                        break;
                }
            }
        }

        private void EV_DecimalValid(TextBox TB_Price)
        {
            if (Regex.Matches(TB_Price.Text, "[^0-9,]").Count > 0)
            {
                TB_Price.Text = Regex.Replace(TB_Price.Text, "[.]", ",");
                TB_Price.Text = Regex.Replace(TB_Price.Text, "[^0-9,]", "");
                TB_Price.SelectionStart = TB_Price.Text.Length;
            }

            if (Regex.Matches(TB_Price.Text, "[.,]").Count > 1)
            {
                TB_Price.Text = ReplaceLastOccurrence(TB_Price.Text, ",", "");
                TB_Price.SelectionStart = TB_Price.Text.Length;
            }
        }

        private void EV_CB_PurchaseTaxUpdate(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_PurchaseTax.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetPurchaseTaxSelected(Convert.ToInt32(temp1.Name.Replace("PurchaseTax", "")));
            }
        }

        private void EV_CB_PurchaseSpecialTaxUpdate(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_PurchaseSpecialTax.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetPurchaseSpecialTaxSelected(Convert.ToInt32(temp1.Name.Replace("PurchaseSpecialTax", "")));
            }
        }

        private void EV_CB_SaleTaxUpdate(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_SaleTax.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetSaleTaxSelected(Convert.ToInt32(temp1.Name.Replace("SaleTax", "")));
            }
        }

        private void EV_CB_SaleSpecialTaxUpdate(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_SaleSpecialTax.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetSaleSpecialTaxSelected(Convert.ToInt32(temp1.Name.Replace("SaleSpecialTax", "")));
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
        }

        private void EV_CompaniesChange(object sender, RoutedEventArgs e)
        {
            //GetController().UpdateCompanies(Convert.ToInt32((sender as CheckBox).Tag.ToString().Replace("company", "")));
        }

        private void EV_MD_StoresAll(object sender, RoutedEventArgs e)
        {
            //GetController().MD_StoresChange(1);
        }

        private void EV_MD_StoresNone(object sender, RoutedEventArgs e)
        {
            //GetController().MD_StoresChange(0);
        }

        private Controller.CT_PDT_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PDT_Item_New)a.MainFrame.Content;
        }
    }
}
