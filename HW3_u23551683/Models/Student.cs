using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HW3_u23551683.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Surname { get; set; }

        public DateTime Birthdate { get; set; }

        [MaxLength(10)]
        public string Gender { get; set; }

        [MaxLength(7)]
        public string Class { get; set; }

        public int Point { get; set; }

        // Navigation property for related borrows
        public ICollection<Borrow> Borrows { get; set; }
    }
}