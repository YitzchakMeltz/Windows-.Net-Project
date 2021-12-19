using System;

<<<<<<< HEAD
namespace BL
=======
namespace IBL.BO
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
{
    public class Location
    {
        public double Longitude { init; get; }
        public double Latitude { init; get; }
        public override string ToString()
        {
            string toSexagesimal(double coord, char positive, char negative) {
                double remainder = Math.Abs(coord);
                // Takes whole part of each int and multiplies the remainder by 60
                int degrees = (int)remainder;
                remainder -= degrees;
                remainder *= 60;
                int minutes = (int)remainder;
                remainder -= minutes;
                remainder *= 60;
                return $"{degrees}°{minutes}'{Math.Round(remainder, 3)}''{(coord >= 0 ? positive : negative)}";
            }
            return $"{toSexagesimal(Latitude, 'N', 'S')}, {toSexagesimal(Longitude, 'E', 'W')}";
        }
    }
}
