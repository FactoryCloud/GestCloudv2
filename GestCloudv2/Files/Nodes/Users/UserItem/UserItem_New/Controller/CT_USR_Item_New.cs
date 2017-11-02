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
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace GestCloudv2.Files.Nodes.Users.UserItem.UserItem_New.Controller
{
    /// <summary>
    /// Interaction logic for CT_USR_Item_New.xaml
    /// </summary>
    public partial class CT_USR_Item_New : Main.Controller.CT_Common
    {
        public User user;

        public CT_USR_Item_New()
        {
            user = new User();
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
            Information.Add("fieldEmpty", 0);
        }

        public void UpdateIfNotEmpty(bool empty)
        {
            if (empty)
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

        public void CT_Menu()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        private void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.NV_USR_Item_New();
                    TS_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.TS_USR_Item_New(); ;
                    MC_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.MC_USER_Item_New(); ;
                    ChangeComponents();
                    break;

                case 2:
                    ChangeComponents();
                    break;

                case 3:
                    ChangeComponents();
                    break;

                case 4:
                    ChangeComponents();
                    break;
            }
        }

        private void ChangeController()
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
                    Main.View.MainWindow a = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    a.MainFrame.Content = new Files.Nodes.Users.UserMenu.Controller.CT_UserMenu();
                    break;

                case 1:
                    /*MainWindow b = (MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new Main.Controller.MainController();*/
                    break;
            }
        }

        public void ControlFieldChangeButton(bool verificated)
        {
            var a = (Files.Nodes.Users.UserItem.UserItem_New.View.TS_USR_Item_New)LeftSide.Content;
            a.EnableButtonSaveUser(verificated);
        }
    }
}