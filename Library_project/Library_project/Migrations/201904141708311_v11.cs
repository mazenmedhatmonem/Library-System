namespace Library_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Workers", "UserType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workers", "UserType", c => c.String(nullable: false));
        }
    }
}
