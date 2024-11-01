using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HW3_u23551683.Models
{
    public class Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TypeId { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        // Navigation property for related books
        public ICollection<Book> Books { get; set; }
    }
}