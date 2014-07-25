namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photomodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Description = c.String(),
                        IsMainPhoto = c.Boolean(nullable: false),
                        PhotoGallery_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PhotoGallery", t => t.PhotoGallery_ID)
                .Index(t => t.PhotoGallery_ID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photo", "PhotoGallery_ID", "dbo.PhotoGallery");
            DropForeignKey("dbo.PhotoGallery", "Project_ID", "dbo.Project");
            DropIndex("dbo.PhotoGallery", new[] { "Project_ID" });
            DropIndex("dbo.Photo", new[] { "PhotoGallery_ID" });
            DropTable("dbo.PhotoGallery");
            DropTable("dbo.Photo");
        }
    }
}
