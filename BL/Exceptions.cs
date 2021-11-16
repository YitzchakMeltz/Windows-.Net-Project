namespace IBL.BO
{
    public class ObjectNotFound : System.Exception
    {
        public ObjectNotFound() { }
        public ObjectNotFound(string message) : base(message) { }
        public ObjectNotFound(string message, System.Exception inner) : base(message, inner) { }
    }
}
