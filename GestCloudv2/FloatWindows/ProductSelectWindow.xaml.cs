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

namespace GestCloudv2.FloatWindows
{
    /// <summary>
    /// Interaction logic for ProductSelectWindow.xaml
    /// </summary>
    public partial class ProductSelectWindow : Window
    {
        ProductsView productsView;
        Movement movement;
        GestCloudDB db;

        StockItem.AddStock.AddStock_Controller stockController;

        public ProductSelectWindow(StockItem.AddStock.AddStock_Controller controller)
        {
            InitializeComponent();

            stockController = controller;
            movement = new Movement();

            this.Loaded += new RoutedEventHandler(StartEvent);
            CB_ProductType.SelectionChanged += new SelectionChangedEventHandler(SearchEvent);
            CB_Expansion.SelectionChanged += new SelectionChangedEventHandler(SearchEvent);
            TB_ProductName.KeyUp += new KeyEventHandler(SearchEvent);
            TX_Quantity.KeyUp += new KeyEventHandler(EV_QuantityChange);
            DG_Products.MouseLeftButtonUp += new MouseButtonEventHandler(ProductSelected_Event);

            productsView = new ProductsView();
            //UpdateData();
        }

        public ProductSelectWindow(StockItem.AddStock.AddStock_Controller controller, Movement mov, string type)
        {
            InitializeComponent();

            stockController = controller;
            movement = mov;

            this.Loaded += new RoutedEventHandler(StartEvent_Edit);
            CB_ProductType.SelectionChanged += new SelectionChangedEventHandler(SearchEvent);
            CB_Expansion.SelectionChanged += new SelectionChangedEventHandler(SearchEvent);
            TB_ProductName.KeyUp += new KeyEventHandler(SearchEvent);
            TX_Quantity.KeyUp += new KeyEventHandler(EV_QuantityChange);
            DG_Products.MouseLeftButtonUp += new MouseButtonEventHandler(ProductSelected_Event);

            productsView = new ProductsView();
            //UpdateData();
        }

        private void StartEvent(object sender, RoutedEventArgs e)
        {
            db = new GestCloudDB();
            movement.documentType = db.DocumentTypes.First(d => d.Name == "Stock" && d.Input == 1);
            movement.DocumentTypeID = movement.documentType.DocumentTypeID;

            List<Expansion> expansions = productsView.GetExpansions();
            List<ProductType> productTypes = productsView.GetProductTypes();

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

            CB_ProductType.SelectedIndex = 0;
            UpdateData();
        }

        private void StartEvent_Edit(object sender, RoutedEventArgs e)
        {
            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                dpd.AddValueChanged(DG_Products, EV_PreSelectedProduct);
            }

            db = new GestCloudDB();

            MTGCard card = db.MTGCards.First(c => movement.product.ExternalID == c.ProductID);

            List<Expansion> expansions = productsView.GetExpansions();
            List<ProductType> productTypes = productsView.GetProductTypes();

            int count = 0;
            foreach (Expansion exp in expansions)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{exp.EnName} ({exp.Abbreviation})";
                temp.Name = $"expansion{exp.Id}";
                CB_Expansion.Items.Add(temp);
                if(card.ExpansionID == exp.Id)
                {
                    MessageBox.Show($"card {card.EnName}, edition {exp.EnName}, num {count}");
                    CB_Expansion.SelectedIndex = count;
                }
                count++;
            }

            foreach (ProductType pt in productTypes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{pt.Name}";
                temp.Name = $"productType{pt.ProductTypeID}";
                CB_ProductType.Items.Add(temp);
            }

            CB_ProductType.SelectedIndex = 0;
            UpdateData();
        }

        
        private void EV_PreSelectedProduct(object sender, EventArgs e)
        {
            //MessageBox.Show($"{DG_Products.Items.Count}");
            for (int i = 0; i < DG_Products.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)DG_Products.ItemContainerGenerator.ContainerFromIndex(i);
                DataRowView dr = row.Item as DataRowView;
                //MessageBox.Show($"posicion {i}, valor {Int32.Parse(dr.Row.ItemArray[0].ToString())}");
                if (Int32.Parse(dr.Row.ItemArray[0].ToString()) == movement.ProductID)
                {
                    //MessageBox.Show($"posicion {i}, valor {Int32.Parse(dr.Row.ItemArray[0].ToString())}");
                    DG_Products.SelectedIndex = i;
                    return;
                }
            }
        }

        private void ProductSelected_Event(object sender, RoutedEventArgs e)
        {
            int product = DG_Products.SelectedIndex;
            if (product >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Products.ItemContainerGenerator.ContainerFromIndex(product);
                DataRowView dr = row.Item as DataRowView;
                movement.product = db.Products.First(p => p.ProductID == Int32.Parse(dr.Row.ItemArray[0].ToString()));
                movement.ProductID = Int32.Parse(dr.Row.ItemArray[0].ToString());
            }
        }

        private void EV_QuantityChange(object sender, RoutedEventArgs e)
        {
            movement.Quantity = float.Parse(TX_Quantity.Text);
        }

        private void EV_SaveMovement(object sender, RoutedEventArgs e)
        {
            GetController().AddNewMovement(movement);
            this.Close();
        }

        private void SearchEvent(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_ProductType.SelectedItem;
            ComboBoxItem temp2 = (ComboBoxItem)CB_Expansion.SelectedItem;

            if (CB_ProductType.SelectedIndex >= 0)
            {
                productsView.SetProductType(Convert.ToInt32(temp1.Name.Replace("productType", "")));
            }

            if (CB_Expansion.SelectedIndex >= 0)
            {
                productsView.SetExpansion(Convert.ToInt32(temp2.Name.Replace("expansion", "")));
            }

            if(TB_ProductName.Text.Length >= 3)
            {
                productsView.ProductName = TB_ProductName.Text;
            }

            else
            {
                productsView.ProductName = null;
            }

            productsView.UpdateFilteredTable();
        }

        public void UpdateData()
        {
            DG_Products.ItemsSource = null;
            DG_Products.ItemsSource = productsView.GetTable();
        }

        private StockItem.AddStock.AddStock_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (StockItem.AddStock.AddStock_Controller)a.MainPage.Content;
        }
    }
}
