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
using FrameworkView.V1;

namespace GestCloudv2.CardList
{
    /// <summary>
    /// Interaction logic for CardList_MainContent.xaml
    /// </summary>
    public partial class CardList_MainContent : Page
    {
        CardsView cardsView;
        public CardList_MainContent()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(StartEvent);
            ExpansionSearchBox.SelectionChanged += new SelectionChangedEventHandler(SearchEvent);

            cardsView = new CardsView();
            UpdateData();
        }

        private void StartEvent(object sender, RoutedEventArgs e)
        {
            List<Expansion> expansions = cardsView.GetExpansions();

            foreach(Expansion exp in expansions.Take(150))
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{exp.EnName} ({exp.Abbreviation})";
                temp.Name = $"expansion{exp.Id}";
                ExpansionSearchBox.Items.Add(temp);
            }
        }

        private void SearchEvent(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp = (ComboBoxItem) ExpansionSearchBox.SelectedItem;
            //MessageBox.Show($"Elemento cambiado a {temp.Name.Replace("expansion","")}");

            cardsView.SetExpansion(Convert.ToInt32(temp.Name.Replace("expansion", "")));
            cardsView.UpdateFilteredTable();
        }

        public void UpdateData()
        {
            CardsTable.ItemsSource = null;
            CardsTable.ItemsSource = cardsView.GetTable();
        }

        private Main.Main_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (Main.Main_Controller)a.MainPage.Content;
        }
    }
}
