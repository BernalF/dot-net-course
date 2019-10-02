namespace Consola
{
    public class Person 
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get => FirstName + LastName; }
    }
}