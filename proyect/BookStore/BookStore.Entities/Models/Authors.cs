using System;
using System.Collections.Generic;

namespace BookStore.Entities.Models
{
    public partial class Authors
    {
        public Authors()
        {
            Books = new HashSet<Books>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public virtual ICollection<Books> Books { get; set; }
    }
}
