namespace Library_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ISBN = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 20),
                        Category = c.String(nullable: false, maxLength: 20),
                        Author = c.String(nullable: false, maxLength: 20),
                        publisher = c.String(nullable: false, maxLength: 20),
                        PublishingDate = c.DateTime(nullable: false),
                        Edition = c.String(nullable: false),
                        Pages = c.Int(nullable: false),
                        NoOfCopy = c.Int(nullable: false),
                        Available = c.Boolean(nullable: false),
                        SelfNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ISBN);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsValid = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Birthdate = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.MemberID)
                .ForeignKey("dbo.AspNetUsers", t => t.MemberID)
                .Index(t => t.MemberID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        WorkerID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Birthdate = c.DateTime(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        Salary = c.Double(nullable: false),
                        Address = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.WorkerID)
                .ForeignKey("dbo.AspNetUsers", t => t.WorkerID)
                .Index(t => t.WorkerID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ApplicationUserBooks",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Book_ISBN = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Book_ISBN })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_ISBN, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Book_ISBN);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Workers", "WorkerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Members", "MemberID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserBooks", "Book_ISBN", "dbo.Books");
            DropForeignKey("dbo.ApplicationUserBooks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserBooks", new[] { "Book_ISBN" });
            DropIndex("dbo.ApplicationUserBooks", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Workers", new[] { "WorkerID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Members", new[] { "MemberID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.ApplicationUserBooks");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Workers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Members");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Books");
        }
    }
}
