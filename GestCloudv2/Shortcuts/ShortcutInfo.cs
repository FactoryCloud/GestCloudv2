using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestCloudv2.Shortcuts
{
    public class ShortcutInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Main.Controller.CT_Common Controller { get; set; }
    }
}