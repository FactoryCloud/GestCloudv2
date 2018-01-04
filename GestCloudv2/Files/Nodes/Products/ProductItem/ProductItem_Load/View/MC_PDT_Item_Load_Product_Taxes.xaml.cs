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

namespace GestCloudv2.Files.Nodes.Products.ProductItem.ProductItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_Load_Company.xaml
    /// </summary>
    public partial class MC_PDT_Item_Load_Product_Taxes : Page
    {
        int external;
        public MC_PDT_Item_Load_Product_Taxes(int external)
        {
            InitializeComponent();
            this.external = external;

            CB_PurchaseTaxPeriod.SelectionChanged += new SelectionChangedEventHandler(EV_CB_PurchasePeriod);
            CB_SaleTaxPeriod.SelectionChanged += new SelectionChangedEventHandler(EV_CB_SalePeriod);
            CB_PurchaseTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_PurchaseTax);
            CB_PurchaseSpecialTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_PurchaseSpecialTax);
            CB_SaleTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_SaleTax);
            CB_SaleSpecialTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_SaleSpecialTax);

            TB_PurchaseDiscount1.KeyUp += new KeyEventHandler(EV_PurchaseDiscount1Update);
            TB_PurchaseDiscount2.KeyUp += new KeyEventHandler(EV_PurchaseDiscount2Update);
            TB_SaleDiscount1.KeyUp += new KeyEventHandler(EV_SaleDiscount1Update);
            TB_SaleDiscount2.KeyUp += new KeyEventHandler(EV_SaleDiscount2Update);

            TB_SalePrice1.KeyUp += new KeyEventHandler(EV_SalePrice1Update);
            TB_SalePrice2.KeyUp += new KeyEventHandler(EV_SalePrice2Update);
            TB_PurchasePrice1.KeyUp += new KeyEventHandler(EV_PurchasePrice1Update);
            TB_PurchasePrice2.KeyUp += new KeyEventHandler(EV_PurchasePrice2Update);

            this.Loaded += new RoutedEventHandler(EV_Start);  
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_PurchaseDiscount1.Text = $"{GetController().product.PurchaseDiscount1}";
            TB_PurchaseDiscount2.Text = $"{GetController().product.PurchaseDiscount2}";
            TB_SaleDiscount1.Text = $"{GetController().product.SaleDiscount1}";
            TB_SaleDiscount2.Text = $"{GetController().product.SaleDiscount2}";

            TB_PurchasePrice1.Text = $"{GetController().product.PurchasePrice1}";
            TB_PurchasePrice2.Text = $"{GetController().product.PurchasePrice2}";
            TB_SalePrice1.Text = $"{GetController().product.SalePrice1}";
            TB_SalePrice2.Text = $"{GetController().product.SalePrice2}";

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

                TB_PurchaseTax.Text = $"{((ComboBoxItem)CB_PurchaseTax.SelectedItem).Content}";
                TB_PurchaseTax.Visibility = Visibility.Visible;
                CB_PurchaseTax.Visibility = Visibility.Hidden;

                TB_PurchaseSpecialTax.Text = $"{((ComboBoxItem)CB_PurchaseSpecialTax.SelectedItem).Content}";
                TB_PurchaseSpecialTax.Visibility = Visibility.Visible;
                CB_PurchaseSpecialTax.Visibility = Visibility.Hidden;

                TB_SaleTax.Text = $"{((ComboBoxItem)CB_SaleTax.SelectedItem).Content}";
                TB_SaleTax.Visibility = Visibility.Visible;
                CB_SaleTax.Visibility = Visibility.Hidden;

                TB_SaleSpecialTax.Text = $"{((ComboBoxItem)CB_SaleSpecialTax.SelectedItem).Content}";
                TB_SaleSpecialTax.Visibility = Visibility.Visible;
                CB_SaleSpecialTax.Visibility = Visibility.Hidden;
            }

            else if (GetController().Information["validProduct"] == 0)
            {
                TB_PurchaseTax.Text = $"{((ComboBoxItem)CB_PurchaseTax.SelectedItem).Content}";
                TB_PurchaseTax.Visibility = Visibility.Visible;
                CB_PurchaseTax.Visibility = Visibility.Hidden;

                TB_PurchaseSpecialTax.Text = $"{((ComboBoxItem)CB_PurchaseSpecialTax.SelectedItem).Content}";
                TB_PurchaseSpecialTax.Visibility = Visibility.Visible;
                CB_PurchaseSpecialTax.Visibility = Visibility.Hidden;

                TB_SaleTax.Text = $"{((ComboBoxItem)CB_SaleTax.SelectedItem).Content}";
                TB_SaleTax.Visibility = Visibility.Visible;
                CB_SaleTax.Visibility = Visibility.Hidden;

                TB_SaleSpecialTax.Text = $"{((ComboBoxItem)CB_SaleSpecialTax.SelectedItem).Content}";
                TB_SaleSpecialTax.Visibility = Visibility.Visible;
                CB_SaleSpecialTax.Visibility = Visibility.Hidden;
            }
        }

        private void EV_PurchaseDiscount1Update(object sender, KeyEventArgs e)
        {
            if (TB_PurchaseDiscount1.Text.Length > 0)
            {
                GetController().product.PurchaseDiscount1 = Convert.ToDecimal(TB_PurchaseDiscount1.Text.Replace(",", "."));
            }
        }

        private void EV_PurchaseDiscount2Update(object sender, KeyEventArgs e)
        {
            if (TB_PurchaseDiscount2.Text.Length > 0)
            {
                GetController().product.PurchaseDiscount2 = Convert.ToDecimal(TB_PurchaseDiscount2.Text.Replace(",", "."));
            }
        }

        private void EV_SaleDiscount1Update(object sender, KeyEventArgs e)
        {
            if (TB_SaleDiscount1.Text.Length > 0)
            {
                GetController().product.SaleDiscount1 = Convert.ToDecimal(TB_SaleDiscount1.Text.Replace(",", "."));
            }
        }

        private void EV_SaleDiscount2Update(object sender, KeyEventArgs e)
        {
            if (TB_SaleDiscount2.Text.Length > 0)
            {
                GetController().product.SaleDiscount2 = Convert.ToDecimal(TB_SaleDiscount1.Text.Replace(",", "."));
            }
        }

        private void EV_PurchasePrice1Update(object sender, KeyEventArgs e)
        {
            if (TB_PurchasePrice1.Text.Length > 0)
            {
                GetController().product.PurchasePrice1 = Convert.ToDecimal(TB_PurchasePrice1.Text.Replace(",", "."));
            }
        }

        private void EV_PurchasePrice2Update(object sender, KeyEventArgs e)
        {
            if (TB_PurchasePrice2.Text.Length > 0)
            {
                GetController().product.PurchasePrice2 = Convert.ToDecimal(TB_PurchasePrice2.Text.Replace(",", "."));
            }
        }

        private void EV_SalePrice1Update(object sender, KeyEventArgs e)
        {
            if (TB_SalePrice1.Text.Length > 0)
            {
                GetController().product.SalePrice1 = Convert.ToDecimal(TB_SalePrice1.Text.Replace(",", "."));
            }
        }

        private void EV_SalePrice2Update(object sender, KeyEventArgs e)
        {
            if (TB_SalePrice2.Text.Length > 0)
            {
                GetController().product.SalePrice2 = Convert.ToDecimal(TB_SalePrice2.Text.Replace(",", "."));
            }
        }

        private void EV_CB_PurchasePeriod(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_PurchaseTaxPeriod.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetPurchaseTaxTypeSelected(Convert.ToInt32(temp1.Name.Replace("PurchaseTaxType", "")));
            }

            CB_PurchaseTax.Items.Clear();
            CB_PurchaseSpecialTax.Items.Clear();

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

            Tax purchaseTax;
            Tax purchaseSpecialTax;

            if (GetController().Information["validProduct"] == 1)
            {
                purchaseTax = GetController().purchaseProductTaxes.Where(c => c.tax.taxType.StartDate == GetController().purchaseTaxTypeSelected.StartDate && c.tax.taxType.EndDate == GetController().purchaseTaxTypeSelected.EndDate).First().tax;
                purchaseSpecialTax = GetController().purchaseProductSpecialTaxes.Where(c => c.tax.taxType.StartDate == GetController().purchaseTaxTypeSelected.StartDate && c.tax.taxType.EndDate == GetController().purchaseTaxTypeSelected.EndDate).First().tax; 
            }

            else
            {
                purchaseTax = GetController().purchaseProductTypeTaxes.Where(c => c.tax.taxType.StartDate == GetController().purchaseTaxTypeSelected.StartDate && c.tax.taxType.EndDate == GetController().purchaseTaxTypeSelected.EndDate).First().tax;
                purchaseSpecialTax = GetController().purchaseProductTypeSpecialTaxes.Where(c => c.tax.taxType.StartDate == GetController().purchaseTaxTypeSelected.StartDate && c.tax.taxType.EndDate == GetController().purchaseTaxTypeSelected.EndDate).First().tax;

            }

            foreach (ComboBoxItem item in CB_PurchaseTax.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("PurchaseTax", "")) == purchaseTax.TaxID)
                {
                    CB_PurchaseTax.SelectedValue = item;
                    break;
                }
            }

            TB_PurchaseTax.Text = $"{((ComboBoxItem)CB_PurchaseTax.SelectedItem).Content}";

            foreach (ComboBoxItem item in CB_PurchaseSpecialTax.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("PurchaseSpecialTax", "")) == purchaseSpecialTax.TaxID)
                {
                    CB_PurchaseSpecialTax.SelectedValue = item;
                    break;
                }
            }

            TB_PurchaseSpecialTax.Text = $"{((ComboBoxItem)CB_PurchaseSpecialTax.SelectedItem).Content}";

            if (GetController().Information["editable"]==0 && (TextBox)this.FindName("TB_PurchaseTax") != null)
            {
                EV_NonEditablePurchase();
            }
           
        }


        private void EV_CB_SalePeriod(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_SaleTaxPeriod.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetSaleTaxTypeSelected(Convert.ToInt32(temp1.Name.Replace("SaleTaxType", "")));
            }

            CB_SaleTax.Items.Clear();
            CB_SaleSpecialTax.Items.Clear();

            List<Tax> taxes = GetController().GetSaleTaxes();
            List<Tax> equiSurs = GetController().GetSaleEquiSurs();
            List<Tax> specTaxes = GetController().GetSaleSpecTaxes();

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

            Tax saleTax;
            Tax saleSpecialTax;

            if (GetController().Information["validProduct"] == 1)
            {
                saleTax = GetController().saleProductTaxes.Where(c => c.tax.taxType.StartDate == GetController().saleTaxTypeSelected.StartDate && c.tax.taxType.EndDate == GetController().saleTaxTypeSelected.EndDate).First().tax;
                saleSpecialTax = GetController().saleProductSpecialTaxes.Where(c => c.tax.taxType.StartDate == GetController().saleTaxTypeSelected.StartDate && c.tax.taxType.EndDate == GetController().saleTaxTypeSelected.EndDate).First().tax;
            }

            else
            {
                saleTax = GetController().saleProductTypeTaxes.Where(c => c.tax.taxType.StartDate == GetController().saleTaxTypeSelected.StartDate && c.tax.taxType.EndDate == GetController().saleTaxTypeSelected.EndDate).First().tax;
                saleSpecialTax = GetController().saleProductTypeSpecialTaxes.Where(c => c.tax.taxType.StartDate == GetController().saleTaxTypeSelected.StartDate && c.tax.taxType.EndDate == GetController().saleTaxTypeSelected.EndDate).First().tax;

            }

            foreach (ComboBoxItem item in CB_SaleTax.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("SaleTax", "")) == saleTax.TaxID)
                {
                    CB_SaleTax.SelectedValue = item;
                    break;
                }
            }

            TB_SaleTax.Text = $"{((ComboBoxItem)CB_SaleTax.SelectedItem).Content}";

            foreach (ComboBoxItem item in CB_SaleSpecialTax.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("SaleSpecialTax", "")) == saleSpecialTax.TaxID)
                {
                    CB_SaleSpecialTax.SelectedValue = item;
                    break;
                }
            }

            TB_SaleSpecialTax.Text = $"{((ComboBoxItem)CB_SaleSpecialTax.SelectedItem).Content}";

            if (GetController().Information["editable"] == 0 && (TextBox)this.FindName("TB_SaleTax") != null)
            {
                EV_NonEditableSale();
            }
        }

        private void EV_NonEditablePurchase()
        {
            TextBox TB_PurchaseTax = (TextBox)this.FindName($"TB_PurchaseTax");
            TB_PurchaseTax.Text = $"{((ComboBoxItem)CB_PurchaseTax.SelectedItem).Content}";

            TextBox TB_PurchaseSpecialTax = (TextBox)this.FindName($"TB_PurchaseSpecialTax");
            TB_PurchaseSpecialTax.Text = $"{((ComboBoxItem)CB_PurchaseSpecialTax.SelectedItem).Content}";
        }

        private void EV_NonEditableSale()
        {
            TextBox TB_SaleTax = (TextBox)this.FindName($"TB_SaleTax");
            TB_SaleTax.Text = $"{((ComboBoxItem)CB_SaleTax.SelectedItem).Content}";

            TextBox TB_SaleSpecialTax = (TextBox)this.FindName($"TB_SaleSpecialTax");
            TB_SaleSpecialTax.Text = $"{((ComboBoxItem)CB_SaleSpecialTax.SelectedItem).Content}";
        }

        private void EV_CB_PurchaseTax(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_PurchaseTax.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetPurchaseTax(Convert.ToInt32(temp1.Name.Replace("PurchaseTax", "")));
            }
        }

        private void EV_CB_PurchaseSpecialTax(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_PurchaseSpecialTax.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetPurchaseSpecialTax(Convert.ToInt32(temp1.Name.Replace("PurchaseSpecialTax", "")));
            }
        }

        private void EV_CB_SaleTax(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_SaleTax.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetSaleTax(Convert.ToInt32(temp1.Name.Replace("SaleTax", "")));
            }
        }

        private void EV_CB_SaleSpecialTax(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_SaleSpecialTax.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetSaleSpecialTax(Convert.ToInt32(temp1.Name.Replace("SaleSpecialTax", "")));
            }
        }

        private Controller.CT_PDT_Item_Load GetController()
        {
            if (external == 0)
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = (Main.View.MainWindow)mainWindow;
                return (Controller.CT_PDT_Item_Load)a.MainFrame.Content;
            }

            else
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = ((Main.Controller.CT_Common)((Main.View.MainWindow)mainWindow).MainFrame.Content);
                return (Controller.CT_PDT_Item_Load)a.CT_Submenu.Subcontroller;
            }
        }
    }
}
