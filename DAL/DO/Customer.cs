using System;
using System.Security.Cryptography;

namespace DO
{
    public struct Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DalApi.Util.Coordinate Location { get; set; }
        private byte[] _hash;
        public byte[] Password {
            get { return _hash; }
            set { _hash =  SHA256.Create().ComputeHash(value); }
        }

        /// <summary>
        /// Returns a String with details about the Customer
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Phone: {Phone}, Location: {{ {Location} }}.";
        }
    }

}