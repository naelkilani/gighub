namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Foo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "DateTime");
        }
    }
}
