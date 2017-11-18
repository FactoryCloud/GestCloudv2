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
    public partial class ProductSelectWindow : Window
    {

        public Movement movement;
        public ProductsView productsView;
        string type { get; set; }

        public ProductSelectWindow()
        {
        }

        public ProductSelectWindow(int option, List<Movement> movements)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            CB_ProductType.SelectionChanged += new SelectionChangedEventHandler(EV_Search);
            CB_Expansion.SelectionChanged += new SelectionChangedEventHandler(EV_Search);
            CB_Condition.SelectionChanged += new SelectionChangedEventHandler(EV_ConditionSelect);
            TB_ProductName.KeyUp += new KeyEventHandler(EV_Search);
            TB_Quantity.KeyUp += new KeyEventHandler(EV_QuantityChange);
            DG_Products.MouseLeftButtonUp += new MouseButtonEventHandler(EV_ProductsSelect);

            productsView = new ProductsView(option);
            movement = new Movement();
            UpdateData();
        }

        protected void EV_Start(object sender, RoutedEventArgs e)
        {
            List<Expansion> expansions = productsView.GetExpansions();
            List<ProductType> productTypes = productsView.GetProductTypes();
            List<FrameworkDB.V1.Condition> conditions = productsView.GetConditions();

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

            foreach (FrameworkDB.V1.Condition ct in conditions)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{ct.Name}";
                temp.Name = $"condition{ct.ConditionID}";
                CB_Condition.Items.Add(temp);
            }

            foreach (ComboBoxItem cmbItem in CB_ProductType.Items)
            {
                if (Convert.ToInt32(cmbItem.Name.Replace("productType", "")) == 1)
                {
                    CB_ProductType.SelectedValue = cmbItem;
                }
            }
            CB_Condition.SelectedIndex = 0;
        }

        public void EV_ConditionSelect(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_Condition.SelectedItem;

            if (CB_Condition.SelectedIndex >= 0)
            {
                movement.condition = productsView.GetCondition(Convert.ToInt32(temp1.Name.Replace("condition", "")));
            }
        }

        public void EV_ProductsSelect(object sender, RoutedEventArgs e)
        {
            int product = DG_Products.SelectedIndex;

            if (product >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Products.ItemContainerGenerator.ContainerFromIndex(product);
                DataRowView dr = row.Item as DataRowView;
                movement.product = productsView.GetProduct(Int32.Parse(dr.Row.ItemArray[0].ToString()));
                TB_ProductName.Text = movement.product.Name; 
                //MessageBox.Show(dr.Row.ItemArray[0].ToString());
            }
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

            if (decimal.TryParse(TB_Quantity.Text, out decimal d))
            {
                movement.Quantity = decimal.Parse(TB_Quantity.Text);
            }
        }

        protected void EV_SaveMovement(object sender, RoutedEventArgs e)
        {
            movement.Quantity = Decimal.Parse(TB_Quantity.Text);
            GetController().EV_MovementAdd(movement);
            this.Close();
        }

        protected void EV_Search(object sender, RoutedEventArgs e)
        {
            productsView.ProductName = TB_ProductName.Text;

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

            if(TB_ProductName.Text.Length >= 4 || CB_Expansion.SelectedIndex >= 0)
                UpdateData();
        }

        public void UpdateData()
        {
            DG_Products.ItemsSource = null;
            DG_Products.ItemsSource = productsView.GetTable();
        }

        virtual public Main.Controller.CT_Common GetController()
        {
            return new Main.Controller.CT_Common();
        }
    }
}