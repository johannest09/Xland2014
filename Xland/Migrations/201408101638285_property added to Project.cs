namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class propertyaddedtoProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "Designers", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "Designers");
        }
    }
}
