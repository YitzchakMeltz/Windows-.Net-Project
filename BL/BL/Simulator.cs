using System;
using BO;
using System.Threading;
using static BL.BL;
using System.Linq;

namespace BL
{
    class Simulator
    {
        const int DELAY = 1000; // milliseconds
        const double SPEED = 1000; // km/s
        double distance; // meters

        public Simulator(BL bl, uint droneID, Action updateAction, Func<bool> stopCheck) {
            while (!stopCheck())
            {
                if (distance > 0) {
                    distance -= SPEED * DELAY;
                    continue;
                }

                Drone drone = bl.GetDrone(droneID);
                if (drone.Status == DroneStatuses.Free)
                {
                    lock (bl)
                        try
                        {
                            bl.AssignPackageToDrone(droneID);
                            distance = bl.Distance(drone.Location, drone.Package.CollectionLocation);
                        }
                        catch (InvalidManeuver)
                        {
                            bl.ChargeDrone(droneID);
                        }
                }
                else if (drone.Status == DroneStatuses.Maintenance)
                {
                    lock (bl)
                        try
                        {
                            bl.ReleaseDrone(droneID);
                            if (bl.GetDrone(droneID).Battery < 100) bl.ChargeDrone(droneID);
                        }
                        catch (InvalidManeuver) { }
                }
                else if (drone.Status == DroneStatuses.Delivering)
                {
                    if (!bl.GetDrone(droneID).Package.Delivering)
                    {
                        lock (bl)
                            try
                            {
                                bl.CollectPackage(droneID);
                                distance = drone.Package.DeliveryDistance;
                            }
                            catch (InvalidManeuver) { }
                    }
                    else
                    {
                        lock (bl)
                            try
                            {
                                bl.DeliverPackage(droneID);
                                distance -= bl.Distance(drone.Location, bl.ClosestStation(drone.Location).Location);
                            }
                            catch (InvalidManeuver) { }
                    }
                }
                updateAction();
                Thread.Sleep(DELAY);
            }
        }
    }
}
