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

namespace GestCloudv2.Files.Nodes.PaymentMethods.PaymentMethodItem.PaymentMethodItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_Load_Company.xaml
    /// </summary>
    public partial class MC_PMT_Item_Load_PaymentMethod : Page
    {
        int external;
        public MC_PMT_Item_Load_PaymentMethod(int external)
        {
            InitializeComponent();

            this.external = external;

            this.Loaded += new RoutedEventHandler(EV_Start);

            CB_PaymentMethodCode.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Changes);
            TB_PaymentMethodName.KeyUp += new KeyEventHandler(EV_PaymentMethodName);
            TB_PaymentMethodName.Loaded += new RoutedEventHandler(EV_PaymentMethodName);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_PaymentMethodName.Text = GetController().paymentMethod.Name;

            if (GetController().Information["editable"] == 0)
            {
                TB_PaymentMethodName.IsReadOnly = true;

                Thickness margin = new Thickness(20);

                TextBox TB_PaymentMethodCode = new TextBox();
                TB_PaymentMethodCode.Name = "TB_StoreCode";
                TB_PaymentMethodCode.Text = $"{GetController().paymentMethod.Code}";
                TB_PaymentMethodCode.VerticalAlignment = VerticalAlignment.Center;
                TB_PaymentMethodCode.TextAlignment = TextAlignment.Center;
                TB_PaymentMethodCode.Margin = margin;
                Grid.SetColumn(TB_PaymentMethodCode, 2);
                Grid.SetRow(TB_PaymentMethodCode, 2);

                GR_Main.Children.Add(TB_PaymentMethodCode);

                CB_PaymentMethodCode.Visibility = Visibility.Hidden;
            }

            else
            {
                List<PaymentMethod> paymentMethods = GetController().GetPaymentMethods();
                List<int> nums = new List<int>();
                foreach (var pmt in paymentMethods)
                {
                    if(pmt.PaymentMethodID != GetController().paymentMethod.PaymentMethodID)
                        nums.Add(Convert.ToInt16(pmt.Code));
                }

                for (int i = 1; i <= 20; i++)
                {
                    if (!nums.Contains(i))
                    {
                        ComboBoxItem temp = new ComboBoxItem();
                        temp.Content = $"{i}";
                        temp.Name = $"paymentMethodCode{i}";
                        CB_PaymentMethodCode.Items.Add(temp);
                    }
                }

                foreach (ComboBoxItem item in CB_PaymentMethodCode.Items)
                {
                    if (item.Content.ToString() == $"{GetController().paymentMethod.Code}")
                    {
                        CB_PaymentMethodCode.SelectedValue = item;
                        break;
                    }
                }
            }
        }

        private void EV_PaymentMethodName(object sender, RoutedEventArgs e)
        {
            if(TB_PaymentMethodName.Text.Length == 0)
            {
                if (SP_PaymentMethodName.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_PaymentMethodName.Children.Add(message);
                }

                else if (SP_PaymentMethodName.Children.Count == 2)
                {
                    SP_PaymentMethodName.Children.RemoveAt(SP_PaymentMethodName.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_PaymentMethodName.Children.Add(message);
                }
                GetController().CleanName();
            }

            else if (GetController().CompanyControlExist(TB_PaymentMethodName.Text))
            {
                if (SP_PaymentMethodName.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Esta forma de pago ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_PaymentMethodName.Children.Add(message);
                }

                else if (SP_PaymentMethodName.Children.Count == 2)
                {
                    SP_PaymentMethodName.Children.RemoveAt(SP_PaymentMethodName.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Esta forma de pago ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_PaymentMethodName.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else
            {
                if (SP_PaymentMethodName.Children.Count == 2)
                {
                    SP_PaymentMethodName.Children.RemoveAt(SP_PaymentMethodName.Children.Count - 1);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }
        }

        private void EV_CB_Changes(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp2 = (ComboBoxItem)CB_PaymentMethodCode.SelectedItem;
            if (temp2 != null)
            {
                GetController().SetPaymentMethodCode(Convert.ToInt32(temp2.Name.Replace("paymentMethodCode", "")));
            }
        }

        private Controller.CT_PMT_Item_Load GetController()
        {
            if (external == 0)
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = (Main.View.MainWindow)mainWindow;
                return (Controller.CT_PMT_Item_Load)a.MainFrame.Content;
            }

            else
            {
                Window mainWindow = Application.Current.MainWindow;
                var a = ((Main.Controller.CT_Common)((Main.View.MainWindow)mainWindow).MainFrame.Content);
                return (Controller.CT_PMT_Item_Load)a.CT_Submenu.Subcontroller;
            }
        }
    }
}
