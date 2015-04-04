namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videofeature : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Video",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        Title = c.String(),
                        VideoGallery_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.VideoGallery", t => t.VideoGallery_ID)
                .Index(t => t.VideoGallery_ID);
            
            CreateTable(
                "dbo.VideoGallery",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Project_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Project", t => t.Project_ID)
                .Index(t => t.Project_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Video", "VideoGallery_ID", "dbo.VideoGallery");
            DropForeignKey("dbo.VideoGallery", "Project_ID", "dbo.Project");
            DropIndex("dbo.VideoGallery", new[] { "Project_ID" });
            DropIndex("dbo.Video", new[] { "VideoGallery_ID" });
            DropTable("dbo.VideoGallery");
            DropTable("dbo.Video");
        }
    }
}
