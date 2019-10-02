using System;
using System.Collections.Generic;

namespace BookStore.Entities.Models
{
    public partial class Users
    {
        public Users()
        {
            Reviews = new HashSet<Reviews>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
