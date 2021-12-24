using SantaClauseConsoleApp.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClauseConsoleApp.Validators
{
    public class LetterValidator
    {
        public static void check(Letter l)
        {
            string ret = string.Empty;
            try { AdressValidator.check(l.author.Adress); }
            catch(Exception ex) { ret += ex.ToString(); }

            try { ChildValidator.check(l.author); }
            catch(Exception ex) { ret += ex.ToString(); }

            if (l.Date.CompareTo(DateTime.Now) == 1)
                ret += "Scrisoare scrisa la o data invalida!\n";
            if (ret.Length > 0)
                throw new ValidationException(ret);
        }
    }
}
