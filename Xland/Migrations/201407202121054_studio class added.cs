namespace Xland.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studioclassadded : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Project", "Studio_ID", c => c.Int());
            CreateIndex("dbo.Project", "Studio_ID");
            AddForeignKey("dbo.Project", "Studio_ID", "dbo.Studio", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project", "Studio_ID", "dbo.Studio");
            DropIndex("dbo.Project", new[] { "Studio_ID" });
            DropColumn("dbo.Project", "Studio_ID");
            DropTable("dbo.Studio");
        }
    }
}
