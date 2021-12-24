using SantaClauseConsoleApp.Core;
using SantaClauseConsoleApp.Exceptions;
using SantaClauseConsoleApp.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SantaClauseConsoleApp.Repository
{
    public class LetterRepository: Repository<Letter, int>
    {
        private readonly string lettersDataPath;
        private readonly string lettersStoragePath;
        Dictionary<int, Letter> letters = null;
        public LetterRepository(string lettersDataPath, string lettersStoragePath)
        {
            this.lettersDataPath = lettersDataPath;
            this.lettersStoragePath = lettersStoragePath;
            letters = new Dictionary<int, Letter>();
            loadFromFile();
        }

        /// <summary>
        /// The function loads all the letter from the disk into the memory
        /// </summary>
        private void loadFromFile()
        {
            letters.Clear();
            using (StreamReader file = new StreamReader(lettersDataPath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string tempPath = this.lettersStoragePath + "letter" + line.Split("|")[0] + ".txt";
                    Letter l = Letter.processLetter(line, tempPath);
                    letters.Add(l.Id, l);
                }
            }

        }
       
        /// <summary>
        /// The function saves to file all the letters data and letters themselves
        /// </summary>
        private void saveToFile()
        {
            using (StreamWriter fs = File.CreateText(this.lettersDataPath))
            {
                foreach (KeyValuePair<int, Letter> l in letters)
                {
                    fs.WriteLine(string.Format("{0}|{1}", l.Key, l.Value.Date));
                    using (StreamWriter lfs = File.CreateText(this.lettersStoragePath + "letter" + l.Key + ".txt"))
                    {
                        lfs.Write(l.Value);
                    }
                }
            }
        }

        /// <summary>
        /// The functions saves a letters data on the disk 
        /// </summary>
        /// <param name="l">Letter to be saved</param>
        private void appendToFile(Letter l)
        {
            using (StreamWriter fs = File.AppendText(this.lettersDataPath))
            {
                string letterDates = string.Format("{0}|{1}", l.Id, l.Date);
                fs.WriteLine(letterDates);
            }
            createFile(l);
        }

        /// <summary>
        /// The functions creates a letter file and prints it using the template provided
        /// </summary>
        /// <param name="l">The letter to be pritned</param>
        private void createFile(Letter l)
        {
            if (File.Exists(this.lettersStoragePath + "letter" + l.Id + ".txt"))
                throw new RepoException("A letter for the Child with the id " + l.Id + " already exists!\n");
            try
            {
                using (FileStream fs = File.Create(this.lettersStoragePath + "letter" + l.Id + ".txt"))
                {
                    Byte[] title = new UTF8Encoding(true).GetBytes(l.ToString());
                    fs.Write(title, 0, title.Length);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// The functions eraseas a letter file from the disk
        /// </summary>
        /// <param name="id">Letter's id (int)</param>
        private void deleteLetterFile(int id)
        {
            try { File.Delete(this.lettersStoragePath + "letter" + id + ".txt"); }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        public Letter findOne(int id)
        {
            if(letters.ContainsKey(id))
                return letters[id];
            return null;
        }

        public IEnumerable<Letter> GetAll()
        {
            return letters.Select(x => x.Value);
        }

        public Letter delete(int id)
        {
            Letter temp = findOne(id);
            if (temp == null)
                return null;
            if (!letters.Remove(id))
                throw new RepoException("The letter was found but cannot remove it from the memory!\n");
            deleteLetterFile(id);
            saveToFile();
            return temp;
        }

        public Letter add(Letter o)
        {
            LetterValidator.check(o);
            if (!letters.ContainsKey(o.Id))
            {
                letters.Add(o.Id, o);
                appendToFile(o);
                return null;
            }
            return o;
        }

        public Letter update(Letter o)
        {
            LetterValidator.check(o);
            if (!letters.ContainsKey(o.Id))
                return o;
            if (!letters.Remove(o.Id))
                throw new RepoException("The letter was found but cannot remove it from the memory!\n");
            deleteLetterFile(o.Id);
            letters.Add(o.Id, o);
            appendToFile(o); // saves the new letter data and content on the disk (withoud deleting the old letter data on disck)
            saveToFile(); // uploads on the disk only the letters and data we want (deleting the old letter)
            return null;
        }
    }
}
