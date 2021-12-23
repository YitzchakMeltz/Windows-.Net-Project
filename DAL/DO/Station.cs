namespace DO
{
    public struct Station
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int AvailableChargeSlots { get; set; }
        //public double Longitude { get; set; }
        //public double Latitude { get; set; }
        public DalApi.Util.Coordinate Location { get; set; }

        /// <summary>
        /// Returns a String with details about the Station
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, ChargeSlots: {AvailableChargeSlots}, Location: {{ {Location} }}.";
        }
    }

}