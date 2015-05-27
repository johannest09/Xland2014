namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videotypeenum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Video", "VideoType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Video", "VideoType");
        }
    }
}
