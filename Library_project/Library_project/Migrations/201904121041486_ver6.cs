namespace Library_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver6 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserBooks");
            AddColumn("dbo.UserBooks", "Duration", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UserBooks", new[] { "ISBN", "UserID", "Date" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserBooks");
            DropColumn("dbo.UserBooks", "Duration");
            AddPrimaryKey("dbo.UserBooks", new[] { "ISBN", "UserID" });
        }
    }
}
