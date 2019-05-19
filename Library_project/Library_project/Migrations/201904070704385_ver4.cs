namespace Library_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserBooks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserBooks", "Book_ISBN", "dbo.Books");
            DropIndex("dbo.ApplicationUserBooks", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserBooks", new[] { "Book_ISBN" });
            DropTable("dbo.ApplicationUserBooks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserBooks",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Book_ISBN = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Book_ISBN });
            
            CreateIndex("dbo.ApplicationUserBooks", "Book_ISBN");
            CreateIndex("dbo.ApplicationUserBooks", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserBooks", "Book_ISBN", "dbo.Books", "ISBN", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserBooks", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
