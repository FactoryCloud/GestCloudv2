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

namespace GestCloudv2
{
    /// <summary>
    /// Interaction logic for Main_Navigation.xaml
    /// </summary>
    public partial class Main_Navigation : Page
    {
        public Main_Navigation()
        {
            InitializeComponent();
        }

        private void UsersNavigationEvent(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;

            a.changeLeftSide(new UserList_ToolSide());
            a.changeMainContent(new UserList_MainContent());
        }

        private void CardsNavigationEvent(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;

            a.changeLeftSide(new CardList.CardList_ToolSide());
            a.changeMainContent(new CardList.CardList_MainContent());
        }
    }
}
