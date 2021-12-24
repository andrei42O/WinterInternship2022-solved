using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClauseConsoleApp.Exceptions
{
    public class AlreadyExistingChild: Exception
    {
        public AlreadyExistingChild(): base() { }
        public AlreadyExistingChild(string str): base(str) { }

    }
}
