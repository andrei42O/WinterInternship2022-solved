using System;
using System.Runtime.Serialization;

namespace SantaClauseConsoleApp.Exceptions
{
    [Serializable]
    internal class ValidationException : Exception
    {
        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }
    }
}