using Compare;
using System.Runtime.InteropServices;
internal class Program
{
    public static string[] Args { get; private set; }
    private static void Main(string[] args)
    {
        

        if (args.Length == 2 || args.Length == 3)
            {
           Args= OSPath.ChangerPath(args);
                Console.WriteLine("Аргументов передано нужное количество"+"\n");
            Compare.Compare.ConfigsCompare();
        }
        
            Console.ReadLine();
       
    }
}