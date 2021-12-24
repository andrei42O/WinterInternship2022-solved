namespace SantaClauseConsoleApp.Core
{
    public class Item
    {
        // Item unique identifier should be name standardized as ( [A-Z][\S]+ => ("Truck", "Mustang V2.3") )
        public string Name { set; get; }

        public Item(string name)
        {
            name = name.Trim();
            Name = name.Remove(1).ToUpper() + name.Substring(1).ToLower();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
