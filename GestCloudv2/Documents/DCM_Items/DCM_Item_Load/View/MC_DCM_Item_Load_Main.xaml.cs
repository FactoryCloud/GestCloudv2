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
using FrameworkView.V1;

namespace GestCloudv2.Documents.DCM_Items.DCM_Item_Load.View
{
    /// <summary>
    /// Interaction logic for MC_POR_Item_Load_Main.xaml
    /// </summary>
    public partial class MC_DCM_Item_Load_Main : Page
    {

        public MC_DCM_Item_Load_Main()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);
            CB_Stores.SelectionChanged += new SelectionChangedEventHandler(EV_StoreSelect);
            CB_PaymentMethod.SelectionChanged += new SelectionChangedEventHandler(EV_PaymentMethodSelect);
            DP_Date.KeyDown += new KeyEventHandler(EV_Cancel);
            DP_Date.KeyUp += new KeyEventHandler(EV_Cancel);

            GR_Provider.MouseEnter += new MouseEventHandler(EV_MouseChange);
            GR_Provider.MouseLeave += new MouseEventHandler(EV_MouseChange);
            GR_Provider.MouseLeftButtonUp += new MouseButtonEventHandler(EV_MouseClick);

            GR_Client.MouseEnter += new MouseEventHandler(EV_MouseChange);
            GR_Client.MouseLeave += new MouseEventHandler(EV_MouseChange);
            GR_Client.MouseLeftButtonUp += new MouseButtonEventHandler(EV_MouseClick);

            GR_Store.MouseEnter += new MouseEventHandler(EV_MouseChange);
            GR_Store.MouseLeave += new MouseEventHandler(EV_MouseChange);
            GR_Store.MouseLeftButtonUp += new MouseButtonEventHandler(EV_MouseClick);

            GR_PaymentMethod.MouseEnter += new MouseEventHandler(EV_MouseChange);
            GR_PaymentMethod.MouseLeave += new MouseEventHandler(EV_MouseChange);
            GR_PaymentMethod.MouseLeftButtonUp += new MouseButtonEventHandler(EV_MouseClick);

            SetSelected();
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            switch (GetController().Information["operationType"])
            {
                case 1:
                    GR_Provider.Visibility = Visibility.Visible;
                    TB_ProviderCode.Text = GetController().GetProvider().ProviderID.ToString();
                    TB_ProviderName.Text = GetController().GetProvider().entity.Name;
                    break;

                case 2:
                    GR_Client.Visibility = Visibility.Visible;
                    TB_ClientCode.Text = GetController().GetClient().Code.ToString();
                    TB_ClientName.Text = GetController().GetClient().entity.Name;
                    break;
            }
            TB_Code.Text = GetController().GetCode();
            
            DP_Date.SelectedDate = Convert.ToDateTime(GetController().GetDate());

            if (GetController().Information["editable"] == 0)
            {
                TB_Code.IsReadOnly = true;

                Thickness margin = new Thickness(20);

                TextBox TB_StoreCode = new TextBox();
                TB_StoreCode.Name = "TB_StoreCode";
                TB_StoreCode.Text = $"{GetController().GetStore().Name}";
                TB_StoreCode.VerticalAlignment = VerticalAlignment.Center;
                TB_StoreCode.TextAlignment = TextAlignment.Center;
                TB_StoreCode.IsReadOnly = true;
                TB_StoreCode.Margin = margin;
                Grid.SetColumn(TB_StoreCode, 2);
                Grid.SetRow(TB_StoreCode, 3);

                GR_Store.Children.Add(TB_StoreCode);

                CB_Stores.Visibility = Visibility.Hidden;

                TextBox TB_PaymentMethodCode = new TextBox();
                TB_PaymentMethodCode.Name = "TB_PaymentMethodCode";
                TB_PaymentMethodCode.Text = $"{GetController().GetPaymentMethod().Name}";
                TB_PaymentMethodCode.VerticalAlignment = VerticalAlignment.Center;
                TB_PaymentMethodCode.TextAlignment = TextAlignment.Center;
                TB_PaymentMethodCode.IsReadOnly = true;
                TB_PaymentMethodCode.Margin = margin;
                Grid.SetColumn(TB_PaymentMethodCode, 2);
                Grid.SetRow(TB_PaymentMethodCode, 5);

                GR_PaymentMethod.Children.Add(TB_PaymentMethodCode);

                CB_PaymentMethod.Visibility = Visibility.Hidden;

                TextBox TB_DateStockAdjust = new TextBox();
                TB_DateStockAdjust.Name = "TB_DateStockAdjust";
                TB_DateStockAdjust.Text = $"{String.Format("{0:dd/MM/yyyy}", GetController().GetDate())}"; ;
                TB_DateStockAdjust.VerticalAlignment = VerticalAlignment.Center;
                TB_DateStockAdjust.TextAlignment = TextAlignment.Center;
                TB_DateStockAdjust.Margin = margin;
                Grid.SetColumn(TB_DateStockAdjust, 2);
                Grid.SetRow(TB_DateStockAdjust, 1);

                GR_Date.Children.Add(TB_DateStockAdjust);

                DP_Date.Visibility = Visibility.Hidden;


            }

            else
            {
                List<Store> stores = GetController().GetStores();
                //CB_Stores.SelectedIndex = Convert.ToInt16(GetController().store.Code);
                //List<int> nums = new List<int>();

                Thickness margin = new Thickness(20);

                TextBox TB_StoreCode = new TextBox();
                TB_StoreCode.Name = "TB_StoreCode";
                TB_StoreCode.Text = $"{GetController().GetStore().Code}";
                TB_StoreCode.VerticalAlignment = VerticalAlignment.Center;
                TB_StoreCode.TextAlignment = TextAlignment.Center;
                TB_StoreCode.Margin = margin;
                Grid.SetColumn(TB_StoreCode, 2);
                Grid.SetRow(TB_StoreCode, 3);

                GR_Store.Children.Add(TB_StoreCode);

                CB_Stores.Visibility = Visibility.Hidden;


                List<PaymentMethod> paymentMethods = GetController().GetPaymentMethods();
                //CB_Stores.SelectedIndex = Convert.ToInt16(GetController().store.Code);

                TextBox TB_PaymentMethod = new TextBox();
                TB_PaymentMethod.Name = "TB_PaymentMethodCode";
                TB_PaymentMethod.Text = $"{GetController().GetPaymentMethod().Code}";
                TB_PaymentMethod.VerticalAlignment = VerticalAlignment.Center;
                TB_PaymentMethod.TextAlignment = TextAlignment.Center;
                TB_PaymentMethod.Margin = margin;
                Grid.SetColumn(TB_PaymentMethod, 2);
                Grid.SetRow(TB_PaymentMethod, 5);

                GR_PaymentMethod.Children.Add(TB_PaymentMethod);

                CB_PaymentMethod.Visibility = Visibility.Hidden;
            }
        }

        private void SetTransparentAll()
        {
            GR_Store.Background = new SolidColorBrush(Colors.Transparent);
            GR_PaymentMethod.Background = new SolidColorBrush(Colors.Transparent);
            GR_Provider.Background = new SolidColorBrush(Colors.Transparent);
            GR_Client.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void SetSelected()
        {
            switch (GetController().Information["submode"])
            {
                case 4:
                    GR_Store.Background = new SolidColorBrush(Colors.Green);
                    break;

                case 5:
                    GR_PaymentMethod.Background = new SolidColorBrush(Colors.Green);
                    break;

                case 6:
                    GR_Client.Background = new SolidColorBrush(Colors.Green);
                    break;

                case 7:
                    GR_Provider.Background = new SolidColorBrush(Colors.Green);
                    break;
            }
        }

        private void EV_MouseChange(object sender, RoutedEventArgs e)
        {
            SetTransparentAll();
            if (GetController().GetProvider().ProviderID > 0)
            {
                if (GR_Provider.IsMouseOver)
                {
                    GR_Provider.Background = new SolidColorBrush(Colors.Red);
                }
            }

            if (GetController().GetClient().ClientID > 0)
            {
                if (GR_Client.IsMouseOver)
                {
                    GR_Client.Background = new SolidColorBrush(Colors.Red);
                }
            }

            if (GetController().GetStore().StoreID > 0)
            {
                if (GR_Store.IsMouseOver)
                {
                    GR_Store.Background = new SolidColorBrush(Colors.Red);
                }
            }

            if (GetController().GetPaymentMethod().PaymentMethodID > 0)
            {
                if (GR_PaymentMethod.IsMouseOver)
                {
                    GR_PaymentMethod.Background = new SolidColorBrush(Colors.Red);
                }
            }

            SetSelected();
        }

        private void EV_MouseClick(object sender, RoutedEventArgs e)
        {

            if (GetController().GetProvider().ProviderID > 0)
            {
                if (GR_Provider.IsMouseOver)
                {
                    GetController().EV_UpdateSubMenu(7);
                }
            }

            if (GetController().GetClient().ClientID > 0)
            {
                if (GR_Client.IsMouseOver)
                {
                    GetController().EV_UpdateSubMenu(6);
                }
            }

            if (GetController().GetStore().StoreID > 0)
            {
                if (GR_Store.IsMouseOver)
                {
                    GetController().EV_UpdateSubMenu(4);
                }
            }

            if (GetController().GetPaymentMethod().PaymentMethodID > 0)
            {
                if (GR_PaymentMethod.IsMouseOver)
                {
                    GetController().EV_UpdateSubMenu(5);
                }
            }

            SetTransparentAll();
            SetSelected();
        }

        private void EV_Cancel(object sender, KeyEventArgs e)
        {
                e.Handled = true;
                //MessageBox.Show("SI");
        }

        private void EV_StockAdjustCode(object sender, RoutedEventArgs e)
        {
        }

        protected void EV_StoreSelect(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_Stores.SelectedItem;

            if (temp1 != null)
            {
                GetController().SetStore(Convert.ToInt32(temp1.Name.Replace("store", "")));
            }
        }

        protected void EV_PaymentMethodSelect(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp2 = (ComboBoxItem)CB_PaymentMethod.SelectedItem;

            if (temp2 != null)
            {
                GetController().SetPaymentMethod(Convert.ToInt32(temp2.Name.Replace("paymentMethod", "")));
            }
        }

        public void EV_DateChange(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;

            DateTime? date = picker.SelectedDate;
            this.Title = date.Value.ToShortDateString();
            GetController().SetDate(date.Value);
        }

        virtual public Controller.CT_DCM_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_DCM_Item_Load)a.MainFrame.Content;
        }
    }
}
