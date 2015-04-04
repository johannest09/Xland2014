namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isvisible : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "IsVisible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "IsVisible");
        }
    }
}
