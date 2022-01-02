    namespace DO
    {
        public class ObjectAlreadyExists : System.Exception
        {
            public ObjectAlreadyExists() { }
            public ObjectAlreadyExists(string message) : base(message) { }
            public ObjectAlreadyExists(string message, System.Exception inner) : base(message, inner) { }
        }

        public class ObjectNotFound : System.Exception
        {
            public ObjectNotFound() { }
            public ObjectNotFound(string message) : base(message) { }
            public ObjectNotFound(string message, System.Exception inner) : base(message, inner) { }
        }

        public class InvalidInput : System.Exception
        {
            public InvalidInput() { }
            public InvalidInput(string message) : base(message) { }
            public InvalidInput(string message, System.Exception inner) : base(message, inner) { }
        }
    }