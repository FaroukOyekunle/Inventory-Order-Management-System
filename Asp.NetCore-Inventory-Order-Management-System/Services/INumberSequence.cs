﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_Inventory_Order_Management_System.Services
{
    public interface INumberSequence
    {
        string GetNumberSequence(string module);
    }
}
