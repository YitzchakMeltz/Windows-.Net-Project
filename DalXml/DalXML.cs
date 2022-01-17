using System;

namespace Dal
{
    internal partial class DalXml : DalApi.IDal
    { 
        private static readonly Lazy<DalXml> lazy = new Lazy<DalXml>(() => new DalXml());
        public static DalXml Instance
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}