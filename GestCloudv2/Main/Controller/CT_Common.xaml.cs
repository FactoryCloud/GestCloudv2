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
        public Entity entity;
        //public Client client;

        protected GestCloudDB db;

        public Dictionary<string, int> Information;

        public CT_Common()
        {
            InitializeComponent();
            db = new GestCloudDB();
            //client = new Client();
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

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        public void SetEntity(int num)
        {
            entity = db.Entities.Where(e => e.EntityID == num).First();
            Information["entityLoaded"] = 1;
        }

        public virtual void EV_MovementAdd(Movement movement)
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

        public void MD_Change(int i)
        {
            Information["oldmode"] = Information["mode"];
            Information["mode"] = i;

            UpdateComponents();
        }

        protected void ChangeComponents()
        {
            TopSide.Content = NV_Page;
            LeftSide.Content = TS_Page;
            MainContent.Content = MC_Page;
        }

        virtual public void UpdateComponents()
        {

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
