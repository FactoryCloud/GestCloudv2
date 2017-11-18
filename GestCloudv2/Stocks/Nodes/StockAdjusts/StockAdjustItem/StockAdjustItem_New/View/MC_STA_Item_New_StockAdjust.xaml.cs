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

namespace GestCloudv2.Stocks.Nodes.StockAdjusts.StockAdjustItem.StockAdjustItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_STA_Item_New_StockAdjust.xaml
    /// </summary>
    public partial class MC_STA_Item_New_StockAdjust : Page
    {
        public MC_STA_Item_New_StockAdjust()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            TB_StockAdjustReference.KeyUp += new KeyEventHandler(EV_StockAdjustCode);
            TB_StockAdjustReference.Loaded += new RoutedEventHandler(EV_StockAdjustCode);
            TB_StockAdjustCode.KeyUp += new KeyEventHandler(EV_StockAdjustCode);
            TB_StockAdjustCode.Loaded += new RoutedEventHandler(EV_StockAdjustCode);

        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_StockAdjustCode.Text = GetController().LastStockAdjustCod().ToString();
            List<Store> stores = GetController().GetStores();
            foreach (Store st in stores)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{st.Code} - {st.Name}";
                temp.Name = $"store{st.StoreID}";
                CB_Stores.Items.Add(temp);
            }
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

        private Controller.CT_STA_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_STA_Item_New)a.MainFrame.Content;
        }
    }
}
