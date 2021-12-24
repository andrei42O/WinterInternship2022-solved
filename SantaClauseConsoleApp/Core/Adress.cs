using System;
using System.Collections.Generic;

namespace SantaClauseConsoleApp.Core
{
    public class Adress
    {
        public string City { set; get; }
        public string Town { set; get; }
        public string Street { set; get; }

        public Adress(string city, string town, string street)
        {
            City = city;
            Town = town;
            Street = street;
        }

        public Adress(string city, string town)
        {
            City = city;
            Town = town;
        }

        public Adress(string city)
        {
            City = city;
            Town = city;
        }

        public override string ToString()
        {
            if(Street == null)
                return String.Format("Jud. {0}, Loc. {1}", City, Town); 
            return String.Format("Jud. {0}, Loc. {1}, {2}", City, Town, Street);
        }

        public override bool Equals(object obj)
        {
            Adress o = (Adress)obj;
            return  obj.Equals(this) ||
                (
                    o.City == this.City &&
                    o.Town == this.Town &&
                    o.Street == this.Street
                ) ||
                 base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Adress parse(string line)
        {
            string[] elems = line.Split(", ");
            return elems.Length switch
            {   
                2 => new Adress(elems[0], elems[1]), // in case there's only city and town
                3 => new Adress(elems[0], elems[1], elems[2]), // in case there's city, town and street
                _ => throw new Exception("The adress string is invalid!\n")
            };
        }
    }
}
