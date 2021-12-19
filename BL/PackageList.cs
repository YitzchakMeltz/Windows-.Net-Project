using System;

<<<<<<< HEAD
namespace BL
=======
namespace IBL.BO
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
{
    public class PackageList
    {
        public int ID { init; get; }
        public string Sender { init; get; }
        public string Receiver { init; get; }
        public WeightCategories Weight { init; get; }
        public Priorities Priority { init; get; }
        public Statuses Status { init; get; }

        public override string ToString()
        {
            return $"ID: {ID}, Sender: {Sender}, Receiver: {Receiver}, Weight: {Weight}, Priority: {Priority}, Status: {Status}";
        }
    }
}
