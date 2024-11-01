using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using HW3_u23551683.Models;
using Ganss.Xss;

namespace HW3_u23551683.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;

        public HomeController()
        {
            _context = new LibraryContext();
        }

        public async Task<ActionResult> Index(int page = 1, int studentPage = 1)
        {
            const int pageSize = 24;
            var totalBooks = _context.Books.Count();

            var totalStudents = _context.Students.Count();
            var students = await _context.Students
                .OrderBy(s => s.StudentId)
                .Skip((studentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var books = await _context.Books               
                .Include(b => b.Author)
                .Include(b => b.Type)
                .Include(b => b.Borrows)
                .OrderBy(b => b.BookId)
                .Skip((page - 1) * 15)
                .Take(15)
                .ToListAsync();
            var authors = await _context.Authors.ToListAsync();
            var types = await _context.Types.ToListAsync();

            var model = new HomeViewModel
            {
                Students = students,
                Books = books,
                Authors = authors,
                Types = types,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalBooks / (double)pageSize),
                StudentCurrentPage = studentPage,
                StudentTotalPages = (int)Math.Ceiling(totalStudents / (double)pageSize),
            };

            ViewBag.Authors = await _context.Authors
                .Select(a => new { a.AuthorId, FullName = a.Name + " " + a.Surname })
                .ToListAsync();
            ViewBag.Types = await _context.Types.ToListAsync();

            return View(model);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        // Action to handle the creation of a new student
        [HttpPost]
        public async Task<ActionResult> CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // Action to handle the creation of a new book
        [HttpPost]
        public async Task<ActionResult> CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                if (book.AuthorId.HasValue)
                {
                    book.Author = await _context.Authors.FindAsync(book.AuthorId.Value);
                }
                if (book.TypeId.HasValue)
                {
                    book.Type = await _context.Types.FindAsync(book.TypeId.Value);
                }
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var authors = await _context.Authors.ToListAsync();
            var types = await _context.Types.ToListAsync();

            ViewBag.Authors = new SelectList(authors, "AuthorId", "Name", book.AuthorId);
            ViewBag.Types = new SelectList(types, "TypeId", "Name", book.TypeId);

            return View(book);

        }
        public async Task<ActionResult> Maintain(int page = 1, int authorPage = 1, int borrowPage = 1)
        {
            const int pageSize = 20;
            
            var totalAuthors = _context.Authors.Count();
            var author = await _context.Authors
                .OrderBy(s => s.AuthorId)
                .Skip((authorPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalBorrows = _context.Borrows.Count();
            var borrow = await _context.Borrows
                .OrderBy(b => b.BorrowId)
                .Skip((borrowPage - 1) * pageSize)
                .Take(pageSize)
                .Include(b => b.Book)
                .Include(b => b.Student)
                .ToListAsync();

            var types = await _context.Types.ToListAsync();

            var model = new MaintainViewModel
            {
                Types = types,
                CurrentPage = authorPage,
                TotalPages = (int)Math.Ceiling(totalAuthors / (double)pageSize),
                BorrowCurrentPage = borrowPage,
                BorrowTotalPages = (int)Math.Ceiling(totalBorrows / (double)pageSize),
                Authors = author,
                Borrows = borrow
            };
            return View(model);
        }
        // CRUD for Types
        [HttpPost]
        public async Task<ActionResult> CreateType(Models.Type type)
        {
            if (ModelState.IsValid)
            {
                _context.Types.Add(type);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Maintain");
        }

        [HttpPost]
        public async Task<ActionResult> EditType(Models.Type type)
        {
            if (ModelState.IsValid)
            {
                var existingType = await _context.Types.FindAsync(type.TypeId);
                if (existingType != null)
                {
                    existingType.Name = type.Name;
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Maintain");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteType(int id)
        {
            var type = await _context.Types.FindAsync(id);
            if (type != null)
            {
                _context.Types.Remove(type);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Maintain");
        }

        // CRUD for Authors
        [HttpPost]
        public async Task<ActionResult> CreateAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Maintain");
        }

        [HttpPost]
        public async Task<ActionResult> EditAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                var existingAuthor = await _context.Authors.FindAsync(author.AuthorId);
                if (existingAuthor != null)
                {
                    existingAuthor.Name = author.Name;
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Maintain");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Maintain");
        }

        // CRUD for Borrows
        [HttpPost]
        public async Task<ActionResult> CreateBorrow(Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                _context.Borrows.Add(borrow);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Maintain");
        }

        [HttpPost]
        public async Task<ActionResult> EditBorrow(Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                var existingBorrow = await _context.Borrows.FindAsync(borrow.BorrowId);
                if (existingBorrow != null)
                {
                    existingBorrow.BookId = borrow.BookId;
                    existingBorrow.StudentId = borrow.StudentId;
                    existingBorrow.TakenDate = borrow.TakenDate; // Ensure this value is passed
                    existingBorrow.BroughtDate = borrow.BroughtDate; // Ensure this value is passed
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Maintain");
        }


        [HttpPost]
        public async Task<ActionResult> DeleteBorrow(int id)
        {
            var borrow = await _context.Borrows.FindAsync(id);
            if (borrow != null)
            {
                _context.Borrows.Remove(borrow);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Maintain");
        }


        private async Task<List<CategoryReport>> GetCategoryReportAsync()
        {
            var reports = await _context.Books
                .GroupBy(b => b.Type.Name) 
                .Select(g => new CategoryReport
                {
                    CategoryName = g.Key,
                    BookCount = g.Count()
                })
                .ToListAsync(); 

            return reports;
        }

        public async Task<ActionResult> Reports()
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Type)
                .Include(b => b.Borrows)
                .ToListAsync();

            var authors = await _context.Authors.ToListAsync();
            var types = await _context.Types.ToListAsync();

            var categoryReports = await GetCategoryReportAsync(); // Assuming this method exists

            var model = new ReportViewModel
            {
                Books = books,
                Authors = authors,
                Types = types,
                CategoryReports = categoryReports,
                ExportData = new ReportExportViewModel() // Initialize the export data
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ExportReport(ReportExportViewModel model)
        {
            byte[] imageBytes = Convert.FromBase64String(model.ChartImage.Replace("data:image/png;base64,", ""));
            var sanitizer = new HtmlSanitizer();
            var sanitizedDescription = sanitizer.Sanitize(model.Description);
            using (var stream = new MemoryStream())
            {
                if (model.FileType == "pdf")
                {
                    var document = new Document();
                    PdfWriter.GetInstance(document, stream);
                    document.Open();

                    document.Add(new Paragraph("Books Per Type Report"));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph("Description: " + model.Description));
                    document.Add(new Paragraph(" "));

                    iTextSharp.text.Image chartImageInstance = iTextSharp.text.Image.GetInstance(imageBytes);
                    chartImageInstance.ScaleToFit(500f, 300f);
                    chartImageInstance.Alignment = Element.ALIGN_CENTER;

                    document.Add(chartImageInstance);
                    document.Close();

                    model.Filename = Path.ChangeExtension(model.Filename, ".pdf");
                    return File(stream.ToArray(), "application/pdf", model.Filename);
                }
                else if (model.FileType == "png")
                {
                    return File(imageBytes, "image/png", model.Filename + ".png");
                }
            }
            ModelState.AddModelError("", "Invalid file type selected.");
            return View("Reports");
        }

    }
}
