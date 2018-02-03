using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrameworkView.V1;

namespace GestCloudv2.Documents.DCM_Items.DCM_Item_New.View
{
    /// <summary>
    /// Interaction logic for MC_DCM_Item_New_Summary.xaml
    /// </summary>
    public partial class MC_DCM_Item_New_Summary : Page
    {
        public MC_DCM_Item_New_Summary()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);

            BT_Provider.Click += new RoutedEventHandler(EV_PopClick);
            BT_Store.Click += new RoutedEventHandler(EV_PopClick);
        }

        public void EV_Start(object sender, RoutedEventArgs e)
        {
            switch(GetController().Information["operationType"])
            {
                case 1:
                    InitializingProvider();
                    GR_Provider.Visibility = Visibility.Visible;
                    break;

                case 2:
                    InitializingClient();
                    GR_Client.Visibility = Visibility.Visible;
                    break;
            }
            
            InitializingStore();
            InitializingCode();
            InitializingDate();

            UpdateContent();
            UpdateBottom();
        }

        public void InitializingProvider()
        {
            SP_Provider.Width = BT_Provider.ActualWidth;
            if(GetController().GetProvider() != null)
                ((TextBlock)BT_Provider.Template.FindName("TB_Provider", BT_Provider)).Text = $"Proveedor: {GetController().GetProvider().Code} / {GetController().GetProvider().entity.Name}";
        }

        public void InitializingClient()
        {
            SP_Client.Width = BT_Client.ActualWidth;
            if (GetController().GetClient() != null)
                ((TextBlock)BT_Client.Template.FindName("TB_Client", BT_Client)).Text = $"Cliente: {GetController().GetClient().Code} / {GetController().GetClient().entity.Name} {GetController().GetClient().entity.Subname}";
        }

        public void InitializingStore()
        {
            SP_Store.Width = BT_Store.ActualWidth;
            ((TextBlock)BT_Store.Template.FindName("TB_Store", BT_Store)).Text = $"Almacén: {GetController().GetStore().Code} / {GetController().GetStore().Name}";
        }

        public void InitializingCode()
        {
            SP_Code.Width = BT_Code.ActualWidth;
            ((TextBlock)BT_Code.Template.FindName("TB_Code", BT_Code)).Text = $"Nº Documento: 18 / {GetController().GetDocumentID()}";
        }

        public void InitializingDate()
        {
            LB_Date1.Content = $"{GetController().GetDate().ToString("dd")}";
            LB_Date2.Content = $"{GetController().GetDate().ToString("MMMM")}";
        }

        public void UpdateContent()
        {
            foreach(DocumentLine item in GetController().documentContent.Lines)
            {
                Grid grid = new Grid();
                ColumnDefinition column1 = new ColumnDefinition();
                ColumnDefinition column2 = new ColumnDefinition();
                ColumnDefinition column3 = new ColumnDefinition();
                ColumnDefinition column4 = new ColumnDefinition();
                ColumnDefinition column5 = new ColumnDefinition();
                ColumnDefinition column6 = new ColumnDefinition();
                ColumnDefinition column7 = new ColumnDefinition();
                column1.Width = new GridLength(2, GridUnitType.Star);
                column2.Width = new GridLength(1, GridUnitType.Star);
                column3.Width = new GridLength(0.5, GridUnitType.Star);
                column4.Width = new GridLength(1, GridUnitType.Star);
                column5.Width = new GridLength(1, GridUnitType.Star);
                column6.Width = new GridLength(1, GridUnitType.Star);
                column7.Width = new GridLength(1, GridUnitType.Star);

                grid.ColumnDefinitions.Add(column1);
                grid.ColumnDefinitions.Add(column2);
                grid.ColumnDefinitions.Add(column3);
                grid.ColumnDefinitions.Add(column4);
                grid.ColumnDefinitions.Add(column5);
                grid.ColumnDefinitions.Add(column6);
                grid.ColumnDefinitions.Add(column7);

                // Description

                Border BR_Name = new Border();
                BR_Name.BorderBrush = new SolidColorBrush(Colors.CadetBlue);
                BR_Name.BorderThickness = new Thickness(0, 0, 0, 1);
                Grid.SetColumn(BR_Name, 0);

                Label LB_Name = new Label();
                LB_Name.Content = item.Name;
                LB_Name.Foreground = new SolidColorBrush(Colors.CadetBlue);
                LB_Name.HorizontalAlignment = HorizontalAlignment.Center;

                BR_Name.Child = LB_Name;
                grid.Children.Add(BR_Name);

                // Quantity

                Border BR_Quantity = new Border();
                BR_Quantity.BorderBrush = new SolidColorBrush(Colors.CadetBlue);
                BR_Quantity.BorderThickness = new Thickness(0, 0, 0, 1);
                Grid.SetColumn(BR_Quantity, 2);

                Label LB_Quantity = new Label();
                LB_Quantity.Content = item.Quantity.ToString("0.##");
                LB_Quantity.Foreground = new SolidColorBrush(Colors.CadetBlue);
                LB_Quantity.HorizontalAlignment = HorizontalAlignment.Center;

                BR_Quantity.Child = LB_Quantity;
                grid.Children.Add(BR_Quantity);

                if (GetController().Information["operationType"] == 1)
                {
                    // TaxBase

                    Border BR_TaxBase = new Border();
                    BR_TaxBase.BorderBrush = new SolidColorBrush(Colors.CadetBlue);
                    BR_TaxBase.BorderThickness = new Thickness(0, 0, 0, 1);
                    Grid.SetColumn(BR_TaxBase, 1);

                    Label LB_TaxBase = new Label();
                    LB_TaxBase.Content = item.PurchaseTaxBase.ToString("0.00");
                    LB_TaxBase.Foreground = new SolidColorBrush(Colors.CadetBlue);
                    LB_TaxBase.HorizontalAlignment = HorizontalAlignment.Center;

                    BR_TaxBase.Child = LB_TaxBase;
                    grid.Children.Add(BR_TaxBase);

                    // TaxBaseFinal

                    Border BR_TaxBaseFinal = new Border();
                    BR_TaxBaseFinal.BorderBrush = new SolidColorBrush(Colors.CadetBlue);
                    BR_TaxBaseFinal.BorderThickness = new Thickness(0, 0, 0, 1);
                    Grid.SetColumn(BR_TaxBaseFinal, 3);

                    Label LB_TaxBaseFinal = new Label();
                    LB_TaxBaseFinal.Content = item.PurchaseTaxBaseFinal.ToString("0.00");
                    LB_TaxBaseFinal.Foreground = new SolidColorBrush(Colors.CadetBlue);
                    LB_TaxBaseFinal.HorizontalAlignment = HorizontalAlignment.Center;

                    BR_TaxBaseFinal.Child = LB_TaxBaseFinal;
                    grid.Children.Add(BR_TaxBaseFinal);

                    // TaxAmount

                    Border BR_TaxAmount = new Border();
                    BR_TaxAmount.BorderBrush = new SolidColorBrush(Colors.CadetBlue);
                    BR_TaxAmount.BorderThickness = new Thickness(0, 0, 0, 1);
                    Grid.SetColumn(BR_TaxAmount, 4);

                    Label LB_TaxAmount = new Label();
                    LB_TaxAmount.Content = item.PurchaseTaxAmount.ToString("0.00");
                    LB_TaxAmount.Foreground = new SolidColorBrush(Colors.CadetBlue);
                    LB_TaxAmount.HorizontalAlignment = HorizontalAlignment.Center;

                    BR_TaxAmount.Child = LB_TaxAmount;
                    grid.Children.Add(BR_TaxAmount);

                    // EquSurAmount

                    Border BR_EquSurAmount = new Border();
                    BR_EquSurAmount.BorderBrush = new SolidColorBrush(Colors.CadetBlue);
                    BR_EquSurAmount.BorderThickness = new Thickness(0, 0, 0, 1);
                    Grid.SetColumn(BR_EquSurAmount, 5);

                    Label LB_EquSurAmount = new Label();
                    LB_EquSurAmount.Content = item.PurchaseEquSurAmount.ToString("0.00");
                    LB_EquSurAmount.Foreground = new SolidColorBrush(Colors.CadetBlue);
                    LB_EquSurAmount.HorizontalAlignment = HorizontalAlignment.Center;

                    BR_EquSurAmount.Child = LB_EquSurAmount;
                    grid.Children.Add(BR_EquSurAmount);

                    // Finalprice

                    Border BR_FinalPrice = new Border();
                    BR_FinalPrice.BorderBrush = new SolidColorBrush(Colors.CadetBlue);
                    BR_FinalPrice.BorderThickness = new Thickness(0, 0, 0, 1);
                    Grid.SetColumn(BR_FinalPrice, 6);

                    Label LB_FinalPrice = new Label();
                    LB_FinalPrice.Content = item.PurchaseFinalPrice.ToString("0.00");
                    LB_FinalPrice.Foreground = new SolidColorBrush(Colors.CadetBlue);
                    LB_FinalPrice.HorizontalAlignment = HorizontalAlignment.Center;

                    BR_FinalPrice.Child = LB_FinalPrice;
                    grid.Children.Add(BR_FinalPrice);
                }

                if (GetController().Information["operationType"] == 2)
                {
                    // TaxBase

                    Border BR_TaxBase = new Border();
                    BR_TaxBase.BorderBrush = new SolidColorBrush(Colors.CadetBlue);
                    BR_TaxBase.BorderThickness = new Thickness(0, 0, 0, 1);
                    Grid.SetColumn(BR_TaxBase, 1);

                    Label LB_TaxBase = new Label();
                    LB_TaxBase.Content = item.SaleTaxBase.ToString("0.00");
                    LB_TaxBase.Foreground = new SolidColorBrush(Colors.CadetBlue);
                    LB_TaxBase.HorizontalAlignment = HorizontalAlignment.Center;

                    BR_TaxBase.Child = LB_TaxBase;
                    grid.Children.Add(BR_TaxBase);

                    // TaxBaseFinal

                    Border BR_TaxBaseFinal = new Border();
                    BR_TaxBaseFinal.BorderBrush = new SolidColorBrush(Colors.CadetBlue);
                    BR_TaxBaseFinal.BorderThickness = new Thickness(0, 0, 0, 1);
                    Grid.SetColumn(BR_TaxBaseFinal, 3);

                    Label LB_TaxBaseFinal = new Label();
                    LB_TaxBaseFinal.Content = item.SaleTaxBaseFinal.ToString("0.00");
                    LB_TaxBaseFinal.Foreground = new SolidColorBrush(Colors.CadetBlue);
                    LB_TaxBaseFinal.HorizontalAlignment = HorizontalAlignment.Center;

                    BR_TaxBaseFinal.Child = LB_TaxBaseFinal;
                    grid.Children.Add(BR_TaxBaseFinal);

                    // TaxAmount

                    Border BR_TaxAmount = new Border();
                    BR_TaxAmount.BorderBrush = new SolidColorBrush(Colors.CadetBlue);
                    BR_TaxAmount.BorderThickness = new Thickness(0, 0, 0, 1);
                    Grid.SetColumn(BR_TaxAmount, 4);

                    Label LB_TaxAmount = new Label();
                    LB_TaxAmount.Content = item.SaleTaxAmount.ToString("0.00");
                    LB_TaxAmount.Foreground = new SolidColorBrush(Colors.CadetBlue);
                    LB_TaxAmount.HorizontalAlignment = HorizontalAlignment.Center;

                    BR_TaxAmount.Child = LB_TaxAmount;
                    grid.Children.Add(BR_TaxAmount);

                    // EquSurAmount

                    Border BR_EquSurAmount = new Border();
                    BR_EquSurAmount.BorderBrush = new SolidColorBrush(Colors.CadetBlue);
                    BR_EquSurAmount.BorderThickness = new Thickness(0, 0, 0, 1);
                    Grid.SetColumn(BR_EquSurAmount, 5);

                    Label LB_EquSurAmount = new Label();
                    LB_EquSurAmount.Content = item.SaleEquSurAmount.ToString("0.00");
                    LB_EquSurAmount.Foreground = new SolidColorBrush(Colors.CadetBlue);
                    LB_EquSurAmount.HorizontalAlignment = HorizontalAlignment.Center;

                    BR_EquSurAmount.Child = LB_EquSurAmount;
                    grid.Children.Add(BR_EquSurAmount);

                    // EquSurAmount

                    Border BR_FinalPrice = new Border();
                    BR_FinalPrice.BorderBrush = new SolidColorBrush(Colors.CadetBlue);
                    BR_FinalPrice.BorderThickness = new Thickness(0, 0, 0, 1);
                    Grid.SetColumn(BR_FinalPrice, 6);

                    Label LB_FinalPrice = new Label();
                    LB_FinalPrice.Content = item.SaleFinalPrice.ToString("0.00");
                    LB_FinalPrice.Foreground = new SolidColorBrush(Colors.CadetBlue);
                    LB_FinalPrice.HorizontalAlignment = HorizontalAlignment.Center;

                    BR_FinalPrice.Child = LB_FinalPrice;
                    grid.Children.Add(BR_FinalPrice);
                }
                // Add Line

                SP_Lines.Children.Add(grid);
            }
        }

        public void UpdateBottom()
        {
            switch (GetController().Information["operationType"])
            {
                case 1:
                    LB_TaxBase.Content = $"{GetController().documentContent.PurchaseTaxBaseFinal.ToString("0.00")} €";
                    LB_TaxBase1.Content = $"{GetController().documentContent.PurchaseTaxBases[1].ToString("0.00")}";
                    LB_TaxBase2.Content = $"{GetController().documentContent.PurchaseTaxBases[2].ToString("0.00")}";
                    LB_TaxBase3.Content = $"{GetController().documentContent.PurchaseTaxBases[3].ToString("0.00")}";
                    LB_TaxBase4.Content = $"{GetController().documentContent.PurchaseTaxBases[4].ToString("0.00")}";
                    LB_TaxBase5.Content = $"{GetController().documentContent.PurchaseTaxBases[5].ToString("0.00")}";

                    LB_TaxAmount.Content = $"{GetController().documentContent.PurchaseTaxAmount.ToString("0.00")} €";
                    LB_TaxAmount1.Content = $"{GetController().documentContent.PurchaseTaxAmounts[1].ToString("0.00")}";
                    LB_TaxAmount2.Content = $"{GetController().documentContent.PurchaseTaxAmounts[2].ToString("0.00")}";
                    LB_TaxAmount3.Content = $"{GetController().documentContent.PurchaseTaxAmounts[3].ToString("0.00")}";
                    LB_TaxAmount4.Content = $"{GetController().documentContent.PurchaseTaxAmounts[4].ToString("0.00")}";
                    LB_TaxAmount5.Content = $"{GetController().documentContent.PurchaseTaxAmounts[5].ToString("0.00")}";

                    LB_EquSurAmount.Content = $"{GetController().documentContent.PurchaseEquSurAmount.ToString("0.00")} €";
                    LB_EquSurAmount1.Content = $"{GetController().documentContent.PurchaseEquSurAmounts[1].ToString("0.00")}";
                    LB_EquSurAmount2.Content = $"{GetController().documentContent.PurchaseEquSurAmounts[2].ToString("0.00")}";
                    LB_EquSurAmount3.Content = $"{GetController().documentContent.PurchaseEquSurAmounts[3].ToString("0.00")}";
                    LB_EquSurAmount4.Content = $"{GetController().documentContent.PurchaseEquSurAmounts[4].ToString("0.00")}";
                    LB_EquSurAmount5.Content = $"{GetController().documentContent.PurchaseEquSurAmounts[5].ToString("0.00")}";

                    LB_FinalPrice.Content = $"{GetController().documentContent.PurchaseFinalPrice.ToString("0.00")} €";
                    LB_FinalPrice1.Content = $"{GetController().documentContent.PurchaseFinalPrices[1].ToString("0.00")}";
                    LB_FinalPrice2.Content = $"{GetController().documentContent.PurchaseFinalPrices[2].ToString("0.00")}";
                    LB_FinalPrice3.Content = $"{GetController().documentContent.PurchaseFinalPrices[3].ToString("0.00")}";
                    LB_FinalPrice4.Content = $"{GetController().documentContent.PurchaseFinalPrices[4].ToString("0.00")}";
                    LB_FinalPrice5.Content = $"{GetController().documentContent.PurchaseFinalPrices[5].ToString("0.00")}";
                    break;

                case 2:
                    LB_TaxBase.Content = $"{GetController().documentContent.SaleTaxBaseFinal.ToString("0.00")} €";
                    LB_TaxBase1.Content = $"{GetController().documentContent.SaleTaxBases[1].ToString("0.00")}";
                    LB_TaxBase2.Content = $"{GetController().documentContent.SaleTaxBases[2].ToString("0.00")}";
                    LB_TaxBase3.Content = $"{GetController().documentContent.SaleTaxBases[3].ToString("0.00")}";
                    LB_TaxBase4.Content = $"{GetController().documentContent.SaleTaxBases[4].ToString("0.00")}";
                    LB_TaxBase5.Content = $"{GetController().documentContent.SaleTaxBases[5].ToString("0.00")}";

                    LB_TaxAmount.Content = $"{GetController().documentContent.SaleTaxAmount.ToString("0.00")} €";
                    LB_TaxAmount1.Content = $"{GetController().documentContent.SaleTaxAmounts[1].ToString("0.00")}";
                    LB_TaxAmount2.Content = $"{GetController().documentContent.SaleTaxAmounts[2].ToString("0.00")}";
                    LB_TaxAmount3.Content = $"{GetController().documentContent.SaleTaxAmounts[3].ToString("0.00")}";
                    LB_TaxAmount4.Content = $"{GetController().documentContent.SaleTaxAmounts[4].ToString("0.00")}";
                    LB_TaxAmount5.Content = $"{GetController().documentContent.SaleTaxAmounts[5].ToString("0.00")}";

                    LB_EquSurAmount.Content = $"{GetController().documentContent.SaleEquSurAmount.ToString("0.00")} €";
                    LB_EquSurAmount1.Content = $"{GetController().documentContent.SaleEquSurAmounts[1].ToString("0.00")}";
                    LB_EquSurAmount2.Content = $"{GetController().documentContent.SaleEquSurAmounts[2].ToString("0.00")}";
                    LB_EquSurAmount3.Content = $"{GetController().documentContent.SaleEquSurAmounts[3].ToString("0.00")}";
                    LB_EquSurAmount4.Content = $"{GetController().documentContent.SaleEquSurAmounts[4].ToString("0.00")}";
                    LB_EquSurAmount5.Content = $"{GetController().documentContent.SaleEquSurAmounts[5].ToString("0.00")}";

                    LB_FinalPrice.Content = $"{GetController().documentContent.SaleFinalPrice.ToString("0.00")} €";
                    LB_FinalPrice1.Content = $"{GetController().documentContent.SaleFinalPrices[1].ToString("0.00")}";
                    LB_FinalPrice2.Content = $"{GetController().documentContent.SaleFinalPrices[2].ToString("0.00")}";
                    LB_FinalPrice3.Content = $"{GetController().documentContent.SaleFinalPrices[3].ToString("0.00")}";
                    LB_FinalPrice4.Content = $"{GetController().documentContent.SaleFinalPrices[4].ToString("0.00")}";
                    LB_FinalPrice5.Content = $"{GetController().documentContent.SaleFinalPrices[5].ToString("0.00")}";
                    break;
            }

        }

        public void EV_PopClick(object sender, RoutedEventArgs e)
        {
            if(Convert.ToBoolean((sender as ToggleButton).IsChecked))
            {
                switch(Convert.ToInt16((sender as ToggleButton).Tag))
                {
                    case 1:
                        ((Border)BT_Provider.Template.FindName("BR_Provider", BT_Provider)).CornerRadius = new CornerRadius(10, 10, 0, 0);
                        break;

                    case 2:
                        ((Border)BT_Store.Template.FindName("BR_Store", BT_Store)).CornerRadius = new CornerRadius(10, 10, 0, 0);
                        break;
                }
            }

            else
            {
                switch (Convert.ToInt16((sender as ToggleButton).Tag))
                {
                    case 1:
                        ((Border)BT_Provider.Template.FindName("BR_Provider", BT_Provider)).CornerRadius = new CornerRadius(10);
                        break;

                    case 2:
                        ((Border)BT_Store.Template.FindName("BR_Store", BT_Store)).CornerRadius = new CornerRadius(10);
                        break;
                }
            }
        }

        virtual public Controller.CT_DCM_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_DCM_Item_New)a.MainFrame.Content;
        }
    }
}
