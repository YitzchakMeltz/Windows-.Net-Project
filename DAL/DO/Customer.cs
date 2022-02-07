using System;
using System.Security.Cryptography;

namespace DO
{
    public struct Customer
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DalApi.Util.Coordinate Location { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Password { set => PasswordHash = SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(value)); }

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