using FrameworkView.V1;
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

namespace GestCloudv2.Main
{
    /// <summary>
    /// Interaction logic for Main_Controller.xaml
    /// </summary>
    public partial class Main_Controller : Page
    {
        private Main_Navigation Navigation;
        Dictionary<string, int> Information;
        User user;

        public Main_Controller()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(StartMainPage_Event);

            Information = new Dictionary<string, int>();
            Information.Add("mode", 0);
            Information.Add("old_mode", 0);
            Information.Add("selected", 0);
            Information.Add("old_selected", 0);
            Information.Add("user", 0);
        }

        private void StartMainPage_Event(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void ChangeMode(int i)
        {
            Information["old_mode"] = Information["mode"];
            Information["mode"] = i;
            UpdateComponents();
        }

        public void StartUser(User user)
        {
            Information["user"] = 1;
            this.user = user;
            ChangeComponents();
        }

        private void UpdateComponents ()
        {
            switch(Information["mode"])
            {
                case 0:
                    Navigation = new Main_Navigation();
                    TopSide.Content = Navigation;
                    break;

                case 1:
                    Navigation = new Main_Navigation();
                    TopSide.Content = Navigation;
                    MainContent.Content = new UserList_MainContent();
                    LeftSide.Content = new UserList_ToolSide();
                    break;
            }
        }

        private void ChangeComponents()
        {
            if (Information["user"] ==1)
            {
                MainWindow a = (MainWindow)Application.Current.MainWindow;
                a.MainPage.Content = new UserItem.InfoUser.InfoUser_Controller(user, false);
            }
        }
    }
}
