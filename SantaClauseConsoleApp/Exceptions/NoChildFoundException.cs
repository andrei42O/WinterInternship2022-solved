using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClauseConsoleApp.Exceptions
{
    public class NoChildFoundException : Exception
    {
        public NoChildFoundException(string message) : base(message)
        {
        }
    }
}
