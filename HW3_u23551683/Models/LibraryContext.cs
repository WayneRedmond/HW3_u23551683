using HW3_u23551683.Models;
using System;
using System.Data.Entity;
using System.Web.Razor.Parser.SyntaxTree;
using Type = HW3_u23551683.Models.Type;

public class LibraryContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Borrow> Borrows { get; set; }
    public DbSet<Type> Types { get; set; }

    public LibraryContext() : base("name=LibraryContext")
    {
    }
}
