using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace GestCloudv2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //UserList_ToolSide toolSide;
        //UserList_MainContent mainContent;
        Main_Navigation topNavigation;
        Dictionary<string, string> Information;
        User user;

        public MainWindow(User user)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            Application.Current.MainWindow = this;

            Information = new Dictionary<string, string>();
            this.user = user;

            //toolSide = new UserList_ToolSide();
            //mainContent = new UserList_MainContent();

            topNavigation = new Main_Navigation();
            TopSide.Content = topNavigation;

            //LeftSide.Content = toolSide;
            //MainContent.Content = mainContent;
        }

        public void ChangeContent(Page mainPage, Page navigationPage, Page toolPage)
        {
            MainContent.Content = mainPage;
            TopSide.Content = navigationPage;
            LeftSide.Content = toolPage;
        }

        public void changeLeftSide(Page page)
        {
            LeftSide.Content = page;
        }

        public void changeMainContent(Page page)
        {
            MainContent.Content = page;
        }

        public void changeTopSide(Page page)
        {
            TopSide.Content = page;
        }

        private void NewUserEvent(object sender, RoutedEventArgs e)
        {
            NewUserWindow newUserWindow = new NewUserWindow();
            newUserWindow.Show();
            this.Close();
        }

        private void EditUserEvent(object sender, RoutedEventArgs e)
        {
            //ModifyUserWindow modifyUserWindow = new ModifyUserWindow();
            //modifyUserWindow.Show();
            this.Close();
        }

        private void DeleteUserEvent(object sender, RoutedEventArgs e)
        {
            DeleteUserWindow deleteUserWindows = new DeleteUserWindow();
            deleteUserWindows.Show();
            this.Close();
        }

        private void LoginUserEvent(object sender, RoutedEventArgs e)
        {
            LoginUserWindow loginUserWindow = new LoginUserWindow();
            loginUserWindow.Show();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Esta seguro que desea salir?","Salir", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
