﻿using System;
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

namespace GestCloudv2.PurchasesDelivery.Nodes.PurchaseDeliveries.PurchaseDeliveryItem.PurchaseDeliveryItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_POR_Item_New_PurchaseOrder.xaml
    /// </summary>
    public partial class MC_PDE_Item_New_PurchaseDelivery : Page
    {

        public MC_PDE_Item_New_PurchaseDelivery()
        {

            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);
            //TB_StockAdjustReference.KeyUp += new KeyEventHandler(EV_OrderReference);
            //TB_StockAdjustReference.Loaded += new RoutedEventHandler(EV_OrderReference);
            TB_StockAdjustCode.KeyUp += new KeyEventHandler(EV_OrderCode);
            TB_StockAdjustCode.Loaded += new RoutedEventHandler(EV_OrderCode);
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
            DP_Date.SelectedDate = DateTime.Now;
            TB_StockAdjustCode.Text = GetController().GetLastOrderCode();
            List<Store> stores = GetController().GetStores();
            foreach (Store st in stores)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{st.Code} - {st.Name}";
                temp.Name = $"store{st.StoreID}";
                CB_Stores.Items.Add(temp);
            }

            CB_Stores.SelectedIndex = 0;

            if (GetController().provider.entity != null)
            {
                TB_ProviderName.Text = GetController().provider.entity.Name;
                TB_ProviderCode.Text = GetController().provider.ProviderID.ToString();
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
            if (GetController().provider.ProviderID > 0)
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

            if (GetController().provider.ProviderID > 0)
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

        private void EV_OrderCode(object sender, RoutedEventArgs e)
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
                GetController().CleanOrderCode();
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
                GetController().CleanOrderCode();
            }

             else if (GetController().PurchaseOrderExist(TB_StockAdjustCode.Text))
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

        private void EV_OrderReference(object sender, RoutedEventArgs e)
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
                GetController().purchaseDelivery.Date = null;
            }
            else
            {
                this.Title = date.Value.ToShortDateString();
                GetController().SetOrderDate(date.Value);
            }
        }

        private void DP_Date_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void EV_ProviderSelect(object sender, RoutedEventArgs e)
        {
            GetController().MD_ProviderSelect();
        }

        private Controller.CT_PDE_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PDE_Item_New)a.MainFrame.Content;
        }
    }
}
