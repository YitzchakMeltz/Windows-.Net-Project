using System;
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

        private static DalObject instance = null;

        public static DalObject Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DalObject();
                }
                return instance;
            }
        }

        public double[] PowerConsumption()
        {
            return new double[] { Config.Free, Config.LightConsumption, Config.MediumConsumption, Config.HeavyConsumption, Config.ChargeRate };
        }
    }
}
