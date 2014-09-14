namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectmodelrequiredfields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Project", "Lat", c => c.String());
            AlterColumn("dbo.Project", "Long", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Project", "Long", c => c.String(nullable: false));
            AlterColumn("dbo.Project", "Lat", c => c.String(nullable: false));
        }
    }
}
