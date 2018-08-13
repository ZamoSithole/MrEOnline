namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRatingEntity1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Comment = c.String(),
                        Ratings = c.Int(nullable: false),
                        VideoId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DateDeleted = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Video", t => t.VideoId, cascadeDelete: true)
                .Index(t => t.VideoId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "VideoId", "dbo.Video");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "VideoId" });
            DropTable("dbo.Reviews");
        }
    }
}
