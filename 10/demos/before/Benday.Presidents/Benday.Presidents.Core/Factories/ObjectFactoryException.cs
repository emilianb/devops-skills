using System;
using System.Collections.Generic;
using System.Linq;

namespace Benday.Presidents.Core.Factories
{
    public class ObjectFactoryException : Exception
    {
        public ObjectFactoryException() { }
        public ObjectFactoryException(string message) : base(message) { }
        public ObjectFactoryException(string message, Exception innerException) : base(message, innerException) { }
    }
}
