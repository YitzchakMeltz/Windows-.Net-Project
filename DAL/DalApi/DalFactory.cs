using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace DalApi
{
    public static class DalFactory
    {
        public static IDal GetDal()
        {
            string dalType = DalConfig.DalName; // Get value of "dal" in xml file
            Dictionary<string, string> dalPkg = DalConfig.DalPackages[dalType]; // Get attributes that correspond with dalType
            if (dalPkg == null) throw new DalConfigException($"Package {dalType} not found in dal-config.xml");

            try { Assembly.LoadFrom(dalPkg["path"]); }  // Load dll of dalType
            catch (Exception) { throw new DalConfigException($"Failed to load {dalPkg["path"]}"); }


            Type type = Type.GetType($"{dalPkg["namespace"]}.{dalPkg["class"]}, {dalPkg["class"]}");
            if (type == null) throw new DalConfigException($"Class {dalPkg["class"]} was not found in {dalPkg["path"]}");

            IDal dal = (IDal)type.GetProperty("Instance",
                BindingFlags.Public | BindingFlags.Static).GetValue(null);

            if (dal == null) throw new DalConfigException($"Class {dalPkg["class"]} is not a singleton or using wrong property name for Instance");

            return dal;
        }
    }
}
