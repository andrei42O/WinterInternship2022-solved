using SantaClauseConsoleApp.Constants;
using SantaClauseConsoleApp.Core;
using SantaClauseConsoleApp.Exceptions;
using SantaClauseConsoleApp.Repository;
using SantaClauseConsoleApp.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClauseConsoleApp.Business
{
    public class SantaService
    {
        private LetterRepository letters;
        private ChildRepository childs;

        public SantaService(ref LetterRepository  letters, ref ChildRepository childs)
        {
            this.letters = letters;
            this.childs = childs;
        }

        
        public void writeLetter(int authorID, DateTime date, params string[] rawItems)
        {
            Child child = childs.findOne(authorID);
            if (child == null)
                throw new NoChildFoundException("There is no child with ID #" + authorID + "\n");
            Letter l = new Letter(child, date, rawItems.Select(x => new Item(x)).ToArray());
            LetterValidator.check(l);
            letters.add(l);
            child.hasLetter = true;
            if (childs.update(child) != null)
                throw new NoChildFoundException("There was a problem writing the letter! Child wasn't updated!\n");
        }

        public void writeLetter(int authorID, params string[] rawItems)
        {
            Child child = childs.findOne(authorID);
            if (child == null)
                throw new NoChildFoundException("There is no child with ID #" + authorID + "\n");
            Letter l = new Letter(child, DateTime.Now, rawItems.Select(x => new Item(x)).ToArray());
            LetterValidator.check(l);
            if (letters.add(l) != null)
                throw new AlreadyExistingLetter("There is already a letter written by " + child.Name + "!\n");

            child.hasLetter = true;
            if (childs.update(child) != null)
                throw new NoChildFoundException("There was a problem writing the letter! Child wasn't updated!\n");
        }

        public void addChild(int id, string name, string rawBirthDate, string rawAdress, string rawBehaviour)
        {
            DateTime bDate = DateTime.Parse(rawBirthDate);
            Adress adr = Adress.parse(rawAdress);
            AdressValidator.check(adr);
            BehaviorEnum behavior = BehaviorEnum.Bad;
            if (rawBehaviour.Trim().ToLower().Equals("good"))
                behavior = BehaviorEnum.Good;
            Child c = new Child(id, name, adr, bDate, behavior);
            ChildValidator.check(c);
            if (childs.add(c) != null)
                throw new AlreadyExistingChild("There is already a child with ID #" + id + " or there was other error in the add process");
        }

        /// <summary>
        /// The function generates a toy report for santas helpers
        /// </summary>
        /// <returns>List<Tuple<string, int>></returns>
        public List<Tuple<string, int>> toyReport()
        {
            Dictionary<string, int> toys = new Dictionary<string, int>();
            string toyName;
            foreach (Letter l in letters.GetAll())
            {
                foreach(Item itm in l.items)
                {
                    if (toys.ContainsKey(itm.Name))
                        toys[itm.Name]++;
                    else
                        toys.Add(itm.Name, 1);
                }
            }
            List<Tuple<string, int>> items = toys.Select(x => Tuple.Create(x.Key, x.Value)).ToList();
            items.Sort((e1, e2) => (e2.Item2.CompareTo(e1.Item2)));
            return items;
        }

        /// <summary>
        /// The function generates a travel itinerary for santa
        /// </summary>
        /// <returns>Dictionary<string, List<Adress>></returns>
        public Dictionary<string, List<Adress>> travelItinerary()
        {
            Dictionary<string, List<Adress>> ret = new Dictionary<string, List<Adress>>();
            foreach (Child child in childs.GetAll())
                if (ret.ContainsKey(child.Adress.City.ToLower()))
                    ret[child.Adress.City.ToLower()].Add(child.Adress);
                else
                    ret.Add(child.Adress.City.ToLower(), new List<Adress>() { child.Adress });
            return ret;
        }
    }
}
