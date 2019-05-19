namespace Library_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserBooks", "IsReturned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserBooks", "IsReturned");
        }
    }
}
