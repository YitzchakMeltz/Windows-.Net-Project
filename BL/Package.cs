﻿using System;

<<<<<<< HEAD
namespace BL
=======
namespace IBL.BO
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
{
    public class Package
    {
        public int ID { init; get; }
        public PackageCustomer Sender { init; get; }
        public PackageCustomer Receiver { init; get; }
        public WeightCategories Weight { init; get; }
        public Priorities Priority { init; get; }
        public DeliveryDrone Drone { init; get; }
        public DateTime? Creation { init; get; }
        public DateTime? AssignmentTime { init; get; }
        public DateTime? CollectionTime { init; get; }
        public DateTime? DeliveryTime { init; get; }

        public override string ToString()
        {
            return $"ID: {ID}, Sender: ({Sender}), Receiver: ({Receiver}), Weight: {Weight}, Priority: {Priority}, Drone: ({Drone}), Creation: {Creation}" + (AssignmentTime == null ? "" : $", Assignment Time: {AssignmentTime}") + (CollectionTime == null ? "" : $", Collection Time: {CollectionTime}") + (DeliveryTime == null ? "" : $", Delivery Time: {DeliveryTime}");
        }
    }
}
