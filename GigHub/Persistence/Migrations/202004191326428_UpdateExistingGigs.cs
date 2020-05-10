namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateExistingGigs : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.Gigs SET Active = 'true'");
        }

        public override void Down()
        {
        }
    }
}
