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
    /// Interaction logic for AddStock_MainContent.xaml
    /// </summary>
    public partial class AddStock_MainContent : Page
    {
        public AddStock_MainContent()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        public void UpdateData()
        {
            DB_Movements.ItemsSource = null;
            DB_Movements.ItemsSource = GetController().movementsView.GetTable();
        }

        private StockItem.AddStock.AddStock_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (StockItem.AddStock.AddStock_Controller)a.MainFrame.Content;
        }

        private void DatePicker_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
