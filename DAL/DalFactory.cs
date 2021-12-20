using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace DalApi
{
    public static class DalFactory
    {
        public static DalApi.IDal GetDal(string objectType)
        {
            switch(objectType)
            {
                case "DalObject":
                    Activator.CreateInstance(@"..\..\..\..\DalObject\bin\Debug\net5.0\ref\DalObject.dll", "DalObject").Unwrap();
                    System.Reflection.Assembly.LoadFrom(@"..\..\..\..\DalObject\bin\Debug\net5.0\ref\DalObject.dll");
                    //return DalObject.DalObject.Instance;
                    throw new NotImplementedException();

                case "DalXml":
                    throw new NotImplementedException();

                default:
                    throw new StringNotValid($"{objectType} is an invalid string. String must be 'DalObject' or 'DalXml'");
            }
        }
    }
}
