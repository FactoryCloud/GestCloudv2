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

namespace GestCloudv2.FloatWindows
{
    /// <summary>
    /// Interaction logic for ProductSelectWindow.xaml
    /// </summary>
    public partial class ProductSelectWindow : Window
    {
        ProductsView productsView;
        ProductView product;

        StockItem.AddStock.AddStock_Controller stockController;

        public ProductSelectWindow(StockItem.AddStock.AddStock_Controller controller)
        {
            InitializeComponent();

            stockController = controller;

            this.Loaded += new RoutedEventHandler(StartEvent);
            CB_ProductType.SelectionChanged += new SelectionChangedEventHandler(SearchEvent);
            CB_Expansion.SelectionChanged += new SelectionChangedEventHandler(SearchEvent);

            productsView = new ProductsView();
            UpdateData();
        }

        private void StartEvent(object sender, RoutedEventArgs e)
        {
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

            productsView.UpdateFilteredTable();
        }

        public void UpdateData()
        {
            DG_Products.ItemsSource = null;
            DG_Products.ItemsSource = productsView.GetTable();
        }
    }
}
