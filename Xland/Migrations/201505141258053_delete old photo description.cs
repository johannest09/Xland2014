namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteoldphotodescription : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Photo", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photo", "Description", c => c.String());
        }
    }
}
