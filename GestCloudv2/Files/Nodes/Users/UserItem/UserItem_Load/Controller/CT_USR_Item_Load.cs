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
using FrameworkView.V1;

namespace GestCloudv2.Files.Nodes.Users.UserItem.UserItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_USR_Item_Load.xaml
    /// </summary>
    public partial class CT_USR_Item_Load : Main.Controller.CT_Common
    {
        public User user;
        public Configuration ConfigSelected;
        //public ConfigurationUser configurationUser;
        Dictionary<int, int> Configurations;

        public CT_USR_Item_Load(User user, int editable)
        {
            Information.Add("editable", editable);
            Information.Add("old_editable", 0);
            Information.Add("minimalInformation", 0);

            this.user = user;

            Information["entityValid"] = 1;
            Configurations = new Dictionary<int, int>();
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            entity = db.Entities.Where(u => u.EntityID == user.EntityID).First();
            MessageBox.Show($"{entity.EntityID}");

            List<Configuration> AllConfigurations = db.Configurations.ToList();
            List<ConfigurationUser> UserConfigurations = db.ConfigurationsUsers.Where(c => c.UserID == user.UserID).ToList();

            foreach(Configuration item in AllConfigurations)
            {
                if (UserConfigurations.Where(u => u.ConfigurationID == item.ConfigurationID).ToList().Count > 0)
                    Configurations.Add(item.ConfigurationID, UserConfigurations.Where(u => u.ConfigurationID == item.ConfigurationID).First().Value);

                else
                    Configurations.Add(item.ConfigurationID, -1);
            }

            UpdateComponents();
        }

        public List<UserType> GetUserTypes()
        {
            return db.UserTypes.ToList();
        }

        public Configuration GetConfiguration()
        {
            return db.Configurations.Where(c => c.ConfigurationID == ConfigSelected.ConfigurationID).First();
        }

        public int GetDefaultConfigurationValue()
        {
                return ConfigSelected.DefaultValue;
        }

        public int GetConfigurationValue()
        {
            if (Configurations[ConfigSelected.ConfigurationID] != -1)
                return Configurations[ConfigSelected.ConfigurationID];
                //return configurationUser.Value;

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
            user.UserTypeID = db.UserTypes.Where(c => c.UserTypeID == num).Include(c => c.UserPermissions).First().UserTypeID;
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

        public void SetDefaultConfig(int num)
        {
            ConfigSelected = db.Configurations.Where(c => c.ConfigurationID == ConfigSelected.ConfigurationID).First();
        }

        /*public void SetDefaultConfig()
        {
            MessageBoxResult result = MessageBox.Show("¿Esta seguro que desea restablecer los valores por defecto?", "Restablecer configuración", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                List<Configuration> AllConfigurations = db.Configurations.ToList();
                List<ConfigurationUser> UserConfigurations = db.ConfigurationsUsers.Where(c => c.UserID == user.UserID).ToList();

                foreach (Configuration item in AllConfigurations)
                {
                    ConfigurationUser temp = db.ConfigurationsUsers.Where(c => c.ConfigurationID == item.ConfigurationID && c.UserID == user.UserID).First();
                    temp.Value = item.DefaultValue;
                    db.ConfigurationsUsers.Update(temp);
                }

                db.SaveChanges();
            }
        }*/

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
                if ((item.Username == username && user.Username != username) || username.Length == 0)
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
            if(user.Username.Length > 0 && user.userType != null && user.Code > 0 && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            if (Information["editable"] != 0)
            {
                TS_Page = new View.TS_USR_Item_Load_Editable(Information["minimalInformation"]);
                LeftSide.Content = TS_Page;
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
            if (Information["entityLoaded"] == 2)
            {
                db.Entities.Update(entity);
            }
            
            User user1 = db.Users.Where(u => u.UserID == user.UserID).First();
            user1.Username = user.Username;
            user1.UserTypeID = user.UserTypeID;
            user1.Code = user.Code;
            user1.EntityID = entity.EntityID;

            db.Users.Update(user1);

            List<Configuration> AllConfigurations = db.Configurations.ToList();
            List<ConfigurationUser> UserConfigurations = db.ConfigurationsUsers.Where(c => c.UserID == user.UserID).ToList();

            foreach (Configuration item in AllConfigurations)
            {
                if (UserConfigurations.Where(u => u.ConfigurationID == item.ConfigurationID).ToList().Count > 0)
                {
                    ConfigurationUser temp = db.ConfigurationsUsers.Where(c => c.ConfigurationID == item.ConfigurationID && c.UserID == user.UserID).First();
                    temp.Value = Configurations[item.ConfigurationID];
                    db.ConfigurationsUsers.Update(temp);
                }

                else
                {
                    if (Configurations[item.ConfigurationID] >= 0)
                        db.ConfigurationsUsers.Add(new ConfigurationUser
                        {
                            UserID = user.UserID,
                            ConfigurationID = item.ConfigurationID,
                            Value = Configurations[item.ConfigurationID],
                        });
                }
            }

            db.SaveChanges();
            MessageBox.Show("Datos guardados correctamente");

            Information["fieldEmpty"] = 0;
            CT_Menu();
        }

        override public void MD_EntityEdit()
        {
            Information["entityLoaded"] = 2;
            MD_Change(3,0);
        }

        override public void MD_EntityLoad()
        {
            View.FW_USR_Item_Load_Entity floatWindow = new View.FW_USR_Item_Load_Entity(4);
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
                    NV_Page = new View.NV_USR_Item_Load();
                    if(Information["editable"] == 0)
                        TS_Page = new View.TS_USR_Item_Load(Information["minimalInformation"]);
                    else
                        TS_Page = new View.TS_USR_Item_Load_Editable(Information["minimalInformation"]);
                    MC_Page = new View.MC_USR_Item_Load_User();
                    ChangeComponents();
                    break;

                case 2:
                    if (Information["editable"] == 0)
                    {
                        Information["mode"] = 4;
                        UpdateComponents();
                        break;
                    }

                    else
                    {
                        NV_Page = new View.NV_USR_Item_Load();
                        TS_Page = new View.TS_USR_Item_Load_Editable(Information["minimalInformation"]);
                        MC_Page = new View.MC_USR_Item_Load_Entity_Select();
                    }
                    ChangeComponents();
                    break;

                case 3:
                    NV_Page = new View.NV_USR_Item_Load();
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_USR_Item_Load(Information["minimalInformation"]);
                    else
                        TS_Page = new View.TS_USR_Item_Load_Editable(Information["minimalInformation"]);
                    MC_Page = new View.MC_USR_Item_Load_Entity_Edit();
                    ChangeComponents();
                    break;

                case 4:
                    NV_Page = new View.NV_USR_Item_Load();
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_USR_Item_Load(Information["minimalInformation"]);
                    else
                        TS_Page = new View.TS_USR_Item_Load_Editable(Information["minimalInformation"]);
                    MC_Page = new View.MC_USR_Item_Load_Entity_Loaded();
                    ChangeComponents();
                    break;

                case 6:
                    NV_Page = new View.NV_USR_Item_Load();
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_USR_Item_Load(Information["minimalInformation"]);
                    else
                        TS_Page = new View.TS_USR_Item_Load_Editable(Information["minimalInformation"]);
                    MC_Page = new View.MC_USR_Item_Load_Configuration();
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