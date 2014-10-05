namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photomodelpropertieschanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photo", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photo", "Name");
        }
    }
}
