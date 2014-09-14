namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initalcommit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoGallery",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Project_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Project", t => t.Project_ID)
                .Index(t => t.Project_ID);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Studio",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Path = c.String(),
                        Description = c.String(),
                        IsMainPhoto = c.Boolean(nullable: false),
                        PhotoGallery_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PhotoGallery", t => t.PhotoGallery_ID)
                .Index(t => t.PhotoGallery_ID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photo", "PhotoGallery_ID", "dbo.PhotoGallery");
            DropForeignKey("dbo.PhotoGallery", "Project_ID", "dbo.Project");
            DropForeignKey("dbo.StudioProject", "Project_ID", "dbo.Project");
            DropForeignKey("dbo.StudioProject", "Studio_ID", "dbo.Studio");
            DropIndex("dbo.StudioProject", new[] { "Project_ID" });
            DropIndex("dbo.StudioProject", new[] { "Studio_ID" });
            DropIndex("dbo.Photo", new[] { "PhotoGallery_ID" });
            DropIndex("dbo.PhotoGallery", new[] { "Project_ID" });
            DropTable("dbo.StudioProject");
            DropTable("dbo.Photo");
            DropTable("dbo.Studio");
            DropTable("dbo.Project");
            DropTable("dbo.PhotoGallery");
        }
    }
}
