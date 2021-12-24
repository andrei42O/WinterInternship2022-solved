using SantaClauseConsoleApp.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClauseConsoleApp.Validators
{
    public class AdressValidator
    {
        public static void check(Adress adr)
        {
            string ret = string.Empty;
            Func<char, bool> notLetter = new Func<char, bool>(x =>
            {
                if ((x >= 'a' && x <= 'z') || (x >= 'A' && x <= 'Z'))
                    return false;
                return true;
            });
            Func<char, bool> hasNumbers = new Func<char, bool>(x =>
            {
                for (char i = '0'; i <= '9'; i++)
                    if (x == i)
                        return true;
                return false;
            });
            if (adr.City.Any(notLetter) || adr.City.Length == 0)
                ret += "Invalid City!\n";
            if (adr.Town.Any(hasNumbers) || adr.Town.Length == 0)
                ret += "Invalid Town!\n";
            if (ret.Length > 0)
                throw new ValidationException(ret);
        }

    }
}
