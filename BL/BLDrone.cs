using IBL.BO;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : IBL.IBL
    {
        private double BatteryRequired(double distance, WieghtCategories weight)
        {
            return distance / PowerConsumption[(int)weight];
        }
    }
}
