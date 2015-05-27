namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photodescriptionenglishicelandic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photo", "DescriptionIS", c => c.String());
            AddColumn("dbo.Photo", "DescriptionEN", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photo", "DescriptionEN");
            DropColumn("dbo.Photo", "DescriptionIS");
        }
    }
}
