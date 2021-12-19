using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace DalObject
{
    public static class DalFactory
    {
        public static DalApi.IDal GetDal(string objectType)
        {
            DalApi.IDal returnValue = new DalObject();

            if (objectType == "DalObject")
            {
                return returnValue;
            }

            else if(objectType == "DalXml")
            {
                //returnValue = new DalXml();
            }

            else
            {
                throw new StringNotValid($"{objectType} is an invalid string. Must be 'DalObject' or 'DalXml'");
            }

            return returnValue;
        }
    }
}
