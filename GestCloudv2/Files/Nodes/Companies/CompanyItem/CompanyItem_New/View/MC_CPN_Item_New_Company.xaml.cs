using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace GestCloudv2.Files.Nodes.Companies.CompanyItem.CompanyItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_New_Company.xaml
    /// </summary>
    public partial class MC_CPN_Item_New_Company : Page
    {
        public MC_CPN_Item_New_Company()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            CB_CompanyCode.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Changes);
            /*CB_StartDayFiscalYear.SelectionChanged += new SelectionChangedEventHandler(EV_CB_StartDayChanges);
            CB_StartMonthFiscalYear.SelectionChanged += new SelectionChangedEventHandler(EV_CB_StartMonthChanges);
            CB_EndDayFiscalYear.SelectionChanged += new SelectionChangedEventHandler(EV_CB_EndDayChanges);
            CB_EndMonthFiscalYear.SelectionChanged += new SelectionChangedEventHandler(EV_CB_EndMonthChanges);*/
            TB_CompanyName.KeyUp += new KeyEventHandler(EV_UserName);
            TB_CompanyName.Loaded += new RoutedEventHandler(EV_UserName);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_CompanyName.Text = GetController().company.Name;
            DateTime month = Convert.ToDateTime("01/01/0001");

            List<Company> companies = GetController().GetCompanies();
            List<int> nums = new List<int>();
            List<int> startdayFP = new List<int>();
            List<int> startmonthFP = new List<int>();
            List<int> enddayFP = new List<int>();
            List<int> endmonthFP = new List<int>();
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
            month = Convert.ToDateTime("01/01/0001");
            for (int i = 1; i <= 31; i++)
            {
                if (!enddayFP.Contains(i))
                {
                    ComboBoxItem temp = new ComboBoxItem();
                    temp.Content = $"{i}";
                    temp.Name = $"endDayhFP{i}";
                    CB_EndDayFiscalYear.Items.Add(temp);
                }
            }
            
            for (int i = 1; i <= 12; i++)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(month.ToString("MMMM").ToLower())}";
                temp.Name = $"endMonthFP{i}";
                month = month.AddMonths(1);
                CB_EndMonthFiscalYear.Items.Add(temp);
            }

            foreach (ComboBoxItem item in CB_CompanyCode.Items)
            {
                if (item.Content.ToString() == $"{GetController().company.Code}")
                {
                    CB_CompanyCode.SelectedValue = item;
                    break;
                }
            }

            CB_StartDayFiscalYear.SelectedIndex = 0;
            CB_StartMonthFiscalYear.SelectedIndex = 0;
            CB_EndDayFiscalYear.SelectedIndex = 30;
            CB_EndMonthFiscalYear.SelectedIndex = 11;
        }

        private void EV_UserName(object sender, RoutedEventArgs e)
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

        private void EV_CB_Changes(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp2 = (ComboBoxItem)CB_CompanyCode.SelectedItem;
            if (temp2 != null)
            {
                GetController().SetCompanyCode(Convert.ToInt32(temp2.Name.Replace("companyCode", "")));
            }
        }

        /*private void EV_CB_StartMonthChanges(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp3 = (ComboBoxItem)CB_StartMonthFiscalYear.SelectedItem;
            MessageBox.Show($"{((ComboBoxItem)CB_StartMonthFiscalYear.SelectedItem).Name.Replace("startMonthFP","")}");
        }

        private void EV_CB_StartDayChanges(object sender, RoutedEventArgs e)
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

        private Controller.CT_CPN_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CPN_Item_New)a.MainFrame.Content;
        }
    }
}
