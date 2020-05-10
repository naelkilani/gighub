namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddActiveToGigModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gigs", "Active", c => c.Boolean(nullable: false, defaultValue: true));
        }

        public override void Down()
        {
            DropColumn("dbo.Gigs", "Active");
        }
    }
}
