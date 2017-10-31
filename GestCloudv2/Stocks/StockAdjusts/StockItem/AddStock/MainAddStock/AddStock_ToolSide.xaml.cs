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

namespace GestCloudv2.StockItem.AddStock.MainAddStock
{
    /// <summary>
    /// Interaction logic for AddStock_ToolSide.xaml
    /// </summary>
    public partial class AddStock_ToolSide : Page
    {
        public AddStock_ToolSide()
        {
            InitializeComponent();
        }

        private void Event_NewLine(object sender, RoutedEventArgs e)
        {
            FloatWindows.ProductSelectWindow floatWindow = new FloatWindows.ProductSelectWindow();
            floatWindow.Show();
        }

        private void EV_EditMovement(object sender, RoutedEventArgs e)
        {
            FloatWindows.ProductSelectWindow floatWindow = new FloatWindows.ProductSelectWindow(
                GetController().movementsView.movements[0], "AddStock_EditMovement");
            floatWindow.Show();
        }

        private StockItem.AddStock.AddStock_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MC_Main)mainWindow;
            return (StockItem.AddStock.AddStock_Controller)a.MainPage.Content;
        }
    }
}
