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

namespace GestCloudv2.Files.Nodes.ProductTypes.ProductTypeItem.ProductTypeItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_New_Company.xaml
    /// </summary>
    public partial class MC_PTY_Item_New_ProductType_Taxes : Page
    {
        public MC_PTY_Item_New_ProductType_Taxes()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            List<Tax> taxes = GetController().GetTaxes();
            foreach (Tax tx in taxes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{tx.Percentage} - {tx.taxType.Name}";
                temp.Name = $"TaxType{tx.TaxID}";
                CB_PurchaseTax.Items.Add(temp);
            }
        }

        private void EV_CompaniesChange(object sender, RoutedEventArgs e)
        {
            //GetController().UpdateCompanies(Convert.ToInt32((sender as CheckBox).Tag.ToString().Replace("company", "")));
        }

        private void EV_MD_StoresAll(object sender, RoutedEventArgs e)
        {
            //GetController().MD_StoresChange(1);
        }

        private void EV_MD_StoresNone(object sender, RoutedEventArgs e)
        {
            //GetController().MD_StoresChange(0);
        }

        private Controller.CT_PTY_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PTY_Item_New)a.MainFrame.Content;
        }
    }
}
