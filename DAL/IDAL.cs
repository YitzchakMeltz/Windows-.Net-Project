using System;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int ChargeSlots { get; set; }
            //public double Longitude { get; set; }
            //public double Latitude { get; set; }
            public Util.Coordinate Location;

            /// <summary>
            /// Returns a String with details about the Station
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"ID: {ID}, Name: {Name}, ChargeSlots: {ChargeSlots}, Location: {Location}.";
            }
        }

        public struct Drone
        {
            public int ID { get; set; }
            public string Model { get; set; }
            public WeightCategories WeightCategory { get; set; }
            public DroneStatuses DroneStatus { get; set; }
            public double Battery { get; set; }

            /// <summary>
            /// Returns a String with details about the Drone
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"ID: {ID}, Model: {Model}, WeightCategory: {Enum.GetName(typeof(WeightCategories), WeightCategory)}, DroneStatus: {Enum.GetName(typeof(DroneStatuses), DroneStatus)}, Battery: {Battery}.";
            }
        }

        public struct Customer
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            //public double Longitude { get; set; }
            //public double Latitude { get; set; }
            public Util.Coordinate Location;

            /// <summary>
            /// Returns a String with details about the Customer
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"ID: {ID}, Name: {Name}, Phone: {Phone}, Location: {Location}.";
            }
        }

        public struct Parcel
        {
            public int ID { get; set; }
            public int SenderID { get; set; }
            public int TargetID { get; set; }
            public WeightCategories WeightCategory { get; set; }
            public Priorities Priority { get; set; }
            public int DroneID { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public int AssignmentTime { get; set; } // Not sure this is necessary
            public DateTime Delivered { get; set; }

            /// <summary>
            /// Returns a String with details about the Parcel
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"ID: {ID}, SenderID: {SenderID}, TargetID: {TargetID}, WeightCategory: {Enum.GetName(typeof(WeightCategories), WeightCategory)}, Priority: {Enum.GetName(typeof(Priorities), Priority)}, DroneID: {DroneID}, Scheduled: {Scheduled}, PickedUp: {PickedUp}, Delivered: {Delivered}.";
            }
        }

        public struct DroneCharge
        {
            public int DroneID { get; set; }
            public int StationID { get; set; }

            /// <summary>
            /// Returns a String that matches a Drone to its Base Station
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"DroneID: {DroneID}, StationID: {StationID}";
            }
        }

    }
    namespace Util
    {
        public struct Coordinate
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public enum Type { Latitude, Longitude }
            public Coordinate(double latitude, double longitude)
            {
                Latitude = latitude;
                Longitude = longitude;
            }

            /*
            /// <summary>
            /// Calculates distance in meters to another Coordinate
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public double DistanceTo(Coordinate other)
            {
                // Formula copied from .NET Framework GeoCoordinate.GetDistanceTo()

                double dLat1 = this.Latitude * (Math.PI / 180.0);
                double dLon1 = this.Longitude * (Math.PI / 180.0);
                double dLat2 = other.Latitude * (Math.PI / 180.0);
                double dLon2 = other.Longitude * (Math.PI / 180.0);

                double dLon = dLon2 - dLon1;
                double dLat = dLat2 - dLat1;

                double a = Math.Pow(Math.Sin(dLat / 2.0), 2.0) +
                           Math.Cos(dLat1) * Math.Cos(dLat2) *
                           Math.Pow(Math.Sin(dLon / 2.0), 2.0);

                double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

                const Double kEarthRadiusMs = 6376500;
                double dDistance = kEarthRadiusMs * c;

                return dDistance;
            }

            /// <summary>
            /// Returns the approximate distance in meters between this
            /// location and the given location.Distance is defined using
            /// the WGS84 ellipsoid.
            /// </summary>
            /// <param name="other"></param>
            /// <returns>The approximate distance in meters</returns>
            */
            public double DistanceTo(Coordinate other)
            {
                // Algorithm copied from Android Open Source Project
                // https://cs.android.com/android/platform/superproject/+/master:frameworks/base/location/java/android/location/Location.java;l=460;drc=master?q=distanceto&sq=&ss=android%2Fplatform%2Fsuperproject

                int MAXITERS = 20;
                // Convert lat/long to radians
                double lat1 = this.Latitude * Math.PI / 180.0;
                double lat2 = other.Latitude * Math.PI / 180.0;
                double lon1 = this.Longitude * Math.PI / 180.0;
                double lon2 = other.Longitude * Math.PI / 180.0;

                double a = 6378137.0; // WGS84 major axis
                double b = 6356752.3142; // WGS84 semi-major axis
                double f = (a - b) / a;
                double aSqMinusBSqOverBSq = (a * a - b * b) / (b * b);

                double L = lon2 - lon1;
                double A = 0.0;
                double U1 = Math.Atan((1.0 - f) * Math.Tan(lat1));
                double U2 = Math.Atan((1.0 - f) * Math.Tan(lat2));

                double cosU1 = Math.Cos(U1);
                double cosU2 = Math.Cos(U2);
                double sinU1 = Math.Sin(U1);
                double sinU2 = Math.Sin(U2);
                double cosU1cosU2 = cosU1 * cosU2;
                double sinU1sinU2 = sinU1 * sinU2;

                double sigma = 0.0;
                double deltaSigma = 0.0;
                double cosSqAlpha;
                double cos2SM;
                double cosSigma;
                double sinSigma;
                double cosLambda = 0.0;
                double sinLambda = 0.0;

                double lambda = L; // initial guess
                for (int iter = 0; iter < MAXITERS; iter++)
                {
                    double lambdaOrig = lambda;
                    cosLambda = Math.Cos(lambda);
                    sinLambda = Math.Sin(lambda);
                    double t1 = cosU2 * sinLambda;
                    double t2 = cosU1 * sinU2 - sinU1 * cosU2 * cosLambda;
                    double sinSqSigma = t1 * t1 + t2 * t2; // (14)
                    sinSigma = Math.Sqrt(sinSqSigma);
                    cosSigma = sinU1sinU2 + cosU1cosU2 * cosLambda; // (15)
                    sigma = Math.Atan2(sinSigma, cosSigma); // (16)
                    double sinAlpha = (sinSigma == 0) ? 0.0 :
                        cosU1cosU2 * sinLambda / sinSigma; // (17)
                    cosSqAlpha = 1.0 - sinAlpha * sinAlpha;
                    cos2SM = (cosSqAlpha == 0) ? 0.0 :
                        cosSigma - 2.0 * sinU1sinU2 / cosSqAlpha; // (18)

                    double uSquared = cosSqAlpha * aSqMinusBSqOverBSq; // defn
                    A = 1 + (uSquared / 16384.0) * // (3)
                        (4096.0 + uSquared *
                         (-768 + uSquared * (320.0 - 175.0 * uSquared)));
                    double B = (uSquared / 1024.0) * // (4)
                        (256.0 + uSquared *
                         (-128.0 + uSquared * (74.0 - 47.0 * uSquared)));
                    double C = (f / 16.0) *
                        cosSqAlpha *
                        (4.0 + f * (4.0 - 3.0 * cosSqAlpha)); // (10)
                    double cos2SMSq = cos2SM * cos2SM;
                    deltaSigma = B * sinSigma * // (6)
                        (cos2SM + (B / 4.0) *
                         (cosSigma * (-1.0 + 2.0 * cos2SMSq) -
                          (B / 6.0) * cos2SM *
                          (-3.0 + 4.0 * sinSigma * sinSigma) *
                          (-3.0 + 4.0 * cos2SMSq)));

                    lambda = L +
                        (1.0 - C) * f * sinAlpha *
                        (sigma + C * sinSigma *
                         (cos2SM + C * cosSigma *
                          (-1.0 + 2.0 * cos2SM * cos2SM))); // (11)

                    double delta = (lambda - lambdaOrig) / lambda;
                    if (Math.Abs(delta) < 1.0e-12)
                    {
                        break;
                    }
                }

                return b * A * (sigma - deltaSigma);
            }
            public override string ToString()
            {
                return $"Latitude: {toSexagesimal(Type.Latitude)}, Longitude: {toSexagesimal(Type.Longitude)}.";
            }

            private string toSexagesimal(Type type)
            {
                double remainder = (type == Type.Latitude ? Latitude : Longitude);
                // Takes whole part of each int and multiplies the remainder by 60
                int degrees = (int)remainder;
                remainder -= degrees;
                remainder *= 60;
                int minutes = (int)remainder;
                remainder -= minutes;
                remainder *= 60;
                return $"{degrees}°{minutes}'{remainder}'' {((type == Type.Latitude) ? (degrees >= 0 ? "N" : "S") : (degrees >= 0) ? "E" : "W")}";
            }
        }
        
    }
}