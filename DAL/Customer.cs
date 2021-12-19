namespace DalApi
{
    namespace DO
    {
        public struct Customer
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            //public double Longitude { get; set; }
            //public double Latitude { get; set; }
            public Util.Coordinate Location { get; set; }

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
}