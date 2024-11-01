using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW3_u23551683.Models
{
    public class MaintainViewModel
    {
        public List<Type> Types { get; set; }
        public List<Author> Authors { get; set; }
        public List<Borrow> Borrows { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int BorrowCurrentPage { get; set; }
        public int BorrowTotalPages { get; set; }
    }
}