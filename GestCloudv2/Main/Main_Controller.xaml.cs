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
        private Page NavigationDesktop;
        private Page MainContentDesktop;
        private Page ToolSideDesktop;
        Dictionary<string, int> Information;//Guarda el estado de la aplicacion, para controlar los permisos del usuario
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
            Information.Add("controller", 0);
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
            Information["controller"] = 1;
            this.user = user;
            ChangeComponents();
        }

        public void StartNewUser()
        {
            Information["controller"] = 2;
            ChangeComponents();
        }


        //Prepara los componentes para que los cargue ChangeEnviroment
        private void UpdateComponents ()
        {
            switch(Information["mode"])
            {
                case 0:
                    NavigationDesktop = new Main_Navigation();
                    MainContentDesktop = null;
                    ToolSideDesktop = null;
                    ChangeEnviroment();
                    break;

                case 1:
                    NavigationDesktop = new Main_Navigation();
                    MainContentDesktop = new UserList_MainContent();
                    ToolSideDesktop = new UserList_ToolSide();
                    ChangeEnviroment();
                    break;
            }
        }


        //Actualiza los componentes de la ventana
        private void ChangeEnviroment()
        {
            TopSide.Content = NavigationDesktop;
            MainContent.Content = MainContentDesktop;
            LeftSide.Content= ToolSideDesktop;
        }


        //A quien cedo el control
        private void ChangeComponents()
        {
            switch (Information["controller"])
            {
                case 1:
                    MainWindow a = (MainWindow)Application.Current.MainWindow;
                    a.MainPage.Content = new UserItem.InfoUser.InfoUser_Controller(user, false);
                    break;

                case 2:
                    MainWindow b = (MainWindow)Application.Current.MainWindow;
                    b.MainPage.Content = new UserItem.NewUser.NewUser_Controller();
                    break;
            }
        }
    }
}
