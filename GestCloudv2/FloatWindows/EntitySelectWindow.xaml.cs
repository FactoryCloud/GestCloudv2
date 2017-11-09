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
using System.Windows.Shapes;
using FrameworkDB.V1;
using FrameworkView.V1;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GestCloudv2.FloatWindows
{
    /// <summary>
    /// Interaction logic for EntitySelectWindow.xaml
    /// </summary>
    public partial class EntitySelectWindow : Window
    {
        public int entity;
        public EntitiesView entitiesView;

        public EntitySelectWindow()
        {

        }

        public EntitySelectWindow(int option)
        {
            InitializeComponent();

            entitiesView = new EntitiesView(option);

            DG_Entities.MouseLeftButtonUp += new MouseButtonEventHandler(EV_SelectedChange);
            DG_Entities.MouseDoubleClick += new MouseButtonEventHandler(EV_SelectEntity);

            this.Loaded += new RoutedEventHandler(EV_Start);
            this.Closed += new EventHandler(EV_Close);
        }

        public void EV_Start(object sender, RoutedEventArgs e)
        {
            List<FrameworkDB.V1.EntityType> entityTypes = entitiesView.GetEntityTypes();

            foreach (FrameworkDB.V1.EntityType ent in entityTypes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{ent.Name}";
                temp.Name = $"entity{ent.EntityTypeID}";
                CB_EntityType.Items.Add(temp);
            }

            UpdateData();
        }

        public void EV_Close(object sender, EventArgs e)
        {
            //GetController().RestartNewMovement();
        }

        protected void EV_SelectedChange(object sender, RoutedEventArgs e)
        {
            int num = DG_Entities.SelectedIndex;
            if (num >= 0)
            {
                BT_SelectEntity.IsEnabled = true;
                DataGridRow row = (DataGridRow)DG_Entities.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                entity =Int32.Parse(dr.Row.ItemArray[0].ToString());
            }
        }

        protected void EV_SelectEntity(object sender, RoutedEventArgs e)
        {
            GetController().SetEntity(entity);
            GetController().MD_EntityLoaded();

            this.Close();
        }

        protected void EV_CancelEntity(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected void EV_Search(object sender, RoutedEventArgs e)
        {
        }

        public void UpdateData()
        {
            DG_Entities.ItemsSource = null;
            DG_Entities.ItemsSource = entitiesView.GetTable();
        }

        virtual public Main.Controller.CT_Common GetController()
        {
            return new Main.Controller.CT_Common();
        }
    }
}
