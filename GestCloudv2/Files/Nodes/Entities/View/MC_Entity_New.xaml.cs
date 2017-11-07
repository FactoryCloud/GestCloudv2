using FrameworkDB.V1;
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

namespace GestCloudv2.Files.Nodes.Entities.View
{
    /// <summary>
    /// Interaction logic for MC_Entity_New.xaml
    /// </summary>
    public partial class MC_Entity_New : Page
    {
        public MC_Entity_New()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);

            TB_Entity_Name.Text = GetController().entity.Name;
            TB_Entity_SubName.Text = GetController().entity.Subname;
            TB_Entity_Phone.Text = GetController().entity.Phone1;
            TB_Entity_NIF.Text = GetController().entity.NIF;
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_Entity_Name.KeyUp += new KeyEventHandler(EV_Keys);
            TB_Entity_SubName.KeyUp += new KeyEventHandler(EV_Keys);
            TB_Entity_Phone.KeyUp += new KeyEventHandler(EV_Keys);
            TB_Entity_NIF.KeyUp += new KeyEventHandler(EV_Keys);
        }

        public void EV_Keys(object sender, RoutedEventArgs e)
        {
            GetController().entity = new Entity
            {
                Name = TB_Entity_Name.Text.ToString(),
                Subname = TB_Entity_SubName.Text.ToString(),
                Phone1 = TB_Entity_Phone.Text.ToString(),
                NIF = TB_Entity_NIF.Text.ToString()
            };

            if (TB_Entity_Name.Text.Length <= 30 && TB_Entity_SubName.Text.Length <= 30 && TB_Entity_Phone.Text.Length <= 20 && TB_Entity_NIF.Text.Length <= 50 && TB_Entity_Name.Text.Length > 0 && TB_Entity_SubName.Text.Length > 0 && TB_Entity_Phone.Text.Length > 0 && TB_Entity_NIF.Text.Length > 0)
            {
                GetController().EV_ActivateSaveButton(true);
            }
            else
            {
                GetController().EV_ActivateSaveButton(false);
            }

            if (!string.IsNullOrEmpty(TB_Entity_Name.Text.ToString()) || !string.IsNullOrEmpty(TB_Entity_SubName.Text.ToString()) || !string.IsNullOrEmpty(TB_Entity_Phone.Text.ToString()) || !string.IsNullOrEmpty(TB_Entity_NIF.Text.ToString()))
            {
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else
            {
                GetController().EV_UpdateIfNotEmpty(false);
            }
        }

        virtual public Main.Controller.CT_Common GetController()
        {
            return new Main.Controller.CT_Common();
        }
    }
}
