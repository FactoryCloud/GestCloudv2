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

namespace GestCloudv2.Main.Controller
{
    /// <summary>
    /// Interaction logic for CT_Common.xaml
    /// </summary>
    public partial class CT_Common : Page
    {
        protected Page NV_Page;
        protected Page TS_Page;
        protected Page MC_Page;
        protected Page SC_Page;
        protected Page SB_Page;
        protected Frame LeftSide;
        protected Frame MainContent;
        protected Frame RightSide;
        public Entity entity;
        public Model.CT_Submenu CT_Submenu;
        public City citySelected;
        public int postalCodeSelected;
        public List<City> cities;
        public Country countrySelected;

        protected GestCloudDB db;

        public Dictionary<string, int> Information;

        public CT_Common()
        {
            InitializeComponent();
            FR_Subcontent.Content = new CT_Common_Subcontent();
            db = new GestCloudDB();
            cities = new List<City>();
            entity = new Entity();
            countrySelected = GetCountries().OrderBy(t => t.CountryID).First();
            citySelected = GetCities(Convert.ToInt32(entity.CityID)).OrderBy(t => t.CityID).First();
 
        

            entity.Name = "";

            Information = new Dictionary<string, int>();
            Information.Add("mode", 1);
            Information.Add("oldmode", 1);
            Information.Add("controller", 0);
            Information.Add("oldcontroller", 0);
            Information.Add("fieldEmpty", 0);
            Information.Add("entityValid", 0);
            Information.Add("entityLoaded", 0);
            Information.Add("submenu", 0);
            Information.Add("submode", 0);
            Information.Add("externalActivated", 0);

            this.Loaded += new RoutedEventHandler(EV_PreStart);
        }

        public CT_Common(int external):base()
        {
            InitializeComponent();
            FR_Subcontent.Content = new CT_Common_Subcontent();

            db = new GestCloudDB();
            entity = new Entity();

            entity.Name = "";

            Information = new Dictionary<string, int>();
            Information.Add("mode", 1);
            Information.Add("oldmode", 1);
            Information.Add("controller", 0);
            Information.Add("oldcontroller", 0);
            Information.Add("fieldEmpty", 0);
            Information.Add("entityValid", 0);
            Information.Add("entityLoaded", 0);
            Information.Add("submenu", 0);
            Information.Add("submode", 0);
            Information.Add("externalActivated", 0);

            this.Loaded += new RoutedEventHandler(EV_PreStart);
            this.Loaded += new RoutedEventHandler(EV_PreStartNoNavigation);
        }

        public virtual void EV_PurchaseOrderAdd(int purchaseOrder)
        {
        }

        public virtual void EV_PurchaseDeliveryAdd(int purchaseDelivery)
        {
        }

        public virtual void EV_SaleOrderAdd(int purchaseOrder)
        {
        }

        public virtual void EV_SaleDeliveryAdd(int purchaseDelivery)
        {
        }

        public virtual void SetMC(int i)
        {

        }

        public virtual void SetTS()
        {

        }

        public virtual void SetNV()
        {

        }

        public virtual void SetSC()
        {

        }

        public virtual void SetSB()
        {

        }

        public virtual void SetSubmenu(int option)
        {
            switch (option)
            {
                case 4:
                    CT_Submenu = new Model.CT_Submenu(GetStore(), option);
                    break;

                case 6:
                    CT_Submenu = new Model.CT_Submenu(GetClient(), option);
                    break;

                case 7:
                    CT_Submenu = new Model.CT_Submenu(GetProvider(), option);
                    break;
            }

            SetNV();
            TopSide.Content = NV_Page;

            EV_UpdateInfoCard();
        }

        virtual public Store GetStore()
        {
            return new Store();
        }

        virtual public Provider GetProvider()
        {
            return new Provider();
        }

        virtual public Client GetClient()
        {
            return new Client();
        }

        virtual public List<Company> GetCompanies()
        {
            return db.Companies.ToList();
        }

        virtual public List<Store> GetStores()
        {
            List<Store> stores = new List<Store>();
            List<CompanyStore> companyStores = db.CompaniesStores.Where(c => c.CompanyID == ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID).Include(z => z.store).ToList();
            foreach (CompanyStore e in companyStores)
            {
                stores.Add(e.store);
            }
            return stores;
        }

        virtual public List<User> GetUsers()
        {
            return db.Users.ToList();
        }

        virtual public List<FiscalYear> GetFiscalYears()
        {
            return db.FiscalYears.ToList();
        }

        virtual public List<City> GetCities(int num)
        {
            return db.Cities.Where(i => i.P_C.Contains(num.ToString()) && i.CountryID == countrySelected.CountryID).ToList();
        }

        virtual public List<Country> GetCountries()
        {
            return db.Countries.ToList();
        }

        virtual public List<ConfigurationType> GetConfigurationTypes()
        {
            return db.ConfigurationTypes.ToList();
        }

        virtual public List<Configuration> GetConfigurations(int num)
        {
            if (num == 0)
                return db.Configurations.ToList();

            else
                return db.Configurations.Where(c => c.ConfigurationTypeID == num).ToList();
        }

        public virtual void SetEntity(int num)
        {
            entity = db.Entities.Where(e => e.EntityID == num).First();
            Information["entityLoaded"] = 1;
        }

        public void SetPostalCode(string num)
        {
            if (num.Length == 5)
            {
                postalCodeSelected = Convert.ToInt32(num);
                cities = GetCities(postalCodeSelected);
            }
            else
            {
                cities = new List<City>();
                postalCodeSelected = 0;
                citySelected = null;
            }
        }

        public void SetCitySelected(int num)
        {
            citySelected = db.Cities.Where(t => t.CityID == num).First();
        }

        public void SetCountrySelected(int num)
        {
            countrySelected = db.Countries.Where(t => t.CountryID == num).First();
        }

        public void EV_UpdateSubMenu(int num)
        {
            if (num == 0)
            {
                Information["submenu"] = 0;
                Information["submode"] = 0;
                CT_Submenu = null;
            }

            else
            {
                Information["submenu"] = 1;
                Information["submode"] = num;
                SetSubmenu(num);
            }
        }

        public virtual void EV_UpdateShortcutDocuments(int option)
        {
            ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).shortcutDocuments.Remove(((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).shortcutDocuments.Where(sd => sd.Id == option).First());
        }

        public virtual void EV_UpdateInfoCard()
        {
            ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).EV_InfoCardUpdate(CT_Submenu.infoCardItem);
            SetSC();
            ((CT_Common_Subcontent)FR_Subcontent.Content).RightSide.Content = SC_Page;
        }

        public void MD_Submenu(int option)
        {
            Information["externalActivated"] = 1;
            CT_Submenu.Subcontroller.MD_Change(option, Information["externalActivated"]);
            FR_Subcontent.Content = CT_Submenu.Subcontroller;
        }

        public void CT_MainWindow()
        {
            Main.View.MainWindow f = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
            f.MainFrame.Content = new Main.Controller.CT_Main();
        }

        public void CT_ShortcutDocumentActivate(int option)
        {
            Shortcuts.ShortcutDocument doc = ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).shortcutDocuments.Where(d => d.Id == option).First();
            ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).shortcutDocuments.Remove(((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).shortcutDocuments.Where(sd => sd.Id == option).First());
            Main.View.MainWindow f = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
            f.MainFrame.Content = doc.Controller;
        }

        public virtual void EV_MovementAdd(Movement movement)
        {

        }

        public virtual void EV_SetProvider(int provider)
        {

        }

        public virtual void EV_SetClient(int client)
        {

        }

        virtual public void MD_EntityNew()
        {

        }

        virtual public void MD_EntityLoad()
        {

        }

        virtual public void MD_EntityLoaded()
        {

        }

        virtual public void MD_EntityEdit()
        {

        }

        public virtual void EV_UserSelected()
        {

        }

        public void MD_Change(int i, int external)
        {
            Information["oldmode"] = Information["mode"];
            Information["mode"] = i;

            if (Information["submenu"] == 1)
            {
                Information["submenu"] = 0;
                Information["submode"] = 0;
                TopSide.Content = NV_Page;
            }
                

            if (Information["externalActivated"] == 1)
                EV_SubcontentClear();

            else
                UpdateComponents();
        }

        protected void EV_SubcontentClear()
        {
            FR_Subcontent.Content = new CT_Common_Subcontent();
            CT_Submenu = null;
            Information["submenu"] = 0;
            Information["submode"] = 0;
            Information["externalActivated"] = 0;
            FR_Subcontent.LoadCompleted += new LoadCompletedEventHandler(EV_SubcontentLoaded);
        }

        public void EV_SubcontentLoaded(object sender, EventArgs e)
        {
            LeftSide = ((CT_Common_Subcontent)FR_Subcontent.Content).LeftSide;
            MainContent = ((CT_Common_Subcontent)FR_Subcontent.Content).MainContent;
            RightSide = ((CT_Common_Subcontent)FR_Subcontent.Content).RightSide;
            FR_Subcontent.LoadCompleted -= EV_SubcontentLoaded;
            MD_Change(Information["mode"], 0);
        }

        protected void ChangeComponents()
        {
            TopSide.Content = NV_Page;
            BotSide.Content = SB_Page;
            ((CT_Common_Subcontent)FR_Subcontent.Content).LeftSide.Content = TS_Page;
            ((CT_Common_Subcontent)FR_Subcontent.Content).MainContent.Content = MC_Page;
            ((CT_Common_Subcontent)FR_Subcontent.Content).RightSide.Content = SC_Page;
        }

        virtual public void UpdateComponents()
        {

        }

        public void EV_PreStartNoNavigation(object sender, RoutedEventArgs e)
        {
            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            row1.Height = new GridLength(0, GridUnitType.Star);
            row2.Height = new GridLength(4, GridUnitType.Star);
            GR_Content.RowDefinitions.Remove(GR_Content.RowDefinitions.Last());
            GR_Content.RowDefinitions.Remove(GR_Content.RowDefinitions.Last());
            GR_Content.RowDefinitions.Add(row1);
            GR_Content.RowDefinitions.Add(row2);
        }

        public void EV_PreStart(object sender, RoutedEventArgs e)
        {
            LeftSide = ((CT_Common_Subcontent)FR_Subcontent.Content).LeftSide;
            MainContent = ((CT_Common_Subcontent)FR_Subcontent.Content).MainContent;
            RightSide = ((CT_Common_Subcontent)FR_Subcontent.Content).RightSide;
            EV_Start(sender, e);
        }

        virtual public void EV_Start(object sender, RoutedEventArgs e)
        {
        }

        virtual public void EV_ActivateSaveButton(bool verificated)
        {
            
        }

        virtual public void EV_EntityChange()
        {

        }

        public void EV_UpdateIfNotEmpty(bool empty)
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
    }
}
