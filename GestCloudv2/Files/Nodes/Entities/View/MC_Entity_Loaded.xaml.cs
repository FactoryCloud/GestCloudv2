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
    /// Interaction logic for MC_Entity_Loaded.xaml
    /// </summary>
    public partial class MC_Entity_Loaded : Page
    {
        public MC_Entity_Loaded()
        { 
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_Entity_Name.Text = GetController().entity.Name;
            TB_Entity_SubName.Text = GetController().entity.Subname;
            TB_Entity_Phone1.Text = GetController().entity.Phone1;
            TB_Entity_Phone2.Text = GetController().entity.Phone2;
            TB_Entity_Mobile.Text = GetController().entity.Mobile;
            TB_Entity_Contact.Text = GetController().entity.Contact;
            TB_Entity_NIF.Text = GetController().entity.NIF;
            TB_Entity_Address.Text = GetController().entity.Address;
            TB_Entity_Email.Text = GetController().entity.Email;

            GetController().EV_ActivateSaveButton(true);
        }

        virtual public Main.Controller.CT_Common GetController()
        {
            return new Main.Controller.CT_Common();
        }
    }
}
