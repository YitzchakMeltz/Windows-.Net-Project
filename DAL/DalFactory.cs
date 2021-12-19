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
                    return DalObject.DalObject.Instance;

                case "DalXml":
                    throw new NotImplementedException();

                default:
                    throw new StringNotValid($"{objectType} is an invalid string. String must be 'DalObject' or 'DalXml'");
            }
        }
    }
}
