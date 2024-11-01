using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HW3_u23551683.Models
{
    public class Borrow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BorrowId { get; set; }

        public int? StudentId { get; set; }
        public Student Student { get; set; }

        public int? BookId { get; set; }
        public Book Book { get; set; }

        public DateTime? TakenDate { get; set; }
        public DateTime? BroughtDate { get; set; }
    }
}