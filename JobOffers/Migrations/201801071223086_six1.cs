namespace JobOffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class six1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "JobTitle", c => c.String(nullable: false));
            AlterColumn("dbo.Jobs", "JobContent", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "JobContent", c => c.String());
            AlterColumn("dbo.Jobs", "JobTitle", c => c.String());
        }
    }
}
