using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClauseConsoleApp.Core
{
    public class Entity<T>
    {
        public T Id { set; get; }
    }
}
