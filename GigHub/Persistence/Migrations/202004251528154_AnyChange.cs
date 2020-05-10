namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnyChange : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Notifications", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "DateTime", c => c.DateTime(nullable: false));
        }
    }
}
