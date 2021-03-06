﻿using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace GestCloudv2.Files.Nodes.Companies.CompanyItem.CompanyItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_New_Company.xaml
    /// </summary>
    public partial class MC_CPN_Item_New_Company : Page
    {
        public const string CORRESPONDENCIA = "TRWAGMYFPDXBNJZSQVHLCKE";

        public MC_CPN_Item_New_Company()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            CB_CompanyCode.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Changes);
            CB_CompanyPeriod.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Changes);
            CB_StartDayFiscalYear.SelectionChanged += new SelectionChangedEventHandler(EV_CB_StartDayChanges);
            CB_StartMonthFiscalYear.SelectionChanged += new SelectionChangedEventHandler(EV_CB_StartMonthChanges);
            //CB_EndDayFiscalYear.SelectionChanged += new SelectionChangedEventHandler(EV_CB_EndDayChanges);
            //CB_EndMonthFiscalYear.SelectionChanged += new SelectionChangedEventHandler(EV_CB_EndMonthChanges);
            TB_CompanyName.KeyUp += new KeyEventHandler(EV_CompanyName);
            TB_CompanyName.Loaded += new RoutedEventHandler(EV_CompanyName);
            TB_CompanyCIF.KeyUp += new KeyEventHandler(EV_CIF);
            TB_CompanyCIF.Loaded += new RoutedEventHandler(EV_CIF);
            TB_CompanyAddress.KeyUp += new KeyEventHandler(EV_Address);
            TB_CompanyAddress.Loaded += new RoutedEventHandler(EV_Address);
            TB_CompanyPhone1.KeyUp += new KeyEventHandler(EV_Phone1);
            TB_CompanyPhone1.Loaded += new RoutedEventHandler(EV_Phone1);
            TB_CompanyPhone2.KeyUp += new KeyEventHandler(EV_Phone2);
            TB_CompanyPhone2.Loaded += new RoutedEventHandler(EV_Phone2);
            TB_CompanyFax.KeyUp += new KeyEventHandler(EV_Fax);
            TB_CompanyFax.Loaded += new RoutedEventHandler(EV_Fax);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            PeriodOptions();
            TB_CompanyName.Text = GetController().company.Name;
            TB_CompanyCIF.Text = GetController().company.CIF;
            TB_CompanyAddress.Text = GetController().company.Address;

            if (GetController().company.Phone1 > 0)
            {
                TB_CompanyPhone1.Text = $"{GetController().company.Phone1}";
            }

            if (GetController().company.Phone2 > 0)
            {
                TB_CompanyPhone2.Text = $"{GetController().company.Phone2}";
            }

            if (GetController().company.Fax > 0)
            {
                TB_CompanyFax.Text = $"{GetController().company.Fax}";
            }

            DateTime month = Convert.ToDateTime("01/01/0001");

            List<Company> companies = GetController().GetCompanies();
            List<int> nums = new List<int>();
            List<int> startdayFP = new List<int>();
            List<int> startmonthFP = new List<int>();
            foreach (var comp in companies)
            {
                nums.Add(Convert.ToInt16(comp.Code));
            }

            for (int i = 1; i <= 20; i++)
            {
                if(!nums.Contains(i))
                {
                    ComboBoxItem temp = new ComboBoxItem();
                    temp.Content = $"{i}";
                    temp.Name = $"companyCode{i}";
                    CB_CompanyCode.Items.Add(temp);
                }
            }

            for (int i = 1; i <= 31; i++)
            {
                if (!startdayFP.Contains(i))
                {
                    ComboBoxItem temp = new ComboBoxItem();
                    temp.Content = $"{i}";
                    temp.Name = $"startDayFP{i}";
                    CB_StartDayFiscalYear.Items.Add(temp);
                }
            }

            for (int i = 1; i <= 12; i++)
            {
                if (!startmonthFP.Contains(i))
                {
                    ComboBoxItem temp = new ComboBoxItem();
                    temp.Content = $"{System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(month.ToString("MMMM").ToLower())}";
                    temp.Name = $"startMonthFP{i}";
                    month = month.AddMonths(1);
                    CB_StartMonthFiscalYear.Items.Add(temp);
                }
            }

            foreach (ComboBoxItem item in CB_CompanyCode.Items)
            {
                if (item.Content.ToString() == $"{GetController().company.Code}")
                {
                    CB_CompanyCode.SelectedValue = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in CB_StartMonthFiscalYear.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("startMonthFP", "")) == GetController().startMonthDate)
                {
                    CB_StartMonthFiscalYear.SelectedValue = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in CB_StartDayFiscalYear.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("startDayFP", "")) == GetController().startDayDate)
                {
                    CB_StartDayFiscalYear.SelectedValue = item;
                    break;
                }
            }
        }

        /*static public char LetraNIF(string dni)
        {
            Match match = new Regex(@"\b(\d{8})\b").Match(dni);
            if (match.Success)
                return CORRESPONDENCIA[int.Parse(dni) % 23];
            else
                throw new ArgumentException("El DNI debe contener 8 dígitos.");
        }*/

        private void EV_CompanyName(object sender, RoutedEventArgs e)
        {
            if(TB_CompanyName.Text.Length == 0)
            {
                if (SP_CompanyName.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyName.Children.Add(message);
                }

                else if (SP_CompanyName.Children.Count == 2)
                {
                    SP_CompanyName.Children.RemoveAt(SP_CompanyName.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyName.Children.Add(message);
                }
                GetController().CleanName();
            }

            else if (GetController().CompanyControlExist(TB_CompanyName.Text))
            {
                if (SP_CompanyName.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Esta empresa ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyName.Children.Add(message);
                }

                else if (SP_CompanyName.Children.Count == 2)
                {
                    SP_CompanyName.Children.RemoveAt(SP_CompanyName.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Esta empresa ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyName.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else
            {
                if (SP_CompanyName.Children.Count == 2)
                {
                    SP_CompanyName.Children.RemoveAt(SP_CompanyName.Children.Count - 1);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }
        }

        private void EV_CIF(object sender, RoutedEventArgs e)
        {
            if (TB_CompanyCIF.Text.Length == 8)
            {
                Match match = new Regex(@"\b(\d{8})\b").Match(TB_CompanyCIF.Text);
                if (match.Success)
                {
                    TB_CompanyCIF.Text = TB_CompanyCIF.Text + "-" + CORRESPONDENCIA[int.Parse(TB_CompanyCIF.Text) % 23].ToString();
                    GetController().SetCompanyCif(((TextBox)sender).Text);
                }

                else
                    throw new ArgumentException("El DNI debe contener 8 dígitos.");
            }

            /*if (TB_CompanyCIF.Text.Length == 0)
            {
                if (SP_CompanyCIF.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyCIF.Children.Add(message);
                }

                else if (SP_CompanyCIF.Children.Count == 2)
                {
                    SP_CompanyCIF.Children.RemoveAt(SP_CompanyCIF.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyCIF.Children.Add(message);
                }
                GetController().CleanCIF();
            }

            else if (GetController().CompanyControlExistCIF(TB_CompanyCIF.Text))
            {
                if (SP_CompanyCIF.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Esta empresa ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyCIF.Children.Add(message);
                }

                else if (SP_CompanyCIF.Children.Count == 2)
                {
                    SP_CompanyCIF.Children.RemoveAt(SP_CompanyCIF.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Esta empresa ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyCIF.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else
            {
                if (SP_CompanyCIF.Children.Count == 2)
                {
                    SP_CompanyCIF.Children.RemoveAt(SP_CompanyCIF.Children.Count - 1);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }*/
        }

        private void EV_Address(object sender, RoutedEventArgs e)
        {
            if (TB_CompanyFax.Text.Length > 0)
            {
                GetController().SetCompanyAddress(((TextBox)sender).Text);
            }
        }

        private void EV_Phone1(object sender, RoutedEventArgs e)
        {
            if (TB_CompanyPhone1.Text.Length > 0)
            {
                if (Regex.Matches(((TextBox)sender).Text, "[^0-9]").Count > 0)
                {
                    ((TextBox)sender).Text = Regex.Replace(((TextBox)sender).Text, "[^0-9]", "");
                    ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
                }
                GetController().SetPhone1(((TextBox)sender).Text);
            }
        }

        private void EV_Phone2(object sender, RoutedEventArgs e)
        {
            if (TB_CompanyPhone2.Text.Length > 0)
            {
                if (Regex.Matches(((TextBox)sender).Text, "[^0-9]").Count > 0)
                {
                    ((TextBox)sender).Text = Regex.Replace(((TextBox)sender).Text, "[^0-9]", "");
                    ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
                }
                GetController().SetPhone2(((TextBox)sender).Text);
            }
        }

        private void EV_Fax(object sender, RoutedEventArgs e)
        {
            if (TB_CompanyFax.Text.Length > 0)
            {
                if (Regex.Matches(((TextBox)sender).Text, "[^0-9]").Count > 0)
                {
                    ((TextBox)sender).Text = Regex.Replace(((TextBox)sender).Text, "[^0-9]", "");
                    ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
                }
                GetController().SetFax(((TextBox)sender).Text);
            }
        }

        private void EV_CB_Changes(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp2 = (ComboBoxItem)CB_CompanyCode.SelectedItem;
            if (temp2 != null)
            {
                GetController().SetCompanyCode(Convert.ToInt32(temp2.Name.Replace("companyCode", "")));
            }

            ComboBoxItem temp3 = (ComboBoxItem)CB_CompanyPeriod.SelectedItem;
            if (temp3 != null)
            {
                GetController().SetCompanyPeriodOption(Convert.ToInt32(temp3.Name.Replace("periodOption", "")));
            }
        }

        private void EV_CB_StartMonthChanges(object sender, RoutedEventArgs e)
        {
            CB_StartDayFiscalYear.Items.Clear();
            List<int> startdayFP = new List<int>();
            ComboBoxItem temp3 = (ComboBoxItem)CB_StartMonthFiscalYear.SelectedItem;
            //MessageBox.Show($"{((ComboBoxItem)CB_StartMonthFiscalYear.SelectedItem).Name.Replace("startMonthFP", "")}");
            switch (Convert.ToInt32(temp3.Name.Replace("startMonthFP", "")))
            {
                case int n when (n == 1 || n == 3 || n == 5 || n == 7 || n == 8 || n == 10 || n == 12):
                    for (int i = 1; i <= 31; i++)
                    {
                        if (!startdayFP.Contains(i))
                        {
                            ComboBoxItem temp = new ComboBoxItem();
                            temp.Content = $"{i}";
                            temp.Name = $"startDayFP{i}";
                            CB_StartDayFiscalYear.Items.Add(temp);
                        }
                    }

                    foreach (ComboBoxItem item in CB_StartDayFiscalYear.Items)
                    {
                        if (Convert.ToInt16(item.Name.Replace("startDayFP", "")) == GetController().startDayDate)
                        {
                            CB_StartDayFiscalYear.SelectedValue = item;
                            break;
                        }
                    }
                    break;

                case int n when (n == 4 || n == 6 || n == 9 || n == 11):
                    for (int i = 1; i <= 30; i++)
                    {
                        if (!startdayFP.Contains(i))
                        {
                            ComboBoxItem temp = new ComboBoxItem();
                            temp.Content = $"{i}";
                            temp.Name = $"startDayFP{i}";
                            CB_StartDayFiscalYear.Items.Add(temp);
                        }
                    }
                    if (GetController().startDayDate == 31)
                        CB_StartDayFiscalYear.SelectedIndex = 29;

                    else
                    {
                        foreach (ComboBoxItem item in CB_StartDayFiscalYear.Items)
                        {
                            if (Convert.ToInt16(item.Name.Replace("startDayFP", "")) == GetController().startDayDate)
                            {
                                CB_StartDayFiscalYear.SelectedValue = item;
                                break;
                            }
                        }
                    }
                    break;

                case 2:
                    for (int i = 1; i <= 28; i++)
                    {
                        if (!startdayFP.Contains(i))
                        {
                            ComboBoxItem temp = new ComboBoxItem();
                            temp.Content = $"{i}";
                            temp.Name = $"startDayFP{i}";
                            CB_StartDayFiscalYear.Items.Add(temp);
                        }
                    }
                    if (GetController().startDayDate > 28)
                        CB_StartDayFiscalYear.SelectedIndex = 27;

                    else
                    {
                        foreach (ComboBoxItem item in CB_StartDayFiscalYear.Items)
                        {
                            if (Convert.ToInt16(item.Name.Replace("startDayFP", "")) == GetController().startDayDate)
                            {
                                CB_StartDayFiscalYear.SelectedValue = item;
                                break;
                            }
                        }
                    }
                    break;
            }
            GetController().startMonthDate = Convert.ToInt32(temp3.Name.Replace("startMonthFP", ""));
        }

        private void EV_CB_StartDayChanges(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_StartDayFiscalYear.SelectedItem;
            if (temp1 != null)
            {
                GetController().startDayDate = Convert.ToInt32(temp1.Name.Replace("startDayFP", ""));
            }          
        }

        /*private void EV_CB_EndDayChanges(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp2 = (ComboBoxItem)CB_EndDayFiscalYear.SelectedItem;
            if (temp2 != null)
            {
                GetController().endDayDate = Convert.ToInt32(temp2.Name.Replace("endDayFP", ""));
            }
        }*/

        /*private void EV_CB_EndMonthChanges(object sender, RoutedEventArgs e)
        {
            CB_EndDayFiscalYear.Items.Clear();
            List<int> enddayFP = new List<int>();
            ComboBoxItem temp4 = (ComboBoxItem)CB_EndMonthFiscalYear.SelectedItem;
            //MessageBox.Show($"{temp4.Name.Replace("endMonthFP", "")}");
            switch (Convert.ToInt32(temp4.Name.Replace("endMonthFP", "")))
            {
                case int n when (n== 1 || n== 3 || n == 5 || n == 7 || n == 8 || n == 10 || n == 12):
                    for (int i = 1; i <= 31; i++)
                    {
                        if (!enddayFP.Contains(i))
                        {
                            ComboBoxItem temp = new ComboBoxItem();
                            temp.Content = $"{i}";
                            temp.Name = $"endDayFP{i}";
                            CB_EndDayFiscalYear.Items.Add(temp);
                        }
                    }
                    CB_EndDayFiscalYear.SelectedIndex = 30;
                    break;

                case int n when (n == 4 || n == 6 || n == 9 || n == 11):
                    for (int i = 1; i <= 30; i++)
                    {
                        if (!enddayFP.Contains(i))
                        {
                            ComboBoxItem temp = new ComboBoxItem();
                            temp.Content = $"{i}";
                            temp.Name = $"endDayFP{i}";
                            CB_EndDayFiscalYear.Items.Add(temp);
                        }
                    }
                    CB_EndDayFiscalYear.SelectedIndex = 29;
                    break;

                case 2:
                    for (int i = 1; i <= 28; i++)
                    {
                        if (!enddayFP.Contains(i))
                        {
                            ComboBoxItem temp = new ComboBoxItem();
                            temp.Content = $"{i}";
                            temp.Name = $"endDayFP{i}";
                            CB_EndDayFiscalYear.Items.Add(temp);
                        }
                    }
                    CB_EndDayFiscalYear.SelectedIndex = 27;
                    break;
            }
            GetController().endMonthDate = Convert.ToInt32(temp4.Name.Replace("endMonthFP", ""));

        }*/
        /*private void EV_CB_StartDayChanges(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp4 = (ComboBoxItem)CB_StartDayFiscalYear.SelectedItem;
        }

        private void EV_CB_EndMonthChanges(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp5 = (ComboBoxItem)CB_EndMonthFiscalYear.SelectedItem;
        }

        private void EV_CB_EndDayChanges(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp6 = (ComboBoxItem)CB_EndDayFiscalYear.SelectedItem;
        }*/

        public void PeriodOptions()
        {
            ComboBoxItem anual = new ComboBoxItem();
            anual.Content = $"Anual";
            anual.Name = $"periodOption1";
            CB_CompanyPeriod.Items.Add(anual);

            ComboBoxItem semestral = new ComboBoxItem();
            semestral.Content = $"Semestral";
            semestral.Name = $"periodOption2";
            CB_CompanyPeriod.Items.Add(semestral);

            ComboBoxItem trimestral = new ComboBoxItem();
            trimestral.Content = $"Trimestral";
            trimestral.Name = $"periodOption3";
            CB_CompanyPeriod.Items.Add(trimestral);

            ComboBoxItem monthly = new ComboBoxItem();
            monthly.Content = $"Mensual";
            monthly.Name = $"periodOption4";
            CB_CompanyPeriod.Items.Add(monthly);
        }

        private Controller.CT_CPN_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CPN_Item_New)a.MainFrame.Content;
        }
    }
}
