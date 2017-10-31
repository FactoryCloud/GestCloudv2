using FrameworkDB.V1;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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
        GestCloudDB db;
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

        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public void BackToMain()
        {
            Information["controller"] = 0;
            ChangeComponents();
        }

        public void SaveNewUser()
        {
            GestCloudDB db = new GestCloudDB();
            db.Users.Add(user);
            db.SaveChanges();
            user = db.Users.First(u => u.Username == user.Username);
            user.ActivationCode = user.UserID.ToString() + GetUniqueKey(5).ToString();
            db.Users.Update(user);
            db.SaveChanges();
            MessageBox.Show("Datos guardados correctamente");
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
                    Main.View.MainWindow a = (Main.View.MainWindow)Application.Current.MainWindow;
                    a.MainFrame.Content = new Main.Controller.CT_Main();
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
