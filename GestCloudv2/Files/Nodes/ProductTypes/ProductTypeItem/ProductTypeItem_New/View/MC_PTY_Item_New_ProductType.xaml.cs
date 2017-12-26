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

namespace GestCloudv2.Files.Nodes.ProductTypes.ProductTypeItem.ProductTypeItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_New_Company.xaml
    /// </summary>
    public partial class MC_PTY_Item_New_ProductType : Page
    {
        public MC_PTY_Item_New_ProductType()
        {
            InitializeComponent();
            TB_ProductTypeCode.KeyUp += new KeyEventHandler(EV_CodeChange);
            TB_ProductTypeName.KeyUp += new KeyEventHandler(EV_NameChange);
            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_ProductTypeCode.Text = $"{GetController().GetProductTypeCode()}";
            TB_ProductTypeName.Text = $"{GetController().GetProductTypeName()}";
        }

        private void EV_NameChange(object sender, RoutedEventArgs e)
        {
            if (TB_ProductTypeName.Text.Length > 0)
            {
                GetController().SetProductTypeName(TB_ProductTypeName.Text);
            }
            else
            {
                GetController().CleanName();
            }
        }

        private void EV_CodeChange(object sender, KeyEventArgs e)
        {
            if (Regex.Matches(TB_ProductTypeCode.Text, "[^0-9]").Count > 0)
            {
                TB_ProductTypeCode.Text = Regex.Replace(TB_ProductTypeCode.Text, "[^0-9]", "");
                TB_ProductTypeCode.SelectionStart = TB_ProductTypeCode.Text.Length;
            }

            if (TB_ProductTypeCode.Text.Length > 0)
            {
                if (GetController().EV_CodeValid(Convert.ToInt32(TB_ProductTypeCode.Text)))
                {
                    GetController().SetProductTypeCode(Convert.ToInt32(TB_ProductTypeCode.Text));
                }
            }
            else
            {
                GetController().CleanCode();
            }
        }

        private Controller.CT_PTY_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PTY_Item_New)a.MainFrame.Content;
        }
    }
}
