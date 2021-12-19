﻿using System;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DalObject.DataSource;

namespace DalObject
{
    internal partial class DalObject : DalApi.IDal
    {
        private Random rd = new Random();
        public DalObject()
        {
            DataSource.Initialize();
        }

        private static readonly Lazy<DalObject> lazy = new Lazy<DalObject>(() => new DalObject());
        public static DalObject Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public double[] PowerConsumption()
        {
            return new double[] { Config.Free, Config.LightConsumption, Config.MediumConsumption, Config.HeavyConsumption, Config.ChargeRate };
        }
    }
}
