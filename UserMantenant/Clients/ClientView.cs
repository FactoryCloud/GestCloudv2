﻿using FrameworkDB.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkView.V1
{
    public class ClientView
    {
        GestCloudDB db;
        public Client client;

        public ClientView(Client client)
        {
            this.client = client;
        }
    }
}
