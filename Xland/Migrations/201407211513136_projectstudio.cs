namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectstudio : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Project", "Studio_ID", "dbo.Studio");
            DropIndex("dbo.Project", new[] { "Studio_ID" });
            CreateTable(
                "dbo.StudioProject",
                c => new
                    {
                        Studio_ID = c.Int(nullable: false),
                        Project_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Studio_ID, t.Project_ID })
                .ForeignKey("dbo.Studio", t => t.Studio_ID, cascadeDelete: true)
                .ForeignKey("dbo.Project", t => t.Project_ID, cascadeDelete: true)
                .Index(t => t.Studio_ID)
                .Index(t => t.Project_ID);
            
            DropColumn("dbo.Project", "Studio_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project", "Studio_ID", c => c.Int());
            DropForeignKey("dbo.StudioProject", "Project_ID", "dbo.Project");
            DropForeignKey("dbo.StudioProject", "Studio_ID", "dbo.Studio");
            DropIndex("dbo.StudioProject", new[] { "Project_ID" });
            DropIndex("dbo.StudioProject", new[] { "Studio_ID" });
            DropTable("dbo.StudioProject");
            CreateIndex("dbo.Project", "Studio_ID");
            AddForeignKey("dbo.Project", "Studio_ID", "dbo.Studio", "ID");
        }
    }
}
