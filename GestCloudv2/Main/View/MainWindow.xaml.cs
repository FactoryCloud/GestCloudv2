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
using Microsoft.EntityFrameworkCore;
using FrameworkDB.V1;

namespace GestCloudv2.Main.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GestCloudDB db;
        //UserList_ToolSide toolSide;
        //UserList_MainContent mainContent;
        public Dictionary<string, string> Information;
        public User user;
        public List<UserPermission> UserPermissions;

        public MainWindow(User user)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            Application.Current.MainWindow = this;
            db = new GestCloudDB();

            UserPermissions = new List<UserPermission>();
            Information = new Dictionary<string, string>();
            this.user = user;

            UserPermissions = db.UserPermissions.Where(u => u.user == user)
                .Include(u => u.user).Include(u => u.userType).Include(u => u.permissionType).ToList();

            MainFrame.Content = new Main.Controller.CT_Main();
        }

        /*public void ChangeContent(Page mainPage, Page navigationPage, Page toolPage)
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
        }*/

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
