using SantaClauseConsoleApp.Core;
using SantaClauseConsoleApp.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SantaClauseConsoleApp.Repository
{
    public class ChildRepository : Repository<Child, int>
    {
        private List<Child> childs = null;
        private string childsDataPath;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="childsDataPath"> Tells the repository where the childs data is stored</param>
        public ChildRepository(string childsDataPath)
        {
            this.childsDataPath = childsDataPath;
            childs = new List<Child>();
            loadFromFile();
        }

        /// <summary>
        /// The function loads all the childs from the file into the memory
        /// </summary>
        private void loadFromFile()
        {
            childs.Clear();
            foreach (string line in System.IO.File.ReadLines(this.childsDataPath))
                childs.Add(Child.createChild(line));
        }

        /// <summary>
        /// Stores a child data into the file
        /// </summary>
        /// <param name="o">The child to be stored(Child)</param>
        private void appendToFile(Child o)
        {
            using (StreamWriter fs = File.AppendText(this.childsDataPath))
            {
                fs.WriteLine(Child.convertChildToData(o));
            }
        }

        /// <summary>
        /// The function saves all the childs loaded in memory on the disk
        /// </summary>
        private void saveToFile()
        {
            using (StreamWriter fs = File.CreateText(this.childsDataPath))
            {
                foreach(Child child in childs)
                    fs.WriteLine(Child.convertChildToData(child));
            }
        }

        
        public Child add(Child o)
        {
            ChildValidator.check(o);
            if (findOne(o.Id) == null)
            {
                childs.Add(o);
                appendToFile(o);
                return null;
            }
            return o;
        }

        public Child delete(int id)
        {
            Child temp = findOne(id);
            if (temp == null)
                return null;
            childs.Remove(temp);
            saveToFile();
            return temp;
        }

        public Child findOne(int id)
        {
            Predicate<Child> sameID = delegate (Child l) { return l.Id.Equals(id); };
            return childs.Find(sameID);
        }

        public IEnumerable<Child> GetAll()
        {
            return childs.AsEnumerable();
        }

        public Child update(Child o)
        {
            ChildValidator.check(o);
            Predicate<Child> sameID = delegate (Child l) { return l.Id.Equals(o.Id); };
            int index = childs.FindIndex(sameID);
            if (index == -1)
                return o;
            childs.RemoveAt(index);
            childs.Add(o);
            saveToFile();
            return null;
        }
    }
}
