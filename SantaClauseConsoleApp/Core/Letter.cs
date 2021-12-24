using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SantaClauseConsoleApp.Core
{
    public class Letter: Entity<int>
    { 
        // A letter is only assigned to only a child so it's identifier is the child (child's id is unique)

        public List<Item> items { get; set; }
        public DateTime Date { set; get; }
        public Child author { set; get; }

        public Letter(Child child, params Item[] items)
        {
            this.author = child;
            this.Id = child.Id;
            this.items = new List<Item>();
            foreach(Item item in items)
                this.items.Add(item);
            this.Date = DateTime.Now;
        }

        public Letter(Child child, DateTime date, params Item[] items)
        {
            this.Id = child.Id;
            this.author = child;
            this.items = new List<Item>();
            foreach (Item item in items)
                this.items.Add(item);
            this.Date = date;
        }

        public override string ToString()
        {
            string ret = String.Format(
                    "Dear Santa,\n" +
                    "I am {0}\n" +
                    "I am {1} years old. I live at {2}. I have been a very {3} child this year\n" +
                    "What I would like the most this Christmas is:\n", 
                    author.Name, author.getAge(), author.Adress, author.Behavior
                );

            for (int i = 0; i < items.Count; i++) {
                if (i > 0)
                    ret += ",";
                ret += items[i];
            }

            return ret;
        }

        /// <summary>
        /// The function process a letter and creates a Letter entity
        /// </summary>
        /// <param name="letterData">A line with all the letter data(id & date when it was written)</param>
        /// <returns>Letter entity</returns>
        public static Letter processLetter(string letterData, string letterPath)
        {
            string[] elems = letterData.Split("|"); // elems[0] -> letter id | elems[1] -> letter date
            Letter ret = Letter.processLetterContent(letterPath);
            ret.Date = DateTime.Parse(elems[1]);
            return ret;
        }

        /// <summary>
        /// The functions extracts the data from a letters content
        /// </summary>
        /// <param name="letterPath">The path to the letter</param>
        /// <returns>Letter entity</returns>
        public static Letter processLetterContent(string letterPath)
        {
            string temp = "";
            for (int i = letterPath.Length - 5; i > 0 && Char.IsDigit(letterPath[i]); i--)
                temp += letterPath[i];
            temp.Reverse();
            int id = Int32.Parse(temp);
            StreamReader r = new StreamReader(letterPath);
            string line = "";
            while (!r.EndOfStream)
                line = r.ReadLine();
            r.Close();
            return new Letter(Child.extractChildFromLetterContent(letterPath), line.Split(",").Select(x => new Item(x)).ToArray());
        }
    }
}
