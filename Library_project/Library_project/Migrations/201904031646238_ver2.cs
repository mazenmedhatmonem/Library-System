namespace Library_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workers", "UserType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workers", "UserType");
        }
    }
}
