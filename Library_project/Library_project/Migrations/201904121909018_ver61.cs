namespace Library_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver61 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserBooks");
            AddPrimaryKey("dbo.UserBooks", new[] { "ISBN", "UserID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserBooks");
            AddPrimaryKey("dbo.UserBooks", new[] { "ISBN", "UserID", "Date" });
        }
    }
}
