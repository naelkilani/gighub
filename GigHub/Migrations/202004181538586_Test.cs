namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Test : DbMigration
    {
        public override void Up()
        {
            Sql("DROP TABLE IF EXISTS dbo.Attendances");
        }

        public override void Down()
        {
        }
    }
}
