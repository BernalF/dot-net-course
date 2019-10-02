using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();
            person.FirstName = "Bernal";
            person.LastName = "F";
            
            Console.WriteLine("Hello World!");

            Console.WriteLine(person.FullName);

            Console.ReadKey();
        }
    }
}
