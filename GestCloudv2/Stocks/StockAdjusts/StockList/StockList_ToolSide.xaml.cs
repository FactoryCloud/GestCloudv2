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

namespace GestCloudv2.StockList
{
    /// <summary>
    /// Interaction logic for Stock_ToolSide.xaml
    /// </summary>
    public partial class StockList_ToolSide : Page
    {
        public StockList_ToolSide()
        {
            InitializeComponent();
        }

        private void AddStockMovement_Event(object sender, RoutedEventArgs e)
        {
            //GetController().StartAddStock();
        }

        private Main.Controller.CT_Main GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Main.Controller.CT_Main)a.MainFrame.Content;
        }
    }
}
