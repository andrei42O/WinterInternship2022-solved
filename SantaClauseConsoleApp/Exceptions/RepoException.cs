using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClauseConsoleApp.Exceptions
{
    public class RepoException: Exception
    {
        public RepoException(): base()
        {

        }

        public RepoException(string s): base(s)
        {

        }

    }
}
