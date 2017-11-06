using FrameworkDB.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkView.V1
{
    public class ProviderView
    {
        GestCloudDB db;
        public Provider provider;

        public ProviderView(Provider provider)
        {
            this.provider = provider;
        }
    }
}
