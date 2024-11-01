namespace HW3_u23551683.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSavedReportModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Surname = c.String(maxLength: 70),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 90),
                        PageCount = c.Int(nullable: false),
                        Point = c.Int(nullable: false),
                        AuthorId = c.Int(),
                        TypeId = c.Int(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Authors", t => t.AuthorId)
                .ForeignKey("dbo.Types", t => t.TypeId)
                .Index(t => t.AuthorId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.Borrows",
                c => new
                    {
                        BorrowId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(),
                        BookId = c.Int(),
                        TakenDate = c.DateTime(),
                        BroughtDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.BorrowId)
                .ForeignKey("dbo.Books", t => t.BookId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Surname = c.String(maxLength: 20),
                        Birthdate = c.DateTime(nullable: false),
                        Gender = c.String(maxLength: 10),
                        Class = c.String(maxLength: 7),
                        Point = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.TypeId);
            
            CreateTable(
                "dbo.SavedReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Filename = c.String(nullable: false, maxLength: 255),
                        FileType = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Borrows", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Borrows", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Borrows", new[] { "BookId" });
            DropIndex("dbo.Borrows", new[] { "StudentId" });
            DropIndex("dbo.Books", new[] { "TypeId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropTable("dbo.SavedReports");
            DropTable("dbo.Types");
            DropTable("dbo.Students");
            DropTable("dbo.Borrows");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
