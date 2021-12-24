using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SantaClauseConsoleApp.Business;
using SantaClauseConsoleApp.Constants;
using SantaClauseConsoleApp.Core;
using SantaClauseConsoleApp.Repository;

namespace SantaClauseConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Question1();
            Question2();
            Question3();
            Question4();
            Question5();  
            Question6();
        }

        public static void Question1() {
            Child c1 = new Child(0, "Marcel", new Adress("Botosani", "Botosani", "Strada Soarelui, nr. 1"), new System.DateTime(2003, 6, 18), true);
            Child c2 = new Child(1, "Mitea", new Adress("Suceava", "Suceava", "Strada Mihai Eminescu, nr. 57"), new System.DateTime(2001, 4, 5), true);
            Child c3 = new Child(2, "Andrei", new Adress("Botosani", "Oraseni-Deal", "Strada Principala, nr. 1337"), new System.DateTime(2002, 2, 1), true);
            Child[] childsArray = new Child[] { c1, c2, c3 };

            Letter[] lettersArray = new Letter[]{
                new Letter(c1, new Item("Camion"), new Item("Casti")),
                new Letter(c2, new Item("Telefon"), new Item("Casti")),
                new Letter(c3, new Item("Internship"), new Item("Bursa"))
            };
            
            System.Console.WriteLine("Childs:\n");
            foreach (Child child in childsArray)
                System.Console.WriteLine(child);


            System.Console.WriteLine("\nLetters:\n");
            bool show = false;
            Console.WriteLine("---------------------------");
            foreach (Letter letter in lettersArray)
            {
                if (show)
                    Console.WriteLine("---------------------------");
                else 
                    show = true;
                System.Console.WriteLine(letter);
            }
            Console.WriteLine("---------------------------");
        }

        static void Question2()
        {
            List<ChildLetterDTO> dtos = new List<ChildLetterDTO>()
            {
                new ChildLetterDTO(Paths.lettersPath + "letter0.txt"),
                new ChildLetterDTO(Paths.lettersPath + "letter1.txt"),
                new ChildLetterDTO(Paths.lettersPath + "letter2.txt")
            };
            Console.WriteLine("@-----Names-----@");
            foreach(ChildLetterDTO dto in dtos)
                Console.WriteLine(dto.Child.Name);
            Console.WriteLine("");
        }

        static void Question3()
        {
            ChildRepository cRepo = new ChildRepository(Paths.childsDataPath);
            LetterRepository lRepo = new LetterRepository(Paths.lettersDataPath, Paths.lettersPath);
            SantaService serv = new SantaService(ref lRepo, ref cRepo);
            foreach (Letter letter in lRepo.GetAll())
                Console.WriteLine(letter + "\n");
            // We must add first a children or a children with no letter must be existing because a letter can't be created without an author
            serv.addChild(4, "Cristi", "1/24/2002", "Cluj, Cluj-Napoca", "good");
            serv.writeLetter(4, "Geaca", "Laptop", "Masina");
            
            foreach (Letter letter in lRepo.GetAll())
                Console.WriteLine(letter + "\n");

            // deletes the child and the letter after so we can rerun with no exceptions
            cRepo.delete(4);
            lRepo.delete(4);
        }

        static void Question4()
        {
            ChildRepository cRepo = new ChildRepository(Paths.childsDataPath);
            LetterRepository lRepo = new LetterRepository(Paths.lettersDataPath, Paths.lettersPath);
            SantaService serv = new SantaService(ref lRepo, ref cRepo); 
            foreach (Tuple<string, int> val in serv.toyReport())
                Console.WriteLine(val.Item1+ " - " + val.Item2);
        }

        static void Question5()
        {
             /**
             * Why we cannot apply the SINGLETON pattern in this implementation?
             * 
             * To start, singleton pattern refers to a class that is instantieted only one time, which implies that the class
             * has a private constructor and a private instance reference to the class itself. The result is a class independent
             * from others. This class provides a way to access its only object which can be accessed directly without need to 
             * instantiate the object of the class.
             * Singleton pattern wouldn't fit in the current implementation for a few big reason, and those sumarize as:
             * the need of other fields. 
             * 
             * Assuming we use the pattern, we would need to feed it the "ChildRepository" and the coresponding "LetterRepository" for
             * access to the entities so that we can be provided with: "toyReport" and "travelItinerary". To do that we need 
             * to feed the constructor parameters which is contradictory with the singleton pattern (dependence of others).
             * 
             * We could also create a init(ChildRepository, LetterRepository) method to assign the repositories to their coresponding 
             * fields but that is contrary with the pattern, as we mentioned earlier (dependence of others).
             * 
             * Also, we could send the repositories as parameters to every function that needs them but that is a bad programming 
             * practice and is also contrary with the pattern. 
             * 
             * Summarizing the main points, we cannot apply the singleton pattern in this implementation.
             */
        }

        static void Question6()
        {
            ChildRepository cRepo = new ChildRepository(Paths.childsDataPath);
            LetterRepository lRepo = new LetterRepository(Paths.lettersDataPath, Paths.lettersPath);
            SantaService serv = new SantaService(ref lRepo, ref cRepo);
            foreach (KeyValuePair<string, List<Adress>> pair in serv.travelItinerary()) {
                Console.WriteLine(String.Format("--------------------{0}--------------------", pair.Key.Remove(1).ToUpper() + pair.Key.Substring(1)));
                foreach (Adress adr in pair.Value)
                    Console.WriteLine(adr);
                Console.WriteLine("");
            }
        }
    }
}
