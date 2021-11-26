namespace IBL.BO
{
    public class ObjectNotFound : System.Exception
    {
        public ObjectNotFound() { }
        public ObjectNotFound(string message) : base(message) { }
        public ObjectNotFound(string message, System.Exception inner) : base(message, inner) { }
    }

    public class ObjectAlreadyExists : System.Exception
    {
        public ObjectAlreadyExists() { }
        public ObjectAlreadyExists(string message) : base(message) { }
        public ObjectAlreadyExists(string message, System.Exception inner) : base(message, inner) { }
    }

    public class InvalidManeuver : System.Exception
    {
        public InvalidManeuver() { }
        public InvalidManeuver(string message) : base(message) { }
    }
}
