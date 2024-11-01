using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW3_u23551683.Models
{
    public class HomeViewModel
    {
        public List<Student> Students { get; set; }
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
        public List<Type> Types { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int StudentCurrentPage { get; set; }
        public int StudentTotalPages { get; set; }
    }
}