using SantaClauseConsoleApp.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClauseConsoleApp.Business
{
    public class ChildLetterDTO
    {
        public Child Child { set; get; }
        public Letter Letter { set; get; }

        public ChildLetterDTO(string letterPath)
        {
            Child = Child.extractChildFromLetterContent(letterPath);
            Letter = Letter.processLetterContent(letterPath);
        }
        
    }
}
