using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkDB.V1;
using FrameworkView.V1;
using GestCloudv2.Main.Controller;

namespace GestCloudv2.Model
{
    public class CT_Submenu
    {
        public List<SubmenuItem> items;
        public int buttons;
        public CT_Common Subcontroller;
        public string Name;

        public CT_Submenu(object Component, int option)
        {
            switch (option)
            {
                case 4:
                    Subcontroller = new Files.Nodes.Stores.StoreItem.StoreItem_Load.Controller.CT_STR_Item_Load((Store)Component, 0, 1);
                    items = new SubmenuItems().GetSubmenuItems(option);
                    buttons = items.Count;
                    Name = "Almacén";
                    break;

                case 6:
                    Subcontroller = new Files.Nodes.Clients.ClientItem.ClientItem_Load.Controller.CT_CLI_Item_Load((Client)Component, 0, 1);
                    items = new SubmenuItems().GetSubmenuItems(option);
                    buttons = items.Count;
                    Name = "Cliente";
                    break;

                case 7:
                    Subcontroller = new Files.Nodes.Providers.ProviderItem.ProviderItem_Load.Controller.CT_PRO_Item_Load((Provider)Component, 0, 1);
                    items = new SubmenuItems().GetSubmenuItems(option);
                    buttons = items.Count;
                    Name = "Proveedor";
                    break;
            }
        }
    }
}
