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
using Microsoft.EntityFrameworkCore;
using FrameworkDB.V1;
using FrameworkView.V1;
using GestCloudv2.Main;

namespace GestCloudv2.StockItem.AddStock
{
    /// <summary>
    /// Interaction logic for AddStock_Controller.xaml
    /// </summary>
    public partial class AddStock_Controller : Page
    {
        public MovementsView movementsView;
        public Dictionary<string, int> Information;
        private Page MainContentStock;
        private Page ToolSideStock;
        private Page NavigationStock;
        public GestCloudDB db;

        public AddStock_Controller()
        {
            InitializeComponent();
            db = new GestCloudDB();
            movementsView = new MovementsView();
            Information = new Dictionary<string, int>();
            Information.Add("changes", 0);

            this.Loaded += new RoutedEventHandler(EV_StartAddStock);
        }

        private void EV_StartAddStock(object sender, RoutedEventArgs e)
        {
            MainContentStock = new MainAddStock.AddStock_MainContent();
            ToolSideStock = new MainAddStock.AddStock_ToolSide();
            NavigationStock = new MainAddStock.AddStock_Navigation();
            UpdateComponents();
        }

        public void AddNewMovement (Movement mov)
        {
            movementsView.AddMovement(mov);
            MainContentStock = new MainAddStock.AddStock_MainContent();
            MainContent.Content = MainContentStock;
        }

        /*public void RestartNewMovement()
        {
            FloatWindows.ProductSelectWindow floatWindow = new FloatWindows.ProductSelectWindow(
                movementsView.movements.Last(), "AddStock_NewMovement");
            floatWindow.Show();
        }*/

        private void UpdateComponents()
        {
            MainContent.Content = MainContentStock;
            LeftSide.Content = ToolSideStock;
            TopSide.Content = NavigationStock;
        }

        public void BackToMain()
        {
            Information["controller"] = 0;
            ChangeComponents();
        }

        private void ChangeComponents()
        {
            switch (Information["controller"])
            {
                case 0:
                    if (Information["changes"] > 0)
                    {
                        MessageBoxResult result = MessageBox.Show("Usted ha realizado cambios, ¿Esta seguro que desea salir?", "Volver", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                    Main.View.MainWindow a = (Main.View.MainWindow)Application.Current.MainWindow;
                    a.MainFrame.Content = new Main.Controller.CT_Main();
                    break;
            }
        }
    }
}
