namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videodescription1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Video", "DescriptionIS", c => c.String());
            AddColumn("dbo.Video", "DescriptionEN", c => c.String());
            DropColumn("dbo.Video", "Title");
            DropColumn("dbo.Video", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Video", "Description", c => c.String());
            AddColumn("dbo.Video", "Title", c => c.String());
            DropColumn("dbo.Video", "DescriptionEN");
            DropColumn("dbo.Video", "DescriptionIS");
        }
    }
}
