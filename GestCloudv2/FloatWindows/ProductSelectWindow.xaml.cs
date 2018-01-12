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
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace GestCloudv2.FloatWindows
{
    /// <summary>
    /// Interaction logic for ProductSelectWindow.xaml
    /// </summary>
    public partial class ProductSelectWindow : Window
    {
        public ProductsView productsView;
        public Movement movement;
        int OperationOption;

        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.LastIndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        public ProductSelectWindow()
        { 
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            CH_IsAltered.Unchecked += new RoutedEventHandler(EV_CheckChange);
            CH_IsFoil.Unchecked += new RoutedEventHandler(EV_CheckChange);
            CH_IsPlayset.Unchecked += new RoutedEventHandler(EV_CheckChange);
            CH_IsSigned.Unchecked += new RoutedEventHandler(EV_CheckChange);
            CH_IsAltered.Checked += new RoutedEventHandler(EV_CheckChange);
            CH_IsFoil.Checked += new RoutedEventHandler(EV_CheckChange);
            CH_IsPlayset.Checked += new RoutedEventHandler(EV_CheckChange);
            CH_IsSigned.Checked += new RoutedEventHandler(EV_CheckChange);
            CB_ProductType.SelectionChanged += new SelectionChangedEventHandler(EV_Search);
            CB_Expansion.SelectionChanged += new SelectionChangedEventHandler(EV_Search);
            CB_Condition.SelectionChanged += new SelectionChangedEventHandler(EV_SetCondition);
            CB_Store.SelectionChanged += new SelectionChangedEventHandler(EV_SetStore);
            TB_ProductName.KeyUp += new KeyEventHandler(EV_Search);
            TB_Quantity.KeyUp += new KeyEventHandler(EV_NumberChange);
            TB_PurchasePrice.KeyUp += new KeyEventHandler(EV_NumberChange);
            TB_SalePrice.KeyUp += new KeyEventHandler(EV_NumberChange);
            TB_PurchaseDiscount.KeyUp += new KeyEventHandler(EV_NumberChange);
            TB_SaleDiscount.KeyUp += new KeyEventHandler(EV_NumberChange);

            TB_Quantity.GotMouseCapture += new MouseEventHandler(EV_NumberEnter);
            TB_PurchasePrice.GotMouseCapture += new MouseEventHandler(EV_NumberEnter);
            TB_SalePrice.GotMouseCapture += new MouseEventHandler(EV_NumberEnter);
            TB_PurchaseDiscount.GotMouseCapture += new MouseEventHandler(EV_NumberEnter);
            TB_SaleDiscount.GotMouseCapture += new MouseEventHandler(EV_NumberEnter);

            TB_Quantity.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(EV_NumberEnter);
            TB_PurchasePrice.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(EV_NumberEnter);
            TB_SalePrice.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(EV_NumberEnter);
            TB_PurchaseDiscount.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(EV_NumberEnter);
            TB_SaleDiscount.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(EV_NumberEnter);

            TB_Quantity.LostFocus += new RoutedEventHandler(EV_NumberLeft);
            TB_PurchasePrice.LostFocus += new RoutedEventHandler(EV_NumberLeft);
            TB_SalePrice.LostFocus += new RoutedEventHandler(EV_NumberLeft);
            TB_PurchaseDiscount.LostFocus += new RoutedEventHandler(EV_NumberLeft);
            TB_SaleDiscount.LostFocus += new RoutedEventHandler(EV_NumberLeft);

            DG_Products.MouseLeftButtonUp += new MouseButtonEventHandler(EV_ProductsSelect);

            productsView = new ProductsView();

            movement = new Movement();
            OperationOption = 0;
            
        }

        public ProductSelectWindow(Movement mov):this()
        {
            movement = mov;
        }

        public ProductSelectWindow(int OperationOption, List<Movement> Movements) : this()
        {
            productsView = new ProductsView(OperationOption, Movements);
            this.OperationOption = OperationOption;
        }

        protected void EV_Start(object sender, RoutedEventArgs e)
        {
            List<ProductType> productTypes = productsView.GetProductTypes();

            foreach (ProductType pt in productTypes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{pt.Name}";
                temp.Name = $"productType{pt.ProductTypeID}";
                CB_ProductType.Items.Add(temp);
            }

            List<Store> stores = GetController().GetStores();

            foreach (Store st in stores)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{st.Code} - {st.Name}";
                temp.Name = $"store{st.StoreID}";
                CB_Store.Items.Add(temp);
            }

            foreach (ComboBoxItem item in CB_Store.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("store", "")) == GetController().GetStore().StoreID)
                {
                    CB_Store.SelectedValue = item;
                    break;
                }
            }

            if (movement.product != null)
            {
                foreach (ComboBoxItem item in CB_ProductType.Items)
                {
                    if (Convert.ToInt16(item.Name.Replace("productType", "")) == movement.product.ProductTypeID)
                    {
                        CB_ProductType.SelectedValue = item;
                        break;
                    }
                }

                foreach (ComboBoxItem item in CB_Store.Items)
                {
                    if (Convert.ToInt16(item.Name.Replace("store", "")) == movement.StoreID)
                    {
                        CB_Store.SelectedValue = item;
                        break;
                    }
                }

                TB_ProductName.Text = movement.product.Name;
                TB_Quantity.Text = Convert.ToDecimal(movement.Quantity).ToString("0.##");

                productsView.ProductName = movement.product.Name;

                TB_Quantity.IsEnabled = true;

                if (OperationOption == 0)
                {
                    TB_PurchasePrice.Text = Convert.ToDecimal(movement.PurchasePrice).ToString("0.00");
                    TB_PurchaseDiscount.Text = Convert.ToDecimal(movement.PurchaseDiscount1).ToString("0.00");
                    TB_PurchasePrice.IsEnabled = true;
                    TB_PurchaseDiscount.IsEnabled = true;
                }

                TB_SalePrice.Text = Convert.ToDecimal(movement.SalePrice).ToString("0.00");
                TB_SaleDiscount.Text = Convert.ToDecimal(movement.SaleDiscount1).ToString("0.00");

                TB_SalePrice.IsEnabled = true;
                TB_SaleDiscount.IsEnabled = true;

                EV_ActivateButton();
            }

            UpdateData();

            if(movement.product != null)
                DG_Products.SelectedIndex = 0;
        }

        public void EV_CheckChange(object sender, RoutedEventArgs e)
        {
            movement.IsAltered = Convert.ToInt16(CH_IsAltered.IsChecked);
            movement.IsSigned = Convert.ToInt16(CH_IsSigned.IsChecked);
            movement.IsPlayset = Convert.ToInt16(CH_IsPlayset.IsChecked);
            movement.IsFoil = Convert.ToInt16(CH_IsFoil.IsChecked);
        }

        public void EV_SetStore(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp = (ComboBoxItem)CB_Store.SelectedItem;

            if (OperationOption != 0)
                EV_CleanSelection();

            if (temp != null)
                movement.StoreID = Convert.ToInt32(temp.Name.Replace("store", ""));

            UpdateData();
        }

        public void EV_SetCondition(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp = (ComboBoxItem)CB_Condition.SelectedItem;

            if (temp != null)
            {
                movement.condition = productsView.GetCondition(Convert.ToInt32(temp.Name.Replace("condition", "")));
                movement.ConditionID = Convert.ToInt32(temp.Name.Replace("condition", ""));
            }
        }

        public void EV_ProductsSelect(object sender, RoutedEventArgs e)
        {
            int product = DG_Products.SelectedIndex;

            if (product >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Products.ItemContainerGenerator.ContainerFromIndex(product);
                DataRowView dr = row.Item as DataRowView;

                if (movement.ProductID != Convert.ToInt32(dr.Row.ItemArray[0].ToString()))
                {
                    movement.product = productsView.GetProduct(Convert.ToInt32(dr.Row.ItemArray[0].ToString()));
                    movement.ProductID = Convert.ToInt32(dr.Row.ItemArray[0].ToString());
                    TB_ProductName.Text = movement.product.Name;

                    if (OperationOption == 0)
                    {
                        movement.Quantity = Convert.ToDecimal(1);
                        movement.SalePrice = Convert.ToDecimal(movement.product.SalePrice1);

                        TB_Quantity.Text = ((decimal)movement.Quantity).ToString("0.##");
                        TB_SalePrice.Text = ((decimal)movement.SalePrice).ToString("0.00");

                        TB_Quantity.IsEnabled = true;
                        TB_PurchasePrice.IsEnabled = true;
                        TB_PurchaseDiscount.IsEnabled = true;
                    }

                    else
                    {
                        if (productsView.stocks[movement.StoreID][Convert.ToInt32(movement.ProductID)] > 0)
                        {
                            movement.Quantity = Convert.ToDecimal(1);
                            TB_Quantity.Text = ((decimal)movement.Quantity).ToString("0.##");
                            TB_Quantity.IsEnabled = true;
                        }

                        movement.SalePrice = productsView.prices[Convert.ToInt32(movement.ProductID)] / productsView.times[Convert.ToInt32(movement.ProductID)];
                        TB_SalePrice.Text = (productsView.prices[Convert.ToInt32(movement.ProductID)] / productsView.times[Convert.ToInt32(movement.ProductID)]).ToString("0.00");
                    }

                    movement.PurchasePrice = Convert.ToDecimal(movement.product.PurchasePrice1);
                    movement.PurchaseDiscount1 = Convert.ToDecimal(movement.product.PurchaseDiscount1);
                    movement.SaleDiscount1 = Convert.ToDecimal(movement.product.SaleDiscount1);

                    TB_PurchasePrice.Text = ((decimal)movement.PurchasePrice).ToString("0.00");
                    TB_PurchaseDiscount.Text = ((decimal)movement.PurchaseDiscount1).ToString("0.00");
                    TB_SaleDiscount.Text = ((decimal)movement.SaleDiscount1).ToString("0.00");

                    TB_SalePrice.IsEnabled = true;
                    TB_SaleDiscount.IsEnabled = true;

                    movement.condition = productsView.GetConditionDefault();

                    if (OperationOption != 0)
                    {
                        if (productsView.stocks[movement.StoreID][Convert.ToInt32(movement.ProductID)] <= 0)
                        {
                            movement = new Movement();

                            ComboBoxItem temp = (ComboBoxItem)CB_Store.SelectedItem;
                            movement.StoreID = Convert.ToInt32(temp.Name.Replace("store", ""));

                            TB_Quantity.Text = (0).ToString("0.##");
                            TB_PurchasePrice.Text = (0).ToString("0.00");
                            TB_PurchaseDiscount.Text = (0).ToString("0.00");
                            TB_SalePrice.Text = (0).ToString("0.00");
                            TB_SaleDiscount.Text = (0).ToString("0.00");

                            TB_Quantity.IsEnabled = false;
                            TB_PurchasePrice.IsEnabled = false;
                            TB_PurchaseDiscount.IsEnabled = false;
                            TB_SalePrice.IsEnabled = false;
                            TB_SaleDiscount.IsEnabled = false;

                            BT_SaveMovement.IsEnabled = false;
                        }
                    }
                }
            }

            EV_ActivateButton();
        }

        protected void EV_NumberEnter(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        protected void EV_NumberLeft(object sender, RoutedEventArgs e)
        {
            if((sender as TextBox).Text.Length == 0)
            {
                switch (Convert.ToInt16((sender as TextBox).Tag))
                {
                    case 1:
                        (sender as TextBox).Text = Convert.ToDecimal(0).ToString("0.##");
                        break;

                    case 2:
                        (sender as TextBox).Text = Convert.ToDecimal(0).ToString("0.00");
                        break;

                    case 3:
                        (sender as TextBox).Text = Convert.ToDecimal(0).ToString("0.00");
                        break;

                    case 4:
                        (sender as TextBox).Text = Convert.ToDecimal(0).ToString("0.00");
                        break;

                    case 5:
                        (sender as TextBox).Text = Convert.ToDecimal(0).ToString("0.00");
                        break;
                }
            }

            switch(Convert.ToInt16((sender as TextBox).Tag))
            {
                case 1:
                    (sender as TextBox).Text = Convert.ToDecimal((sender as TextBox).Text).ToString("0.##");
                    break;

                case 2:
                    (sender as TextBox).Text = Convert.ToDecimal((sender as TextBox).Text).ToString("0.00");
                    break;

                case 3:
                    (sender as TextBox).Text = Convert.ToDecimal((sender as TextBox).Text).ToString("0.00");
                    break;

                case 4:
                    (sender as TextBox).Text = Convert.ToDecimal((sender as TextBox).Text).ToString("0.00");
                    break;

                case 5:
                    (sender as TextBox).Text = Convert.ToDecimal((sender as TextBox).Text).ToString("0.00");
                    break;
            }
        }

        protected void EV_NumberChange(object sender, RoutedEventArgs e)
        {
            if (Regex.Matches(((TextBox)sender).Text, "[^0-9,]").Count > 0)
            {
                ((TextBox)sender).Text = Regex.Replace(((TextBox)sender).Text, "[.]", ",");
                ((TextBox)sender).Text = Regex.Replace(((TextBox)sender).Text, "[^0-9,]", "");
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }

            if (Regex.Matches(((TextBox)sender).Text, "[.,]").Count > 1)
            {
                ((TextBox)sender).Text = ReplaceLastOccurrence(((TextBox)sender).Text, ",", "");
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }

            switch (Convert.ToInt16(((TextBox)sender).Tag))
            {
                case 1:
                    if (TB_Quantity.Text.Length > 0)
                    {
                        if (OperationOption != 0)
                        {
                            if (productsView.documentLines[movement.StoreID].ContainsKey(Convert.ToInt32(movement.ProductID)))
                            {
                                if (Convert.ToDecimal((sender as TextBox).Text) > Convert.ToInt32(productsView.stocks[movement.StoreID][Convert.ToInt32(movement.ProductID)] - productsView.documentLines[movement.StoreID][Convert.ToInt32(movement.ProductID)]))
                                    (sender as TextBox).Text = Convert.ToDecimal(Convert.ToInt32(productsView.stocks[movement.StoreID][Convert.ToInt32(movement.ProductID)] - productsView.documentLines[movement.StoreID][Convert.ToInt32(movement.ProductID)])).ToString("0.##");
                            }

                            else
                                if (Convert.ToDecimal((sender as TextBox).Text) > Convert.ToInt32(productsView.stocks[movement.StoreID][Convert.ToInt32(movement.ProductID)]))
                                (sender as TextBox).Text = Convert.ToDecimal(Convert.ToInt32(productsView.stocks[movement.StoreID][Convert.ToInt32(movement.ProductID)])).ToString("0.##");
                        }

                        movement.Quantity = Convert.ToDecimal(TB_Quantity.Text);
                    }
                        

                    else
                    {
                        TB_Quantity.Text = Convert.ToDecimal(0).ToString("#.##");
                        movement.Quantity = Convert.ToDecimal(0);
                    }
                    break;

                case 2:
                    if(TB_PurchasePrice.Text.Length > 0)
                        movement.PurchasePrice = Convert.ToDecimal(TB_PurchasePrice.Text);

                    else
                    {
                        TB_PurchasePrice.Text = Convert.ToDecimal(0).ToString("#.##");
                        TB_SalePrice.SelectionStart = 0;
                        movement.PurchasePrice = Convert.ToDecimal(0);
                    }
                    break;

                case 3:
                    if (TB_SalePrice.Text.Length > 0)
                        movement.SalePrice = Convert.ToDecimal(TB_SalePrice.Text);

                    else
                    {
                        TB_SalePrice.Text = Convert.ToDecimal(0).ToString("#.##");
                        TB_SalePrice.SelectionStart = 0;
                        movement.SalePrice = Convert.ToDecimal(0);
                    }
                    break;

                case 4:
                    if (TB_PurchaseDiscount.Text.Length > 0)
                        movement.PurchaseDiscount1 = Convert.ToDecimal(TB_PurchaseDiscount.Text);

                    else
                    {
                        TB_PurchaseDiscount.Text = Convert.ToDecimal(0).ToString("#.##");
                        TB_PurchaseDiscount.SelectionStart = 0;
                        movement.PurchaseDiscount1 = Convert.ToDecimal(0);
                    }
                    break;

                case 5:
                    if (TB_SaleDiscount.Text.Length > 0)
                        movement.SaleDiscount1 = Convert.ToDecimal(TB_SaleDiscount.Text);

                    else
                    {
                        TB_SaleDiscount.Text = Convert.ToDecimal(0).ToString("#.##");
                        TB_SaleDiscount.SelectionStart = 0;
                        movement.SaleDiscount1 = Convert.ToDecimal(0);
                    }
                    break;
            }

            EV_ActivateButton();
        }

        protected void EV_SaveMovement(object sender, RoutedEventArgs e)
        {
            GetController().EV_MovementAdd(movement);

            this.Close();
        }

        protected void EV_Search(object sender, RoutedEventArgs e)
        {
            if(sender.GetType() == typeof(ComboBox))
            {
                ComboBoxItem temp;
                switch (Convert.ToInt16(((ComboBox)sender).Tag))
                {
                    // productType
                    case 1:
                        temp = (ComboBoxItem)CB_ProductType.SelectedItem;
                        if (temp != null)
                        {
                            productsView.SetProductType(Convert.ToInt32(temp.Name.Replace("productType", "")));
                        }

                        if(productsView.productType.Name.Contains("MTGCard"))
                        {
                            if (CB_Expansion.Items.Count == 0)
                            {
                                List<Expansion> expansions = productsView.GetExpansions();

                                ComboBoxItem expansionDefault = new ComboBoxItem();
                                expansionDefault.Content = $"";
                                expansionDefault.Name = $"expansion0";
                                CB_Expansion.Items.Add(expansionDefault);

                                foreach (Expansion exp in expansions)
                                {
                                    ComboBoxItem expansion = new ComboBoxItem();
                                    expansion.Content = $"{exp.EnName} ({exp.Abbreviation})";
                                    expansion.Name = $"expansion{exp.Id}";
                                    CB_Expansion.Items.Add(expansion);
                                }

                                if (movement.product != null)
                                {
                                    if (movement.product.ProductTypeID == productsView.productType.ProductTypeID)
                                    {
                                        int num = productsView.GetExpansion(Convert.ToInt32(movement.product.ExternalID)).Id;
                                        foreach (ComboBoxItem item in CB_Expansion.Items)
                                        {
                                            if (Convert.ToInt16(item.Name.Replace("expansion", "")) == num)
                                            {
                                                CB_Expansion.SelectedValue = item;
                                                break;
                                            }
                                        }
                                    }

                                    else
                                        CB_Expansion.SelectedIndex = 0;
                                }

                                else
                                    CB_Expansion.SelectedIndex = 0;
                            }

                            if (CB_Condition.Items.Count == 0)
                            {
                                List<FrameworkDB.V1.Condition> conditions = productsView.GetConditions();

                                foreach (FrameworkDB.V1.Condition ct in conditions)
                                {
                                    ComboBoxItem condition = new ComboBoxItem();
                                    condition.Content = $"{ct.Name}";
                                    condition.Name = $"condition{ct.ConditionID}";
                                    CB_Condition.Items.Add(condition);
                                }

                                if (movement.product != null)
                                {
                                    if (movement.product.ProductTypeID != productsView.productType.ProductTypeID)
                                        movement.condition = productsView.GetConditionDefault();
                                }

                                else
                                    movement.condition = productsView.GetConditionDefault();

                                foreach (ComboBoxItem item in CB_Condition.Items)
                                {
                                    if (Convert.ToInt16(item.Name.Replace("condition", "")) == movement.condition.ConditionID)
                                    {
                                        CB_Condition.SelectedValue = item;
                                        break;
                                    }
                                }
                            }
                            GB_MTGCard.Visibility = Visibility.Visible;
                        }

                        else
                        {
                            productsView.expansion = null;
                            movement.condition = null;

                            CB_Expansion.Items.Clear();
                            CB_Condition.Items.Clear();

                            GB_MTGCard.Visibility = Visibility.Hidden;
                        }
                        break;
                    
                    // expansion
                    case 2:
                        temp = (ComboBoxItem)CB_Expansion.SelectedItem;
                        if (temp != null)
                        {
                            productsView.SetExpansion(Convert.ToInt32(temp.Name.Replace("expansion", "")));
                        }
                        break;
                }
            }

            else if(sender.GetType() == typeof(TextBox))
            {
                switch (Convert.ToInt16(((TextBox)sender).Tag))
                {
                    // productName
                    case 1:
                        productsView.ProductName = TB_ProductName.Text;
                        break;

                    // expansion
                    case 2:
                        break;
                }
            }

            UpdateData();
        }

        public void EV_CleanSelection()
        {
            movement = new Movement();

            TB_Quantity.Text = $"0";
            TB_PurchasePrice.Text = ((decimal)0).ToString("0.00");
            TB_PurchaseDiscount.Text = ((decimal)0).ToString("0.00");
            TB_SalePrice.Text = ((decimal)0).ToString("0.00");
            TB_SaleDiscount.Text = ((decimal)0).ToString("0.00");

            TB_Quantity.IsEnabled = false;
            TB_PurchasePrice.IsEnabled = false;
            TB_PurchaseDiscount.IsEnabled = false;
            TB_SalePrice.IsEnabled = false;
            TB_SaleDiscount.IsEnabled = false;

            BT_SaveMovement.IsEnabled = false;
        }

        public void UpdateData()
        {
            DG_Products.ItemsSource = null;
            DG_Products.ItemsSource = productsView.GetTable(movement.StoreID);
        }

        public void EV_ActivateButton()
        {
                if (movement.product != null && movement.Quantity != null && movement.PurchasePrice != null && movement.SalePrice != null)
                BT_SaveMovement.IsEnabled = true;

            else
                BT_SaveMovement.IsEnabled = false;
        }

        virtual public Main.Controller.CT_Common GetController()
        {
            return new Main.Controller.CT_Common();
        }
    }
}