namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videodescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Video", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Video", "Description");
        }
    }
}
