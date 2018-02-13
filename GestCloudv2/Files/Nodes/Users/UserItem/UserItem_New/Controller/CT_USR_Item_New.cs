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
        public UserType userType;
        public Configuration ConfigSelected;
        Dictionary<int, int> Configurations;

        public CT_USR_Item_New()
        {
            user = new User();
            Information.Add("minimalInformation", 0);
            Configurations = new Dictionary<int, int>();
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            List<Configuration> AllConfigurations = db.Configurations.ToList();
            foreach (Configuration item in AllConfigurations)
            {
                Configurations.Add(item.ConfigurationID, -1);
            }
            UpdateComponents();
        }

        public List<User> GetUsers()
        {
            return db.Users.OrderBy(u => u.Code).ToList();
        }

        public List<UserType> GetUserTypes()
        {
            return db.UserTypes.ToList();
        }

        public Configuration GetConfiguration()
        {
            return db.Configurations.Where(c => c.ConfigurationID == ConfigSelected.ConfigurationID).First();
        }

        public int GetConfigurationValue()
        {
            if (Configurations[ConfigSelected.ConfigurationID] != -1)
                return Configurations[ConfigSelected.ConfigurationID];

            else
                return ConfigSelected.DefaultValue;
        }

        public void SetUserCode(int num)
        {
            user.Code = num;
            TestMinimalInformation();
        }

        public void SetUserType(int num)
        {
            userType = db.UserTypes.Where(c => c.UserTypeID == num).Include(c => c.UserPermissions).First();
            TestMinimalInformation();
        }

        public void SetConfiguration(int num)
        {
            ConfigSelected = db.Configurations.Where(c => c.ConfigurationID == num).First();
        }

        public void SetConfigValue(int num)
        {
            Configurations[ConfigSelected.ConfigurationID] = num;
        }

        public override void EV_ActivateSaveButton(bool verificated)
        {
            if(verificated)
            {
                Information["entityValid"] = 1;
            }

            else
            {
                Information["entityValid"] = 0;
            }

            TestMinimalInformation();
        }

        public void CleanUsername()
        {
            user.Username = "";
            TestMinimalInformation();
        }

        public Boolean UserControlExist(string username)
        {
            List<User> users = db.Users.ToList();
            foreach (var item in users)
            {
                if (item.Username == username || username.Length == 0)
                {
                    CleanUsername();
                    return true;
                }
            }
            user.Username = username;
            TestMinimalInformation();
            return false;
        }

        private void TestMinimalInformation()
        {
            if(user.Username.Length > 0 && userType != null && user.Code > 0 && userType != null && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            TS_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.TS_USR_Item_New(Information["minimalInformation"]);
            LeftSide.Content = TS_Page;
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
            if (Information["entityLoaded"] == 2)
            {
                if (db.Entities.ToList().Count > 0)
                {
                    entity.Cod = db.Entities.OrderBy(u => u.Cod).Last().Cod + 1;
                }
                else
                {
                    entity.Cod = 1;
                }

                db.Entities.Add(entity);
            }

            user.entity = entity;

            user.userType = userType;
            user.ActivationCode = user.Code.ToString() + GetUniqueKey(5).ToString();
            user.Enabled = 1;
            db.Users.Add(user);

            List<Configuration> AllConfigurations = db.Configurations.ToList();

            foreach (Configuration item in AllConfigurations)
            {
                db.ConfigurationsUsers.Add(new ConfigurationUser
                {
                    UserID = user.UserID,
                    ConfigurationID = item.ConfigurationID,
                    Value = Configurations[item.ConfigurationID],
                });
            }

            db.SaveChanges();
            MessageBox.Show("Datos guardados correctamente");

            Information["fieldEmpty"] = 0;
            CT_Menu();
        }

        override public void MD_EntityNew()
        {
            Information["entityLoaded"] = 2;
            MD_Change(3,0);
        }

        override public void MD_EntityLoad()
        {
            View.FW_USR_Item_New_Entity floatWindow = new View.FW_USR_Item_New_Entity(4);
            floatWindow.Show();
        }

        public override void MD_EntityLoaded()
        {
            MD_Change(4,0);
        }

        public void CT_Menu()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        override public void UpdateComponents()
        {
            if (Information["entityLoaded"] == 1 && Information["mode"] == 2)
                Information["mode"] = 4;

            if(Information["entityLoaded"] == 2 && Information["mode"] == 2)
                Information["mode"] = 3;

            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.NV_USR_Item_New();
                    TS_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.TS_USR_Item_New(Information["minimalInformation"]); ;
                    MC_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.MC_USR_Item_New_User(); ;
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.NV_USR_Item_New();
                    TS_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.TS_USR_Item_New(Information["minimalInformation"]); ;
                    MC_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.MC_USR_Item_New_Entity_Select(); ;
                    ChangeComponents();
                    break;

                case 3:
                    NV_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.NV_USR_Item_New();
                    TS_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.TS_USR_Item_New(Information["minimalInformation"]); ;
                    MC_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.MC_USR_Item_New_Entity_New(); ;
                    ChangeComponents();
                    break;

                case 4:
                    NV_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.NV_USR_Item_New();
                    TS_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.TS_USR_Item_New(Information["minimalInformation"]); ;
                    MC_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.MC_USR_Item_New_Entity_Loaded(); ;
                    ChangeComponents();
                    break;

                case 5:
                    NV_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.NV_USR_Item_New();
                    TS_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.TS_USR_Item_New(Information["minimalInformation"]); ;
                    MC_Page = new Files.Nodes.Users.UserItem.UserItem_New.View.MC_USR_Item_New_Configuration(); ;
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
            TestMinimalInformation();
        }
    }
}