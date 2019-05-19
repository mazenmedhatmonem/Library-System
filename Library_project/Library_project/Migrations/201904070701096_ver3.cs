namespace Library_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserBooks",
                c => new
                    {
                        ISBN = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                        isBorrowed = c.Boolean(nullable: false),
                        isRead = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ISBN, t.UserID })
                .ForeignKey("dbo.Books", t => t.ISBN, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ISBN)
                .Index(t => t.UserID);
            
            AddColumn("dbo.Books", "ShelfNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "IsNewArrived", c => c.Boolean(nullable: false));
            DropColumn("dbo.Books", "SelfNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "SelfNumber", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserBooks", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserBooks", "ISBN", "dbo.Books");
            DropIndex("dbo.UserBooks", new[] { "UserID" });
            DropIndex("dbo.UserBooks", new[] { "ISBN" });
            DropColumn("dbo.Books", "IsNewArrived");
            DropColumn("dbo.Books", "ShelfNumber");
            DropTable("dbo.UserBooks");
        }
    }
}
