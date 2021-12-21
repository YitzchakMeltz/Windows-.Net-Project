using System;
using static Dal.DataSource;

namespace Dal
{
    internal partial class DalObject : DalApi.IDal
    {
        private Random rd = new Random();
        private DalObject()
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
