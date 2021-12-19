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

        public double[] PowerConsumption()
        {
            return new double[] { Config.Free, Config.LightConsumption, Config.MediumConsumption, Config.HeavyConsumption, Config.ChargeRate };
        }
    }
}
