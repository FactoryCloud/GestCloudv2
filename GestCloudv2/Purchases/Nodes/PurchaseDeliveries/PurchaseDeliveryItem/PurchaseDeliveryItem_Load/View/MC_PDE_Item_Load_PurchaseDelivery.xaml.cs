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

namespace GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryItem.PurchaseDeliveryItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_STA_Item_New_StockAdjust.xaml
    /// </summary>
    public partial class MC_PDE_Item_Load_PurchaseDelivery : Page
    {

        public MC_PDE_Item_Load_PurchaseDelivery()
        {

            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);
            CB_Stores.SelectionChanged += new SelectionChangedEventHandler(EV_StoreSelect);
            DP_Date.KeyDown += new KeyEventHandler(EV_Cancel);
            DP_Date.KeyUp += new KeyEventHandler(EV_Cancel);

            GR_Provider.MouseEnter += new MouseEventHandler(EV_MouseChange);
            GR_Provider.MouseLeave += new MouseEventHandler(EV_MouseChange);
            GR_Provider.MouseLeftButtonUp += new MouseButtonEventHandler(EV_MouseClick);

            GR_Store.MouseEnter += new MouseEventHandler(EV_MouseChange);
            GR_Store.MouseLeave += new MouseEventHandler(EV_MouseChange);
            GR_Store.MouseLeftButtonUp += new MouseButtonEventHandler(EV_MouseClick);

            SetSelected();

        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_PurchaseDeliveryCode.Text = GetController().purchaseDelivery.Code.Trim();
            TB_ProviderCode.Text = GetController().purchaseDelivery.ProviderID.ToString();
            TB_ProviderName.Text = GetController().purchaseDelivery.provider.entity.Name;

            if (GetController().Information["editable"] == 0)
            {
                TB_PurchaseDeliveryCode.IsReadOnly = true;

                Thickness margin = new Thickness(20);

                TextBox TB_StoreCode = new TextBox();
                TB_StoreCode.Name = "TB_StoreCode";
                TB_StoreCode.Text = $"{GetController().store.Code}";
                TB_StoreCode.VerticalAlignment = VerticalAlignment.Center;
                TB_StoreCode.TextAlignment = TextAlignment.Center;
                TB_StoreCode.Margin = margin;
                Grid.SetColumn(TB_StoreCode, 2);
                Grid.SetRow(TB_StoreCode, 3);

                GR_Store.Children.Add(TB_StoreCode);

                CB_Stores.Visibility = Visibility.Hidden;

                TextBox TB_DateStockAdjust = new TextBox();
                TB_DateStockAdjust.Name = "TB_DateStockAdjust";
                TB_DateStockAdjust.Text = $"{String.Format("{0:dd/MM/yyyy}", GetController().purchaseDelivery.Date)}"; ;
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
                DP_Date.SelectedDate = Convert.ToDateTime(GetController().purchaseDelivery.Date);
                List<Store> stores = GetController().GetStores();
                //CB_Stores.SelectedIndex = Convert.ToInt16(GetController().store.Code);
                List<int> nums = new List<int>();

                Thickness margin = new Thickness(20);

                TextBox TB_StoreCode = new TextBox();
                TB_StoreCode.Name = "TB_StoreCode";
                TB_StoreCode.Text = $"{GetController().store.Code}";
                TB_StoreCode.VerticalAlignment = VerticalAlignment.Center;
                TB_StoreCode.TextAlignment = TextAlignment.Center;
                TB_StoreCode.Margin = margin;
                Grid.SetColumn(TB_StoreCode, 2);
                Grid.SetRow(TB_StoreCode, 3);

                GR_Store.Children.Add(TB_StoreCode);

                CB_Stores.Visibility = Visibility.Hidden;
            }
        }

        private void SetTransparentAll()
        {
            GR_Store.Background = new SolidColorBrush(Colors.Transparent);
            GR_Provider.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void SetSelected()
        {
            switch (GetController().Information["submode"])
            {
                case 4:
                    GR_Store.Background = new SolidColorBrush(Colors.Green);
                    break;

                case 7:
                    GR_Provider.Background = new SolidColorBrush(Colors.Green);
                    break;
            }
        }

        private void EV_MouseChange(object sender, RoutedEventArgs e)
        {
            SetTransparentAll();
            if (GetController().purchaseDelivery.provider.ProviderID > 0)
            {
                if (GR_Provider.IsMouseOver)
                {
                    GR_Provider.Background = new SolidColorBrush(Colors.Red);
                }
            }

            if (GetController().store.StoreID > 0)
            {
                if (GR_Store.IsMouseOver)
                {
                    GR_Store.Background = new SolidColorBrush(Colors.Red);
                }
            }
            SetSelected();
        }

        private void EV_MouseClick(object sender, RoutedEventArgs e)
        {

            if (GetController().purchaseDelivery.provider.ProviderID > 0)
            {
                if (GR_Provider.IsMouseOver)
                {
                    GetController().EV_UpdateSubMenu(7);
                }
            }

            if (GetController().store.StoreID > 0)
            {
                if (GR_Store.IsMouseOver)
                {
                    GetController().EV_UpdateSubMenu(4);
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

        public void EV_DateChange(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;

            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                this.Title = "Sin fecha";
                GetController().purchaseDelivery.Date = null;
            }
            else
            {
                this.Title = date.Value.ToShortDateString();
                GetController().SetDate(date.Value);
            }
        }

        private Controller.CT_PDE_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PDE_Item_Load)a.MainFrame.Content;
        }

        private void DP_Date_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
