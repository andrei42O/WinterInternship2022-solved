using SantaClauseConsoleApp.Constants;
using SantaClauseConsoleApp.Core;
using System;
using System.IO;
using System.Linq;

namespace SantaClauseConsoleApp.Core
{
    public class Child: Entity<int>
    {
        public string Name { set; get; }
        public DateTime BirthDate { set; get; }
        public Adress Adress { set; get; }
        public BehaviorEnum Behavior { set; get; }
        public bool hasLetter { set; get; }

        // Constructor which recives all the data
        public Child(int Id, string Name, Adress adress, DateTime BirthDate, BehaviorEnum Behavior, bool hasLetter = false)
        {
            this.Id = Id;
            this.Name = Name;
            this.Adress = adress;
            this.BirthDate = BirthDate;
            this.Behavior = Behavior;
            this.hasLetter = hasLetter;
        }

        // Constructor with only necessary parameters
        public Child(int Id, string Name, Adress adress, DateTime BirthDate, bool hasLetter = false)
        {
            this.Id = Id;
            this.Name = Name;
            this.Adress = adress;
            this.BirthDate = BirthDate;
            this.Behavior = BehaviorEnum.Good;
            this.hasLetter = hasLetter;
        }


        public override string ToString()
        {
            return String.Format("Name: {0}, Age: {1}, Adress: {2}, Behavior: {3}",
                    Name, getAge(), Adress, Behavior);
        }

        public override bool Equals(object obj)
        {
            Child o = (Child)obj;
            return this.Id.Equals(o.Id) || base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int getAge()
        {
            int age = DateTime.Now.Year - BirthDate.Year;
            if ((DateTime.Now.Month > BirthDate.Month) || (DateTime.Now.Month == BirthDate.Month && DateTime.Now.Day >= BirthDate.Day))
                age++;
            return age;
        }
        /// <summary>
        /// The function creates a child from a line 
        /// </summary>
        /// <param name="line">string</param>
        /// <returns>A Child entity with the data provided</returns>
        public static Child createChild(string line)
        {
            string[] elems = line.Split("|");
            return new Child(
                    Int32.Parse(elems[0]),
                    elems[1],
                    Adress.parse(elems[3]),
                    DateTime.Parse(elems[2]),
                    (BehaviorEnum)Enum.Parse(typeof(BehaviorEnum),
                    elems[4]),
                    "1".Equals(elems[5])
                ) ;
            
        }

        /// <summary>
        /// The function converts the child to data that can be stored
        /// </summary>
        /// <param name="o">Child entity</param>
        /// <returns>string - that represents the child data</returns>
        public static string convertChildToData(Child o)
        {
            string ret = o.Id + "|" +
                o.Name + "|" +
                o.BirthDate + "|" +
                o.Adress.City + ", " + o.Adress.Town;
            if (o.Adress.Street != null)
                ret += "," + o.Adress.Street;
               ret += "|" + o.Behavior + "|";
            if (o.hasLetter)
                ret += "1";
            else
                ret += "0";
            return ret;
        }

        /// <summary>
        /// The function extract all the data that can pe extracted from a letter only 
        /// </summary>
        /// <param name="content">string[] representing all the lines in the letter</param>
        /// <returns>Child entity with all the data colected</returns>
        public static Child extractChildFromLetterContent(string path)
        {
            // parse the ID from the letter name
            string temp = string.Empty;
            int i, st;
            for (i = path.Length - 5; i > 0 && Char.IsDigit(path[i]); i--)
                temp += path[i];
            temp.Reverse();
            int id = Int32.Parse(temp);

            // letter content
            string[] content = File.ReadAllLines(path);

            // child name
            string tempName = content[1].Split()[2];

            // adress composition
            string[] tempElems = content[2].Split();
            int age = Int32.Parse(tempElems[2]);

            i = st = 8;
            string city = string.Empty, town = string.Empty;
            Adress adress;
            do // extract the city (may be multiple words)
            {
                i++;
                if (i > st + 1)
                    city += " ";
                city += tempElems[i];
            } while (!tempElems[i].EndsWith(","));
            city = city.Remove(city.Length - 1);

            i += 1;
            st = i;
            do // extract the town (may be multiple words)
            {
                i++;
                if (i > st + 1)
                    town += " ";
                town += tempElems[i];
            } while (!tempElems[i].EndsWith(",") && !tempElems[i].EndsWith("."));
            town = town.Remove(town.Length - 1);
            if (tempElems[i].EndsWith(",")) // check if there is a street 
            {
                string street = string.Empty;
                st = i;
                do
                {
                    i++;
                    if (i > st + 1)
                        street += " ";
                    street += tempElems[i];
                } while (!tempElems[i].EndsWith(",") && !tempElems[i].EndsWith("."));
                street += tempElems[++i].Trim();
                street = street.Remove(street.Length - 2);
                adress = new Adress(city, town, street); // create an adress with city, town and street
            }
            else
                adress = new Adress(city, town); // create an adress only with the city and town

            // behaviour parse
            BehaviorEnum b = (BehaviorEnum)Enum.Parse(typeof(BehaviorEnum), tempElems[tempElems.Length - 4]);

            return new Child(id, tempName, adress, DateTime.Now.AddYears(-(age - 1)), b, true);

        }

    }
}
