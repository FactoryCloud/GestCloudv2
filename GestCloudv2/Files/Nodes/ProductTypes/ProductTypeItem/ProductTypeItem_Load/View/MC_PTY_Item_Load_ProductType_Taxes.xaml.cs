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
            CB_PurchaseTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_PurchaseTax);
            CB_PurchaseSpecialTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_PurchaseSpecialTax);
            CB_SaleTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_SaleTax);
            CB_SaleSpecialTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_SaleSpecialTax);
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
            if (GetController().Information["editable"] == 0 && (TextBox)this.FindName("TB_SaleTax") != null)
            {
                //MessageBox.Show("SI");
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
