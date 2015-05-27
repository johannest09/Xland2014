namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videoembed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Video", "Embed", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Video", "Embed");
        }
    }
}
