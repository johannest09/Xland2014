namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photoaddtitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photo", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photo", "Title");
        }
    }
}
