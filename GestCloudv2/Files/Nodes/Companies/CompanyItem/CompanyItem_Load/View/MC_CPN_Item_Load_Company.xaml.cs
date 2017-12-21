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
            TB_CompanyName.KeyUp += new KeyEventHandler(EV_CompanyName);
            TB_CompanyName.Loaded += new RoutedEventHandler(EV_CompanyName);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_CompanyName.Text = GetController().company.Name;
            PeriodOptions();

            if (GetController().Information["editable"] == 0)
            {
                TB_CompanyName.IsReadOnly = true;

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
                switch (GetController().company.PeriodOption)
                {
                    case 1:
                        TB_CompanyPeriodOption.Text = $"Anual";
                        break;

                    case 2:
                        TB_CompanyPeriodOption.Text = $"Semestral";
                        break;

                    case 3:
                        TB_CompanyPeriodOption.Text = $"Trimestral";
                        break;

                    case 4:
                        TB_CompanyPeriodOption.Text = $"Mensual";
                        break;
                }
                
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

                foreach (ComboBoxItem item in CB_CompanyPeriod.Items)
                {
                    if (Convert.ToInt16(item.Name.Replace("periodOption", "")) == GetController().company.PeriodOption)
                    {
                        CB_CompanyPeriod.SelectedValue = item;
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

        private void EV_CB_Changes(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp2 = (ComboBoxItem)CB_CompanyCode.SelectedItem;
            if (temp2 != null)
            {
                GetController().SetCompanyCode(Convert.ToInt32(temp2.Name.Replace("companyCode", "")));
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
