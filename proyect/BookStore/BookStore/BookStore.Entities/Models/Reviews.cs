using System;
using System.Collections.Generic;

namespace BookStore.Entities.Models
{
    public partial class Reviews
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public DateTime? InsertDate { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }

        public virtual Books Book { get; set; }
        public virtual Users User { get; set; }
    }
}
