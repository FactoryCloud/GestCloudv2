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
using System.Data;

namespace GestCloudv2.Documents.DCM_Items.DCM_Item_New.View
{
    /// <summary>
    /// Interaction logic for MC_DCM_Item_New_Movements.xaml
    /// </summary>
    public partial class MC_DCM_Item_New_Movements : Page
    {
        public MC_DCM_Item_New_Movements()
        {
            InitializeComponent();

            DG_Movements.MouseLeftButtonUp += new MouseButtonEventHandler(EV_MovementsSelect);
            //GetController().EV_MovementsUpdate();

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            GetController().EV_UpdateSubMenu(0);
            UpdateData();
            UpdateBottom();
        }

        public void EV_MovementsSelect(object sender, RoutedEventArgs e)
        {
            int movement = DG_Movements.SelectedIndex;

            if (movement >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Movements.ItemContainerGenerator.ContainerFromIndex(movement);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetMovementSelected(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        public void UpdateData()
        {
            DG_Movements.ItemsSource = null;
            DG_Movements.ItemsSource = GetController().documentContent.GetTable();
        }

        public void UpdateBottom()
        {
            switch(GetController().Information["operationType"])
            {
                case 1:
                    LB_GrossPrice.Content = $"{GetController().documentContent.PurchaseGrossPrice.ToString("0.00")} €";
                    LB_Discount.Content = $"{GetController().documentContent.PurchaseDiscount.ToString("0.00")} €";
                    LB_TaxBase.Content = $"{GetController().documentContent.PurchaseTaxBase.ToString("0.00")} €";
                    LB_TaxAmount.Content = $"{GetController().documentContent.PurchaseTaxAmount.ToString("0.00")} €";
                    LB_EqSurAmount.Content = $"{GetController().documentContent.PurchaseEquSurAmount.ToString("0.00")} €";
                    LB_FinalPrice.Content = $"{GetController().documentContent.PurchaseFinalPrice.ToString("0.00")} €";
                    break;

                case 2:
                    LB_GrossPrice.Content = $"{GetController().documentContent.SaleGrossPrice.ToString("0.00")} €";
                    LB_Discount.Content = $"{GetController().documentContent.SaleDiscount.ToString("0.00")} €";
                    LB_TaxBase.Content = $"{GetController().documentContent.SaleTaxBase.ToString("0.00")} €";
                    LB_TaxAmount.Content = $"{GetController().documentContent.SaleTaxAmount.ToString("0.00")} €";
                    LB_EqSurAmount.Content = $"{GetController().documentContent.SaleEquSurAmount.ToString("0.00")} €";
                    LB_FinalPrice.Content = $"{GetController().documentContent.SaleFinalPrice.ToString("0.00")} €";
                    break;
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
