using System.ComponentModel.DataAnnotations;

namespace WebServer.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Color { get; set; }
    }
}