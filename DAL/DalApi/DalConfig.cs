using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalApi
{
    class DalConfig
    {
        /// <summary>
        /// Gets the Dal type specified in dal-config.xml
        /// </summary>
        internal static string DalName;

        /// <summary>
        /// Dictionary of all available Dal types, with corresponding attributes for each one
        /// </summary>
        internal static Dictionary<string, Dictionary<string, string>> DalPackages;
        static DalConfig()
        {
            XElement dalConfig = XElement.Load(@"xml\dal-config.xml");
            DalName = dalConfig.Element("dal").Value;
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                           select pkg).ToDictionary(p => "" + p.Name, p => {
                               Dictionary<string, string> dict = (from attribute in p.Attributes() select attribute).ToDictionary(a => "" + a.Name, a => a.Value);
                               return dict.Prepend(KeyValuePair.Create("path", p.Value)).ToDictionary(p => p.Key, p => p.Value);
                           });
        }
    }

    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception e) : base(msg, e) { }
    }
}