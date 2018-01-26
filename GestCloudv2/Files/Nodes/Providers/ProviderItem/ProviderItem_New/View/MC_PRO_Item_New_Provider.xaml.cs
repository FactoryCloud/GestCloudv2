using FrameworkDB.V1;
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

namespace GestCloudv2.Files.Nodes.Providers.ProviderItem.ProviderItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_CLI_Item_New_Client.xaml
    /// </summary>
    public partial class MC_PRO_Item_New_Provider : Page
    {
        public Provider provider;

        public MC_PRO_Item_New_Provider()
        {
            InitializeComponent();
            provider = new Provider();
            this.Loaded += new RoutedEventHandler(EV_Start);
            TB_ProviderCod.KeyUp += new KeyEventHandler(EV_ClientCod);

            CB_TaxPeriod.SelectionChanged += new SelectionChangedEventHandler(EV_CB_TaxPeriodUpdate);
            CB_Tax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_TaxUpdate);
            CB_SpecialTax.SelectionChanged += new SelectionChangedEventHandler(EV_CB_TaxUpdate);
            CB_EquivalenceSurcharge.SelectionChanged += new SelectionChangedEventHandler(EV_CB_TaxUpdate);
        }

        public void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_ProviderCod.Text = GetController().LastProviderCod().ToString();

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

        private void EV_ClientCod(object sender, RoutedEventArgs e)
        {
            if (TB_ProviderCod.Text.Length == 0)
            {
            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(TB_ProviderCod.Text, "[^0-9]"))
                {
                    if (SP_Providercod.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Solo se permiten números";
                        SP_Providercod.Children.Add(message);
                    }
                    TB_ProviderCod.Text = TB_ProviderCod.Text.Remove(TB_ProviderCod.Text.Length - 1);
                }

                else if (GetController().ProviderControlExist(int.Parse(TB_ProviderCod.Text)))
                {
                    if (SP_Providercod.Children.Count == 1)
                    {
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este usuario ya existe";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_Providercod.Children.Add(message);
                    }

                    else if (SP_Providercod.Children.Count == 2)
                    {
                        SP_Providercod.Children.RemoveAt(SP_Providercod.Children.Count - 1);
                        TextBlock message = new TextBlock();
                        message.TextWrapping = TextWrapping.WrapWithOverflow;
                        message.Text = "Este usuario ya existe";
                        message.HorizontalAlignment = HorizontalAlignment.Center;
                        SP_Providercod.Children.Add(message);
                    }
                    GetController().EV_UpdateIfNotEmpty(true);
                }

                else
                {
                    if (SP_Providercod.Children.Count == 2)
                    {
                        SP_Providercod.Children.RemoveAt(SP_Providercod.Children.Count - 1);
                    }
                }

                if (int.TryParse(TB_ProviderCod.Text, out int d))
                {
                    provider.Code = int.Parse(TB_ProviderCod.Text);
                }
            }
        }

        private void EV_CB_TaxPeriodUpdate(object sender, RoutedEventArgs e)
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
        }

        private void EV_CB_TaxUpdate(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            if (temp1 != null)
            {
                switch (Convert.ToInt32(((ComboBox)sender).Tag))
                {
                    case 1:
                        GetController().InformationTaxes[GetController().taxTypeSelected.TaxTypeID] = (Convert.ToInt32(temp1.Name.Replace("Tax", "")));
                        break;

                    case 2:
                        GetController().InformationEquivalenceSurcharges[GetController().taxTypeSelected.TaxTypeID] = (Convert.ToInt32(temp1.Name.Replace("EquiSur", "")));
                        break;
                    case 3:
                        GetController().InformationSpecialTaxes[GetController().taxTypeSelected.TaxTypeID] = (Convert.ToInt32(temp1.Name.Replace("SpecialTax", "")));
                        break;
                }
            }
        }

        private ProviderItem_New.Controller.CT_PRO_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (ProviderItem_New.Controller.CT_PRO_Item_New)a.MainFrame.Content;
        }
    }
}
