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

namespace GestCloudv2.Files.Nodes.Companies.CompanyItem.CompanyItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_Load_Company.xaml
    /// </summary>
    public partial class MC_CPN_Item_Load_Company : Page
    {
        public MC_CPN_Item_Load_Company()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            CB_CompanyCode.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Changes);
            CB_CompanyPeriod.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Changes);
            CB_StartDayFiscalYear.SelectionChanged += new SelectionChangedEventHandler(EV_CB_StartDayChanges);
            CB_StartMonthFiscalYear.SelectionChanged += new SelectionChangedEventHandler(EV_CB_StartMonthChanges);

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
            DateTime month = Convert.ToDateTime("01/01/0001");
            List<int> startdayFP = new List<int>();
            List<int> startmonthFP = new List<int>();

            TB_CompanyName.Text = GetController().company.Name;
            TB_CompanyCIF.Text = GetController().company.CIF;
            TB_CompanyAddress.Text = GetController().company.Address;
            TB_CompanyPhone1.Text = $"{GetController().company.Phone1}";
            TB_CompanyPhone2.Text = $"{GetController().company.Phone2}";
            TB_CompanyFax.Text = $"{GetController().company.Fax}";
            PeriodOptions();

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

            foreach (ComboBoxItem item in CB_CompanyPeriod.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("periodOption", "")) == GetController().company.PeriodOption)
                {
                    CB_CompanyPeriod.SelectedValue = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in CB_StartMonthFiscalYear.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("startMonthFP", "")) == Convert.ToInt16(GetController().fiscalYears.Last().StartDate.ToString("MM")))
                {
                    CB_StartMonthFiscalYear.SelectedValue = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in CB_StartDayFiscalYear.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("startDayFP", "")) == Convert.ToInt16(GetController().fiscalYears.Last().StartDate.ToString("dd")))
                {
                    CB_StartDayFiscalYear.SelectedValue = item;
                    break;
                }
            }

            Thickness margin1 = new Thickness(20, 0, 0, 0);
            TextBox TB_CompanyStartDay = new TextBox();
            TB_CompanyStartDay.Name = "TB_CompanyStartDay";
            TB_CompanyStartDay.IsReadOnly = true;
            TB_CompanyStartDay.Text = $"{((ComboBoxItem)CB_StartDayFiscalYear.SelectedItem).Content}";
            TB_CompanyStartDay.VerticalAlignment = VerticalAlignment.Center;
            TB_CompanyStartDay.TextAlignment = TextAlignment.Center;
            TB_CompanyStartDay.Margin = margin1;

            GR_Date.Children.Add(TB_CompanyStartDay);

            CB_StartDayFiscalYear.Visibility = Visibility.Hidden;

            Thickness margin2 = new Thickness(0, 0, 20, 0);
            TextBox TB_CompanyStartMonth = new TextBox();
            TB_CompanyStartMonth.Name = "TB_CompanyStartMonth";
            TB_CompanyStartMonth.IsReadOnly = true;
            TB_CompanyStartMonth.Text = $"{((ComboBoxItem)CB_StartMonthFiscalYear.SelectedItem).Content}";
            TB_CompanyStartMonth.VerticalAlignment = VerticalAlignment.Center;
            TB_CompanyStartMonth.TextAlignment = TextAlignment.Center;
            TB_CompanyStartMonth.Margin = margin2;
            Grid.SetColumn(TB_CompanyStartMonth, 1);

            GR_Date.Children.Add(TB_CompanyStartMonth);

            CB_StartMonthFiscalYear.Visibility = Visibility.Hidden;

            if (GetController().Information["editable"] == 0)
            {
                TB_CompanyName.IsReadOnly = true;
                TB_CompanyCIF.IsReadOnly = true;
                TB_CompanyAddress.IsReadOnly = true;
                TB_CompanyPhone1.IsReadOnly = true;
                TB_CompanyPhone2.IsReadOnly = true;
                TB_CompanyFax.IsReadOnly = true;

                Thickness margin = new Thickness(20);

                TextBox TB_CompanyCode = new TextBox();
                TB_CompanyCode.Name = "TB_CompanyCode";
                TB_CompanyCode.IsReadOnly = true;
                TB_CompanyCode.Text = $"{GetController().company.Code}";
                TB_CompanyCode.VerticalAlignment = VerticalAlignment.Center;
                TB_CompanyCode.TextAlignment = TextAlignment.Center;
                TB_CompanyCode.Margin = margin;
                Grid.SetColumn(TB_CompanyCode, 2);
                Grid.SetRow(TB_CompanyCode, 2);

                GR_Main.Children.Add(TB_CompanyCode);

                CB_CompanyCode.Visibility = Visibility.Hidden;

                TextBox TB_CompanyPeriodOption = new TextBox();
                TB_CompanyPeriodOption.Name = "TB_CompanyPeriodOption";
                TB_CompanyPeriodOption.IsReadOnly = true;
                TB_CompanyPeriodOption.Text = $"{((ComboBoxItem)CB_CompanyPeriod.SelectedItem).Content}";
                TB_CompanyPeriodOption.VerticalAlignment = VerticalAlignment.Center;
                TB_CompanyPeriodOption.TextAlignment = TextAlignment.Center;
                TB_CompanyPeriodOption.Margin = margin;
                Grid.SetColumn(TB_CompanyPeriodOption, 2);
                Grid.SetRow(TB_CompanyPeriodOption, 3);

                GR_Main.Children.Add(TB_CompanyPeriodOption);

                CB_CompanyPeriod.Visibility = Visibility.Hidden; 

            }

            else
            {
                List<Company> companies = GetController().GetCompanies();
                List<int> nums = new List<int>();
                foreach (var comp in companies)
                {
                    if (comp.CompanyID != GetController().company.CompanyID)
                        nums.Add(Convert.ToInt16(comp.Code));
                }

                for (int i = 1; i <= 20; i++)
                {
                    if (!nums.Contains(i))
                    {
                        ComboBoxItem temp = new ComboBoxItem();
                        temp.Content = $"{i}";
                        temp.Name = $"companyCode{i}";
                        CB_CompanyCode.Items.Add(temp);
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

                
            }
        }

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
            if (TB_CompanyCIF.Text.Length == 0)
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
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else if (GetController().CompanyControlExist(TB_CompanyCIF.Text))
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
            }
        }

        private void EV_Address(object sender, RoutedEventArgs e)
        {
            if (TB_CompanyAddress.Text.Length == 0)
            {
                if (SP_CompanyAddress.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyAddress.Children.Add(message);
                }

                else if (SP_CompanyAddress.Children.Count == 2)
                {
                    SP_CompanyAddress.Children.RemoveAt(SP_CompanyAddress.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyAddress.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else if (GetController().CompanyControlExist(TB_CompanyAddress.Text))
            {
                if (SP_CompanyAddress.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Esta empresa ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyAddress.Children.Add(message);
                }

                else if (SP_CompanyAddress.Children.Count == 2)
                {
                    SP_CompanyAddress.Children.RemoveAt(SP_CompanyAddress.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Esta empresa ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyAddress.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else
            {
                if (SP_CompanyAddress.Children.Count == 2)
                {
                    SP_CompanyAddress.Children.RemoveAt(SP_CompanyAddress.Children.Count - 1);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }
        }

        private void EV_Phone1(object sender, RoutedEventArgs e)
        {
            if (TB_CompanyPhone1.Text.Length == 0)
            {
                if (SP_CompanyPhone1.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyPhone1.Children.Add(message);
                }

                else if (SP_CompanyPhone1.Children.Count == 2)
                {
                    SP_CompanyPhone1.Children.RemoveAt(SP_CompanyPhone1.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyPhone1.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else if (GetController().CompanyControlExist(TB_CompanyPhone1.Text))
            {
                if (SP_CompanyPhone1.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Esta empresa ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyPhone1.Children.Add(message);
                }

                else if (SP_CompanyPhone1.Children.Count == 2)
                {
                    SP_CompanyPhone1.Children.RemoveAt(SP_CompanyPhone1.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Esta empresa ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyPhone1.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else
            {
                if (SP_CompanyPhone1.Children.Count == 2)
                {
                    SP_CompanyPhone1.Children.RemoveAt(SP_CompanyPhone1.Children.Count - 1);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }
        }

        private void EV_Phone2(object sender, RoutedEventArgs e)
        {
            if (TB_CompanyPhone2.Text.Length == 0)
            {
                if (SP_CompanyPhone2.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyPhone2.Children.Add(message);
                }

                else if (SP_CompanyPhone2.Children.Count == 2)
                {
                    SP_CompanyPhone2.Children.RemoveAt(SP_CompanyPhone2.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyPhone2.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else if (GetController().CompanyControlExist(TB_CompanyPhone2.Text))
            {
                if (SP_CompanyPhone2.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Esta empresa ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyPhone2.Children.Add(message);
                }

                else if (SP_CompanyPhone2.Children.Count == 2)
                {
                    SP_CompanyPhone2.Children.RemoveAt(SP_CompanyPhone2.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Esta empresa ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyPhone2.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else
            {
                if (SP_CompanyPhone2.Children.Count == 2)
                {
                    SP_CompanyPhone2.Children.RemoveAt(SP_CompanyPhone2.Children.Count - 1);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }
        }

        private void EV_Fax(object sender, RoutedEventArgs e)
        {
            if (TB_CompanyFax.Text.Length == 0)
            {
                if (SP_CompanyFax.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyFax.Children.Add(message);
                }

                else if (SP_CompanyFax.Children.Count == 2)
                {
                    SP_CompanyFax.Children.RemoveAt(SP_CompanyFax.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyFax.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else if (GetController().CompanyControlExist(TB_CompanyFax.Text))
            {
                if (SP_CompanyFax.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Esta empresa ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyFax.Children.Add(message);
                }

                else if (SP_CompanyFax.Children.Count == 2)
                {
                    SP_CompanyFax.Children.RemoveAt(SP_CompanyFax.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Esta empresa ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_CompanyFax.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else
            {
                if (SP_CompanyFax.Children.Count == 2)
                {
                    SP_CompanyFax.Children.RemoveAt(SP_CompanyFax.Children.Count - 1);
                }
                GetController().EV_UpdateIfNotEmpty(true);
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
                        if (Convert.ToInt16(item.Name.Replace("startDayFP", "")) == Convert.ToInt16(GetController().fiscalYears.Last().StartDate.ToString("dd")))
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
                            if (Convert.ToInt16(item.Name.Replace("startDayFP", "")) == Convert.ToInt16(GetController().fiscalYears.Last().StartDate.ToString("dd")))
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
                            if (Convert.ToInt16(item.Name.Replace("startDayFP", "")) == Convert.ToInt16(GetController().fiscalYears.Last().StartDate.ToString("dd")))
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

            ComboBoxItem temp3 = (ComboBoxItem)CB_CompanyPeriod.SelectedItem;
            if (temp3 != null)
            {
                GetController().SetCompanyPeriodOption(Convert.ToInt32(temp3.Name.Replace("periodOption", "")));
            }
        }

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

        private Controller.CT_CPN_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CPN_Item_Load)a.MainFrame.Content;
        }
    }
}
