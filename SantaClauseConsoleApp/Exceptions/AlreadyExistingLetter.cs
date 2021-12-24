using System;
using System.Runtime.Serialization;

namespace SantaClauseConsoleApp.Exceptions
{
    [Serializable]
    internal class AlreadyExistingLetter : Exception
    {
        public AlreadyExistingLetter()
        {
        }

        public AlreadyExistingLetter(string message) : base(message)
        {
        }
    }
}