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

namespace GestCloudv2.Sales.Nodes.SaleOrders.SaleOrderItem.SaleOrderItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_POR_Item_New_PurchaseOrder.xaml
    /// </summary>
    public partial class MC_SOR_Item_New_SaleOrder : Page
    {

        public MC_SOR_Item_New_SaleOrder()
        {

            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);
            TB_StockAdjustReference.KeyUp += new KeyEventHandler(EV_StockAdjustCode);
            TB_StockAdjustReference.Loaded += new RoutedEventHandler(EV_StockAdjustCode);
            TB_StockAdjustCode.KeyUp += new KeyEventHandler(EV_StockCode);
            TB_StockAdjustCode.Loaded += new RoutedEventHandler(EV_StockCode);
            CB_Stores.SelectionChanged += new SelectionChangedEventHandler(EV_StoreSelect);
            DP_Date.KeyDown += new KeyEventHandler(EV_Cancel);
            DP_Date.KeyUp += new KeyEventHandler(EV_Cancel);

        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            DP_Date.SelectedDate = DateTime.Now;
            //TB_StockAdjustCode.Text = GetController().LastClientCod().ToString();
            List<Store> stores = GetController().GetStores();
            foreach (Store st in stores)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{st.Code} - {st.Name}";
                temp.Name = $"store{st.StoreID}";
                CB_Stores.Items.Add(temp);
            }
            CB_Stores.SelectedIndex = 0;
        }

        private void EV_StockCode(object sender, RoutedEventArgs e)
        {
            if (TB_StockAdjustCode.Text.Length == 0)
            {
                if (SP_StockAdjustCode.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_StockAdjustCode.Children.Add(message);
                }

                else if (SP_StockAdjustCode.Children.Count == 2)
                {
                    SP_StockAdjustCode.Children.RemoveAt(SP_StockAdjustCode.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_StockAdjustCode.Children.Add(message);
                }
                GetController().CleanStockCode();
                TB_StockAdjustCode.Text = "";
            }
            else if (TB_StockAdjustCode.Text.Any(x => Char.IsWhiteSpace(x)))
            {
                if (SP_StockAdjustCode.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede contener espacios";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_StockAdjustCode.Children.Add(message);
                }

                else if (SP_StockAdjustCode.Children.Count == 2)
                {
                    SP_StockAdjustCode.Children.RemoveAt(SP_StockAdjustCode.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede contener espacios";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_StockAdjustCode.Children.Add(message);
                }
                GetController().CleanStockCode();
            }

             else if (GetController().StockAdjustExist(TB_StockAdjustCode.Text))
             {
                 if (SP_StockAdjustCode.Children.Count == 1)
                 {
                     TextBlock message = new TextBlock();
                     message.TextWrapping = TextWrapping.WrapWithOverflow;
                     message.Text = "Este código ya existe";
                     message.HorizontalAlignment = HorizontalAlignment.Center;
                     SP_StockAdjustCode.Children.Add(message);
                 }

                 else if (SP_StockAdjustCode.Children.Count == 2)
                 {
                     SP_StockAdjustCode.Children.RemoveAt(SP_StockAdjustCode.Children.Count - 1);
                     TextBlock message = new TextBlock();
                     message.TextWrapping = TextWrapping.WrapWithOverflow;
                     message.Text = "Este código ya existe";
                     message.HorizontalAlignment = HorizontalAlignment.Center;
                     SP_StockAdjustCode.Children.Add(message);
                 }
                 GetController().EV_UpdateIfNotEmpty(true);
             }

             else
             {
                 if (SP_StockAdjustCode.Children.Count == 2)
                 {
                     SP_StockAdjustCode.Children.RemoveAt(SP_StockAdjustCode.Children.Count - 1);
                 }
                 GetController().EV_UpdateIfNotEmpty(true);
             }
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

            if (CB_Stores.SelectedIndex >= 0)
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
                GetController().stockAdjust.Date = null;
            }
            else
            {
                this.Title = date.Value.ToShortDateString();
                GetController().SetAdjustDate(date.Value);
            }
        }

        private void DP_Date_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void EV_ClientSelect(object sender, RoutedEventArgs e)
        {
            GetController().MD_ClientSelect();
        }

        private Controller.CT_SOR_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_SOR_Item_New)a.MainFrame.Content;
        }
    }
}
