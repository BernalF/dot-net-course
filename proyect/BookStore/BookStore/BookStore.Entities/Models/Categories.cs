using System;
using System.Collections.Generic;

namespace BookStore.Entities.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Books = new HashSet<Books>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? InsertDate { get; set; }

        public virtual ICollection<Books> Books { get; set; }
    }
}
