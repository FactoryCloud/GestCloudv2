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
using System.Windows.Controls.Primitives;

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
        public UserAccessControl uac;
        public List<UserPermission> userPermissions;

        public MainWindow(User user, UserAccessControl uac)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            Application.Current.MainWindow = this;
            db = new GestCloudDB();

            shortcutDocuments = new List<Shortcuts.ShortcutDocument>();
            userPermissions = new List<UserPermission>();
            Information = new Dictionary<string, string>();
            this.selectedUser = user;
            this.uac = uac;
            userPermissions = db.UserPermissions.Where(u => u.user == user)
                .Include(u => u.user).Include(u => u.userType).Include(u => u.permissionType).ToList();
            selectedCompany = db.Companies.Include(f => f.fiscalYear).First();

            GR_Main.PreviewMouseDown += new MouseButtonEventHandler(EV_PopupHide);

            MainFrame.Content = new Main.Controller.CT_Main();

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        public void EV_Start(object sender, RoutedEventArgs e)
        {
            InitializingCompany();
            InitializingUser();                 
        }

        private void EV_SetCompany(object sender, RoutedEventArgs e)
        {
            BT_Company.IsChecked = false;
            selectedCompany = db.Companies.Where(c => c.CompanyID == (Convert.ToInt16(((Button)sender).Tag))).First();
            InitializingCompany();
        }

        private void EV_SetUser(object sender, RoutedEventArgs e)
        {
            BT_User.IsChecked = false;
            Window FL_Password = new FloatWindows.PasswordWindow(Convert.ToInt16(((Button)sender).Tag), uac);
            FL_Password.Show();
        }

        private void EV_PopupHide(object sender, RoutedEventArgs e)
        {
            if(!BT_Company.IsMouseOver && !SP_Company.IsMouseOver)
            {
                if (PU_Company.IsOpen)
                    BT_Company.IsChecked = false;
            }

            if (!BT_User.IsMouseOver && !SP_User.IsMouseOver)
            {
                if (PU_User.IsOpen)
                    BT_User.IsChecked = false;
            }
        }

        public void InitializingCompany()
        {
            LB_Company.Content = $"Empresa: {selectedCompany.Code} - {selectedCompany.Name}";

            SP_Company.Children.Clear();

            List<Company> companies = db.Companies.OrderBy(u => u.Code).ToList();
            foreach (Company c in companies)
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
                    button.Tag = c.CompanyID;
                    button.Click += new RoutedEventHandler(EV_SetCompany);

                    SP_Company.Children.Add(button);
                }
            }
        }

        public void InitializingUser()
        {
            LB_User.Content = $"Usuario: {selectedUser.Code} - {selectedUser.entity.Name}, {selectedUser.entity.Subname}";

            SP_User.Children.Clear();

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
                    button.Tag = u.UserID;
                    button.Click += new RoutedEventHandler(EV_SetUser);

                    SP_User.Children.Add(button);
                }
            }
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
                UserAccessControl uact = db.UsersAccessControl.Where(u => u.UserAccessControlID == uac.UserAccessControlID).First();
                uact.DateEndAccess = DateTime.Now;
                db.UsersAccessControl.Update(uact);
                db.SaveChanges();

                Application.Current.Shutdown();
            }
        }
    }
}
