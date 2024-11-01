using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HW3_u23551683.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [MaxLength(90)]
        public string Name { get; set; }

        public int PageCount { get; set; }
        public int Point { get; set; }

        // Foreign Key relationships
        public int? AuthorId { get; set; }
        public Author Author { get; set; }

        public int? TypeId { get; set; }
        public Type Type { get; set; }

        // Navigation property for borrows
        public ICollection<Borrow> Borrows { get; set; }
    }
}