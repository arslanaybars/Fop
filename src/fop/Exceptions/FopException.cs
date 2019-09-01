using System;

namespace Fop.Exceptions
{
    public class FopException : Exception
    {
        public FopException() { }

        public FopException(string message) : base(message) { }

        public FopException(string message, Exception inner) : base(message, inner) { }
    }
}
