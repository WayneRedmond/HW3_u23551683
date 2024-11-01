using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HW3_u23551683.Models
{
    public class ReportExportViewModel
    {
        public string Filename { get; set; }
        public string FileType { get; set; }

        [AllowHtml]
        public string Description { get; set; }
        public string ChartImage { get; set; }
    }
}