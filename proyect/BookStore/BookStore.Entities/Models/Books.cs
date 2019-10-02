using System;
using System.Collections.Generic;

namespace BookStore.Entities.Models
{
    public partial class Books
    {
        public Books()
        {
            Reviews = new HashSet<Reviews>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? InsertDate { get; set; }
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Authors Author { get; set; }
        public virtual Categories Category { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
