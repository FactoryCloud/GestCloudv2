using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkView.V1
{
    public class SubmenuItems
    {
        public List<SubmenuItem> GetSubmenuItems (int option)
        {
            switch(option)
            {
                case 4:
                    return new List<SubmenuItem>
                    {
                        new SubmenuItem
                        {
                            Name = "BT_Store",
                            Content = "Datos Almacén",
                            Option = 1
                        },
                        new SubmenuItem
                        {
                            Name = "BT_Companies",
                            Content = "Compañias",
                            Option = 2
                        }
                    };

                case 6:
                    return new List<SubmenuItem>
                    {
                        new SubmenuItem
                        {
                            Name = "BT_Client",
                            Content = "Datos Cliente",
                            Option = 1
                        },
                        new SubmenuItem
                        {
                            Name = "BT_Entity",
                            Content = "Datos Personales",
                            Option = 2
                        }
                    };
            }

            return null;
        }
    }
}
