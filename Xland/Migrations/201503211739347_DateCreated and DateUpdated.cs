namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateCreatedandDateUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Project", "DateUpdated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "DateUpdated");
            DropColumn("dbo.Project", "DateCreated");
        }
    }
}
