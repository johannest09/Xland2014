namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeprojectmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "Description", c => c.String());
            AddColumn("dbo.Project", "DescriptionEnglish", c => c.String());
            DropColumn("dbo.Project", "Author");
            DropColumn("dbo.Project", "Email");
            DropColumn("dbo.Project", "Website");
            DropColumn("dbo.Project", "ShortDescription");
            DropColumn("dbo.Project", "LongDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project", "LongDescription", c => c.String());
            AddColumn("dbo.Project", "ShortDescription", c => c.String());
            AddColumn("dbo.Project", "Website", c => c.String());
            AddColumn("dbo.Project", "Email", c => c.String());
            AddColumn("dbo.Project", "Author", c => c.String());
            DropColumn("dbo.Project", "DescriptionEnglish");
            DropColumn("dbo.Project", "Description");
        }
    }
}
