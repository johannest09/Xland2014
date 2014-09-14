namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectmodelupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "Email", c => c.String());
            AddColumn("dbo.Project", "Website", c => c.String());
            AddColumn("dbo.Project", "ProjectType", c => c.Int());
            AddColumn("dbo.Project", "ContactPerson", c => c.String());
            AddColumn("dbo.Project", "ProjectBeginDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Project", "ProjectEndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Project", "InProgress", c => c.Boolean(nullable: false));
            AddColumn("dbo.Project", "CapitalCost", c => c.String());
            AddColumn("dbo.Project", "Affiliates", c => c.String());
            AddColumn("dbo.Project", "ProjectOwner", c => c.String());
            AddColumn("dbo.Project", "Contractor", c => c.String());
            AddColumn("dbo.Project", "AreaSize", c => c.String());
            AddColumn("dbo.Project", "ShortDescription", c => c.String());
            AddColumn("dbo.Project", "LongDescription", c => c.String());
            AddColumn("dbo.Project", "Lat", c => c.String(nullable: false));
            AddColumn("dbo.Project", "Long", c => c.String(nullable: false));
            AddColumn("dbo.Project", "ProjectLocation", c => c.String());
            AddColumn("dbo.Project", "Locality", c => c.String());
            AlterColumn("dbo.Project", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Project", "Title", c => c.String());
            DropColumn("dbo.Project", "Locality");
            DropColumn("dbo.Project", "ProjectLocation");
            DropColumn("dbo.Project", "Long");
            DropColumn("dbo.Project", "Lat");
            DropColumn("dbo.Project", "LongDescription");
            DropColumn("dbo.Project", "ShortDescription");
            DropColumn("dbo.Project", "AreaSize");
            DropColumn("dbo.Project", "Contractor");
            DropColumn("dbo.Project", "ProjectOwner");
            DropColumn("dbo.Project", "Affiliates");
            DropColumn("dbo.Project", "CapitalCost");
            DropColumn("dbo.Project", "InProgress");
            DropColumn("dbo.Project", "ProjectEndDate");
            DropColumn("dbo.Project", "ProjectBeginDate");
            DropColumn("dbo.Project", "ContactPerson");
            DropColumn("dbo.Project", "ProjectType");
            DropColumn("dbo.Project", "Website");
            DropColumn("dbo.Project", "Email");
        }
    }
}
