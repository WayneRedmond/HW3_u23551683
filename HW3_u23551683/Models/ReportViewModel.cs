using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW3_u23551683.Models
{
    public class ReportViewModel
    {
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
        public List<Type> Types { get; set; }
        public List<CategoryReport> CategoryReports { get; set; } // New property for category report data
        public ReportExportViewModel ExportData { get; set; }
    }

    public class CategoryReport
    {
        public string CategoryName { get; set; }
        public int BookCount { get; set; }
    }
}