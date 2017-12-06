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

        public CT_Submenu(object Component, int option)
        {
            switch (option)
            {
                case 4:
                    Subcontroller = new Files.Nodes.Stores.StoreItem.StoreItem_Load.Controller.CT_STR_Item_Load((Store)Component, 0, 1);
                    items = new SubmenuItems().GetSubmenuItems(option);
                    buttons = items.Count;
                    break;

                case 6:
                    Subcontroller = new Files.Nodes.Clients.ClientItem.ClientItem_Load.Controller.CT_CLI_Item_Load((Client)Component, 0, 1);
                    items = new SubmenuItems().GetSubmenuItems(option);
                    buttons = items.Count;
                    break;
            }
        }
    }
}
