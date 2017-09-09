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

namespace GestCloudv2.UserItem.NewUser
{
    /// <summary>
    /// Interaction logic for NewUser_Controller.xaml
    /// </summary>
    public partial class NewUser_Controller : Page
    {
        public Dictionary<string, int> Information;
        private Page MainContentUser;
        private Page ToolSideUser;
        private Page NavigationUser;
        public User user;

        public NewUser_Controller()
        {
            InitializeComponent();
            Information = new Dictionary<string, int>();
            Information.Add("mode", 0);
            Information.Add("fieldEmpty", 0);
            Information.Add("controller", 0);
            user = new User();

            this.Loaded += new RoutedEventHandler(StartNewUser_Event);
        }

        private void StartNewUser_Event(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void UpdateIfNotEmpty(bool empty)
        {
            if(empty)
            {
                Information["fieldEmpty"] = 1;
            }
            else
            {
                Information["fieldEmpty"] = 0;
            }
        }

        public void BackToMain()
        {
            Information["controller"] = 0;
            ChangeComponents();
        }

        public void SaveNewUser()
        {
            GestCloudDB db = new GestCloudDB();
            MessageBox.Show("Datos guardados correctamente");
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    NavigationUser = new UserItem.NewUser_Navigation();
                    MainContentUser = new UserItem.NewUser_MainPage();
                    ToolSideUser = new UserItem.NewUser_ToolSide();
                    ChangeEnviroment();
                    break;
            }
        }

        private void ChangeComponents()
        {
            switch (Information["controller"])
            {
                case 0:
                    if (Information["fieldEmpty"] == 1)
                    {
                        MessageBoxResult result = MessageBox.Show("Usted ha realizado cambios, ¿Esta seguro que desea salir?", "Volver", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                    MainWindow a = (MainWindow)Application.Current.MainWindow;
                    a.MainPage.Content = new Main.Main_Controller();
                    break;
            }
        }

        private void ChangeEnviroment()
        {
            TopSide.Content = NavigationUser;
            MainContent.Content = MainContentUser;
            LeftSide.Content = ToolSideUser;
        }

        public void ControlFieldChangeButton(bool verificated)
        {
            var a = (NewUser_ToolSide)LeftSide.Content;
            a.EnableButtonSaveUser(verificated);
        }
    }
}
