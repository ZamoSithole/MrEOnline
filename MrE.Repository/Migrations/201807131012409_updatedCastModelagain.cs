namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedCastModelagain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Video", "CastId", "dbo.Cast");
            DropIndex("dbo.Video", new[] { "CastId" });
            AddColumn("dbo.Cast", "VideoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cast", "VideoId");
            AddForeignKey("dbo.Cast", "VideoId", "dbo.Video", "Id", cascadeDelete: true);
            DropColumn("dbo.Video", "CastId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Video", "CastId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Cast", "VideoId", "dbo.Video");
            DropIndex("dbo.Cast", new[] { "VideoId" });
            DropColumn("dbo.Cast", "VideoId");
            CreateIndex("dbo.Video", "CastId");
            AddForeignKey("dbo.Video", "CastId", "dbo.Cast", "Id", cascadeDelete: true);
        }
    }
}
