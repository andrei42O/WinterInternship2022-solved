using SantaClauseConsoleApp.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClauseConsoleApp.Validators
{
    public class ChildValidator
    {
        public static void check(Child child)
        {
            Func<char, bool> notLetters = new Func<char, bool>(x =>
            {
                if ((x >= 'a' && x <= 'z') || (x >= 'A' && x <= 'Z'))
                    return false;
                return true;
            });
            string ret = String.Empty;
            if (child.Name.Length == 0 || child.Name.Any(notLetters))
                ret += "Invalid Name\n";
            if (child.getAge() >= 35 || child.getAge() <= 0)
                ret += "Invalid Birth day\n";
            if (ret.Length != 0)
                throw new ValidationException(ret);
        }
    }
}
