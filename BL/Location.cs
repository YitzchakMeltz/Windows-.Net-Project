namespace IBL.BO
{
    public class Location
    {
        public double Longitude { init; get; }
        public double Latitude { init; get; }
        public override string ToString()
        {
            return $"Latitude: {Latitude}, Longitude: {Longitude}";
        }
    }
}
