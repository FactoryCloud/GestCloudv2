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
            TB_CompanyName.KeyUp += new KeyEventHandler(EV_UserName);
            TB_CompanyName.Loaded += new RoutedEventHandler(EV_UserName);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_CompanyName.Text = GetController().company.Name;

            if (GetController().Information["editable"] == 0)
            {
                TB_CompanyName.IsReadOnly = true;

                Thickness margin = new Thickness(20);

                TextBox TB_UserCode = new TextBox();
                TB_UserCode.Name = "TB_CompanyCode";
                TB_UserCode.Text = $"{GetController().company.Code}";
                TB_UserCode.VerticalAlignment = VerticalAlignment.Center;
                TB_UserCode.TextAlignment = TextAlignment.Center;
                TB_UserCode.Margin = margin;
                Grid.SetColumn(TB_UserCode, 2);
                Grid.SetRow(TB_UserCode, 2);

                GR_Main.Children.Add(TB_UserCode);

                CB_CompanyCode.Visibility = Visibility.Hidden;
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

        private Controller.CT_CPN_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CPN_Item_Load)a.MainFrame.Content;
        }
    }
}
