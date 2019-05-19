namespace Library_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArchiveBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ISBN = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        isBorrowed = c.Boolean(nullable: false),
                        isRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => t.ISBN, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.ISBN)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Nodifications",
                c => new
                    {
                        Nodification_ID = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        Role = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Nodification_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArchiveBooks", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ArchiveBooks", "ISBN", "dbo.Books");
            DropIndex("dbo.ArchiveBooks", new[] { "UserID" });
            DropIndex("dbo.ArchiveBooks", new[] { "ISBN" });
            DropTable("dbo.Nodifications");
            DropTable("dbo.ArchiveBooks");
        }
    }
}
