using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace GestCloudv2.Files.Nodes.Products.ProductItem.ProductItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_New_Company.xaml
    /// </summary>
    public partial class MC_PDT_Item_New_Product : Page
    {
        public MC_PDT_Item_New_Product()
        {
            InitializeComponent();
            TB_ProductCode.KeyUp += new KeyEventHandler(EV_CodeChange);
            TB_ProductName.KeyUp += new KeyEventHandler(EV_NameChange);
            CB_ProductType.SelectionChanged += new SelectionChangedEventHandler(EV_CB_ProductType);
            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            List<ProductType> productTypes = GetController().GetProductTypes();
            foreach (ProductType tx in productTypes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{tx.Name}";
                temp.Name = $"ProductType{tx.ProductTypeID}";
                CB_ProductType.Items.Add(temp);
            }

            foreach (ComboBoxItem item in CB_ProductType.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("ProductType", "")) == GetController().productTypeSelected.ProductTypeID)
                {
                    CB_ProductType.SelectedValue = item;
                    break;
                }
            }

            TB_ProductCode.Text = $"{GetController().GetProductCode()}";
            TB_ProductName.Text = $"{GetController().GetProductName()}";
        }

        private void EV_CB_ProductType(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_ProductType.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetProductTypeSelected(Convert.ToInt32(temp1.Name.Replace("ProductType", "")));
            }
        }

        private void EV_NameChange(object sender, RoutedEventArgs e)
        {
            if (TB_ProductName.Text.Length > 0)
            {
                GetController().SetProductName(TB_ProductName.Text);
            }
            else
            {
                GetController().CleanName();
            }
        }

        private void EV_CodeChange(object sender, KeyEventArgs e)
        {
            if (Regex.Matches(TB_ProductCode.Text, "[^0-9]").Count > 0)
            {
                TB_ProductCode.Text = Regex.Replace(TB_ProductCode.Text, "[^0-9]", "");
                TB_ProductCode.SelectionStart = TB_ProductCode.Text.Length;
            }

            if (TB_ProductCode.Text.Length > 0)
            {
                if (GetController().EV_CodeValid(Convert.ToInt32(TB_ProductCode.Text)))
                {
                    GetController().SetProductCode(Convert.ToInt32(TB_ProductCode.Text));
                }
            }
            else
            {
                GetController().CleanCode();
            }
        }

        private Controller.CT_PDT_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PDT_Item_New)a.MainFrame.Content;
        }
    }
}
