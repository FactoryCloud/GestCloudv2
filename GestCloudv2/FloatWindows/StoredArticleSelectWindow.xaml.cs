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
using System.Windows.Shapes;
using FrameworkDB.V1;
using FrameworkView.V1;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;

namespace GestCloudv2.FloatWindows
{
    /// <summary>
    /// Interaction logic for ProductSelectWindow.xaml
    /// </summary>
    public partial class StoredArticleSelectWindow : Window
    {
        public StoredStocksView storedStocksView;
        public Movement movement;
        string type { get; set; }

        public StoredArticleSelectWindow()
        {

        }

        public StoredArticleSelectWindow(int option, List<Movement> movements)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            //this.Closed += new EventHandler(EV_Close);
            CB_ProductType.SelectionChanged += new SelectionChangedEventHandler(EV_Search);
            CB_Expansion.SelectionChanged += new SelectionChangedEventHandler(EV_Search);
            CB_Stores.SelectionChanged += new SelectionChangedEventHandler(EV_Search);
            TB_ProductName.KeyUp += new KeyEventHandler(EV_Search);
            TB_Quantity.KeyUp += new KeyEventHandler(EV_QuantityChange);
            DG_StoredStocks.MouseLeftButtonUp += new MouseButtonEventHandler(EV_StoredStockSelect);

            storedStocksView = new StoredStocksView(option, movements);
            movement = new Movement();
            UpdateData();
        }

        protected void EV_Start(object sender, RoutedEventArgs e)
        {
            List<Expansion> expansions = storedStocksView.GetExpansions();
            List<ProductType> productTypes = storedStocksView.GetProductTypes();
            List<Store> stores = storedStocksView.GetStores();

            foreach (Expansion exp in expansions)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{exp.EnName} ({exp.Abbreviation})";
                temp.Name = $"expansion{exp.Id}";
                CB_Expansion.Items.Add(temp);
            }

            foreach (ProductType pt in productTypes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{pt.Name}";
                temp.Name = $"productType{pt.ProductTypeID}";
                CB_ProductType.Items.Add(temp);
            }

            foreach (Store st in stores)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{st.Code} - {st.Name}";
                temp.Name = $"store{st.StoreID}";
                CB_Stores.Items.Add(temp);
            }
        }

        /*private void ItemContainerGeneratorOnStatusChanged(object sender, EventArgs eventArgs)
        {
            
            var dataGrid = DG_Products;

            if (dataGrid.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                
                for (int i = 0; i < DG_Products.Items.Count; i++)
                {
                    DataGridRow row = (DataGridRow)DG_Products.ItemContainerGenerator.ContainerFromIndex(i);
                    if(row != null)
                    {
                        DataRowView dr = row.Item as DataRowView;
                        if (Int32.Parse(dr.Row.ItemArray[0].ToString()) == movement.ProductID)
                        {
                            DG_Products.SelectedIndex = i;
                            return;
                        }
                    }
                }
            }
        }*/

        public void EV_StoredStockSelect(object sender, RoutedEventArgs e)
        {
            int product = DG_StoredStocks.SelectedIndex;
            if (product >= 0)
            {
                DataGridRow row = (DataGridRow)DG_StoredStocks.ItemContainerGenerator.ContainerFromIndex(product);
                DataRowView dr = row.Item as DataRowView;
                movement = storedStocksView.GetMovement(Int32.Parse(dr.Row.ItemArray[0].ToString()));
                movement.Quantity = Int32.Parse(dr.Row.ItemArray[7].ToString());
                TB_Quantity.Text = Int32.Parse(dr.Row.ItemArray[7].ToString()).ToString();
            }
            TB_Quantity.IsEnabled = true;
        }

        protected void EV_QuantityChange(object sender, RoutedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(TB_Quantity.Text, "[^0-9]"))
            {
                if (SP_Quantity.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Solo se permiten números";
                    SP_Quantity.Children.Add(message);
                }
                TB_Quantity.Text = TB_Quantity.Text.Remove(TB_Quantity.Text.Length - 1);
            }

            else
            {
                if (SP_Quantity.Children.Count == 2)
                {
                    SP_Quantity.Children.RemoveAt(SP_Quantity.Children.Count - 1);
                }
            }

            /*if (decimal.TryParse(TB_Quantity.Text, out decimal d))
            {
                movement.Quantity = decimal.Parse(TB_Quantity.Text);
            }*/

            if (TB_Quantity.Text.Length > 0)
            {
                if ((int)movement.Quantity != Int32.Parse(TB_Quantity.Text))
                    BT_SaveMovement.IsEnabled = true;

                else
                    BT_SaveMovement.IsEnabled = false;
            }
        }

        protected void EV_SaveMovement(object sender, RoutedEventArgs e)
        {
            /*GetController().AddNewMovement(movement);
            if( type == null )
            {
                this.Closed += new EventHandler(EV_Close);
            }

            else if (!type.Contains("AddStock_EditMovement"))
            {
                this.Closed += new EventHandler(EV_Close);
            }
            this.Close();*/
            movement.Quantity = (decimal)(Decimal.Parse(TB_Quantity.Text) - movement.Quantity);
            movement = storedStocksView.UpdateMovement(movement);
            GetController().EV_MovementAdd(movement);
            this.Close();
        }

        protected void EV_Search(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_ProductType.SelectedItem;
            ComboBoxItem temp2 = (ComboBoxItem)CB_Expansion.SelectedItem;
            ComboBoxItem temp3 = (ComboBoxItem)CB_Stores.SelectedItem;

            if (CB_ProductType.SelectedIndex >= 0)
            {
                storedStocksView.SetProductType(Convert.ToInt32(temp1.Name.Replace("productType", "")));
            }

            if (CB_Expansion.SelectedIndex >= 0)
            {
                storedStocksView.SetExpansion(Convert.ToInt32(temp2.Name.Replace("expansion", "")));
            }

            if (CB_Stores.SelectedIndex >= 0)
            {
                storedStocksView.SetStore(Convert.ToInt32(temp3.Name.Replace("store", "")));
            }

            if (TB_ProductName.Text.Length >= 3)
            {
                storedStocksView.ProductName = TB_ProductName.Text;
            }

            else
            {
                storedStocksView.ProductName = null;
            }

            storedStocksView.UpdateFilteredTable();
        }

        public void UpdateData()
        {
            DG_StoredStocks.ItemsSource = null;
            DG_StoredStocks.ItemsSource = storedStocksView.GetTable();
        }

        virtual public Main.Controller.CT_Common GetController()
        {
            return new Main.Controller.CT_Common();
        }
    }
}
