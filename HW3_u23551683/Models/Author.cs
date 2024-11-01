using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HW3_u23551683.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(70)]
        public string Surname { get; set; }

        // Navigation property for related books
        public ICollection<Book> Books { get; set; }
    }
}