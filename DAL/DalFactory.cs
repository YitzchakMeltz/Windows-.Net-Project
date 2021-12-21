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
        public static IDal GetDal(string objectType)
        {
            switch(objectType)
            {
                case "DalObject":
                    return (IDal)Assembly.LoadFrom(@"..\..\..\..\DalObject\bin\Debug\net5.0\DalObject.dll").GetType("Dal.DalObject").GetProperty("Instance").GetValue(null);

                case "DalXml":
                    throw new NotImplementedException();

                default:
                    throw new StringNotValid($"{objectType} is an invalid string. String must be 'DalObject' or 'DalXml'");
            }
        }
    }
}
