using System;
using BO;
using System.Threading;
using static BL.BL;
using System.Linq;

namespace BL
{
    /// <summary>
    /// Simulator that simulates the actions for a drone. Performs actions automatically and follows time limitations.
    /// </summary>
    class Simulator
    {
        const int DELAY = 1000; // milliseconds
        const double SPEED = 1000; // km/s
        double distance = 0; // meters

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
                            distance = bl.Distance(drone.Location, bl.GetDrone(droneID).Package.CollectionLocation);
                        }
                        catch (InvalidManeuver)
                        {
                            if (drone.Battery == 100) { }
                            else if (drone.Location == bl.ClosestStation(drone.Location).Location)
                                bl.ChargeDrone(droneID);
                            else
                            {
                                distance = bl.Distance(drone.Location, bl.ClosestStation(drone.Location).Location);
                                bl.Drones.Find(d => d.ID == droneID).Location = bl.ClosestStation(drone.Location).Location;
                            }
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
