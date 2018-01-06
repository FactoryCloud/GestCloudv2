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

namespace GestCloudv2.Files.Nodes.Clients.ClientItem.ClientItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_USR_Item_New_User.xaml
    /// </summary>
    public partial class MC_CLI_Item_Load_Client : Page
    {
        int external;

        public MC_CLI_Item_Load_Client(int external)
        {
            InitializeComponent();

            this.external = external;

            this.Loaded += new RoutedEventHandler(EV_Start);
            TB_ClientCode.KeyUp += new KeyEventHandler(EV_ClientCode);
            TB_ClientCode.Loaded += new RoutedEventHandler(EV_ClientCode);
            CB_TaxPeriod.SelectionChanged += new SelectionChangedEventHandler(EV_PeriodUpdate);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_ClientCode.Text = $"{GetController().client.Code}";

            List<TaxType> taxTypes = GetController().GetTaxTypes();
            foreach (TaxType tx in taxTypes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{tx.StartDate.ToString("dd/MM/yyyy")} - {tx.EndDate.ToString("dd/MM/yyyy")}";
                temp.Name = $"TaxPeriod{tx.TaxTypeID}";
                CB_TaxPeriod.Items.Add(temp);
            }

            foreach (ComboBoxItem item in CB_TaxPeriod.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("TaxPeriod", "")) == GetController().taxTypeSelected.TaxTypeID)
                {
                    CB_TaxPeriod.SelectedValue = item;
                    break;
                }
            }
        }

        private void EV_ClientCode(object sender, RoutedEventArgs e)
        {
            if (GetController().Information["editable"] == 1)
            {
                if (TB_ClientCode.Text.Length == 0)
                {
                    if (SP_ClientCode.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este campo no puede estar vacio";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ClientCode.Children.Add(message);
                    }

                    else if (SP_ClientCode.Children.Count == 2)
                    {
                        SP_ClientCode.Children.RemoveAt(SP_ClientCode.Children.Count - 1);
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este campo no puede estar vacio";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ClientCode.Children.Add(message);
                    }
                    GetController().CleanCod();
                }

                else if (TB_ClientCode.Text.Any(x => Char.IsWhiteSpace(x)))
                {
                    if (SP_ClientCode.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este campo no puede contener espacios";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ClientCode.Children.Add(message);
                    }

                    else if (SP_ClientCode.Children.Count == 2)
                    {
                        SP_ClientCode.Children.RemoveAt(SP_ClientCode.Children.Count - 1);
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este campo no puede contener espacios";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ClientCode.Children.Add(message);
                    }
                    GetController().CleanCod();
                }
                
                else if (GetController().UserControlExist(Convert.ToInt32(TB_ClientCode.Text)))
                {
                    if (SP_ClientCode.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este usuario ya existe";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ClientCode.Children.Add(message);
                    }

                    else if (SP_ClientCode.Children.Count == 2)
                    {
                        SP_ClientCode.Children.RemoveAt(SP_ClientCode.Children.Count - 1);
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este usuario ya existe";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_ClientCode.Children.Add(message);
                    }
                    GetController().EV_UpdateIfNotEmpty(true);
                }

                else
                {
                    if (SP_ClientCode.Children.Count == 2)
                    {
                        SP_ClientCode.Children.RemoveAt(SP_ClientCode.Children.Count - 1);
                    }
                    GetController().EV_UpdateIfNotEmpty(true);
                }
            }
        }

        public void EV_PeriodUpdate(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem temp1 = (ComboBoxItem)CB_TaxPeriod.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetTaxTypeSelected(Convert.ToInt32(temp1.Name.Replace("TaxPeriod", "")));
            }

            foreach (ComboBoxItem item in CB_Tax.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("Tax", "")) == GetController().InformationTaxes[Convert.ToInt32(temp1.Name.Replace("TaxPeriod", ""))])
                {
                    CB_Tax.SelectedValue = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in CB_EquivalenceSurcharge.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("EquiSur", "")) == GetController().InformationEquivalenceSurcharges[Convert.ToInt32(temp1.Name.Replace("TaxPeriod", ""))])
                {
                    CB_EquivalenceSurcharge.SelectedValue = item;
                    break;
                }
            }

            CB_SpecialTax.Items.Clear();

            List<Tax> specTaxes = GetController().GetSpecTaxes();

            ComboBoxItem defaultSpecTax = new ComboBoxItem();
            defaultSpecTax.Content = $"No";
            defaultSpecTax.Name = $"SpecialTax0";
            CB_SpecialTax.Items.Add(defaultSpecTax);

            foreach (Tax tx in specTaxes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"Tipo {tx.Type}: {tx.Percentage.ToString("0.##")}%";
                temp.Name = $"SpecialTax{tx.TaxID}";
                CB_SpecialTax.Items.Add(temp);
            }

            foreach (ComboBoxItem item in CB_SpecialTax.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("SpecialTax", "")) == GetController().InformationSpecialTaxes[Convert.ToInt32(temp1.Name.Replace("TaxPeriod", ""))])
                {
                    CB_SpecialTax.SelectedValue = item;
                    break;
                }
            }

            if (GetController().Information["editable"] == 0)
            {
                TB_ClientCode.IsReadOnly = true;

                TB_Tax.Text = $"{((ComboBoxItem)CB_Tax.SelectedItem).Content}";
                TB_Tax.Visibility = Visibility.Visible;
                CB_Tax.Visibility = Visibility.Hidden;

                TB_EquivalenceSurcharge.Text = $"{((ComboBoxItem)CB_EquivalenceSurcharge.SelectedItem).Content}";
                TB_EquivalenceSurcharge.Visibility = Visibility.Visible;
                CB_EquivalenceSurcharge.Visibility = Visibility.Hidden;

                TB_SpecialTax.Text = $"{((ComboBoxItem)CB_SpecialTax.SelectedItem).Content}";
                TB_SpecialTax.Visibility = Visibility.Visible;
                CB_SpecialTax.Visibility = Visibility.Hidden;
            }
        }

        private Controller.CT_CLI_Item_Load GetController()
        {
            if (external == 0)
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = (Main.View.MainWindow)mainWindow;
                return (Controller.CT_CLI_Item_Load)a.MainFrame.Content;
            }

            else
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = ((Main.Controller.CT_Common)((Main.View.MainWindow)mainWindow).MainFrame.Content);
                return (Controller.CT_CLI_Item_Load)a.CT_Submenu.Subcontroller;
            }
        }
    }
}
