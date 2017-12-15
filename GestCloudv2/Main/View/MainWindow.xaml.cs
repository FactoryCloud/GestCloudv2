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
        public Dictionary<string, string> Information;
        public User selectedUser;
        public List<Shortcuts.ShortcutDocument> shortcutDocuments;
        public Company selectedCompany;
        public List<UserPermission> userPermissions;

        public MainWindow(User user)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            Application.Current.MainWindow = this;
            db = new GestCloudDB();

            shortcutDocuments = new List<Shortcuts.ShortcutDocument>();
            userPermissions = new List<UserPermission>();
            Information = new Dictionary<string, string>();
            this.selectedUser = user;
            userPermissions = db.UserPermissions.Where(u => u.user == user)
                .Include(u => u.user).Include(u => u.userType).Include(u => u.permissionType).ToList();
            selectedCompany = db.Companies.First();

            GR_Company.MouseLeftButtonDown += new MouseButtonEventHandler(EV_CompanyMouseClick);

            MainFrame.Content = new Main.Controller.CT_Main();

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        public void EV_Start(object sender, RoutedEventArgs e)
        {
            LB_Company.Content = $"Empresa: {selectedCompany.Code} - {selectedCompany.Name}";
            LB_User.Content = $"Usuario: {selectedUser.Code} - {selectedUser.entity.Name}, {selectedUser.entity.Subname}";

            List<Company> companies = db.Companies.OrderBy(u => u.Code).ToList();
            foreach(Company c in companies)
            {
                if (c.CompanyID != selectedCompany.CompanyID)
                {
                    Button button = new Button();
                    StackPanel panel = new StackPanel();
                    Label label = new Label();
                    label.Content = $"{c.Code} - {c.Name}";
                    panel.Children.Add(label);
                    button.Content = panel;
                    button.Width = BT_Company.ActualWidth;

                    SP_Company.Children.Add(button);
                }
            }

            List<User> users = db.Users.OrderBy(u => u.Code).ToList();
            foreach (User u in users)
            {
                if (u.UserID != selectedUser.UserID)
                {
                    Button button = new Button();
                    StackPanel panel = new StackPanel();
                    Label label = new Label();
                    label.Content = $"{u.Code} - {u.Username}";
                    panel.Children.Add(label);
                    button.Content = panel;
                    button.Width = BT_User.ActualWidth;

                    SP_User.Children.Add(button);
                }
            }
        }

        private void EV_CompanyMouseClick(object sender, RoutedEventArgs e)
        {
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
