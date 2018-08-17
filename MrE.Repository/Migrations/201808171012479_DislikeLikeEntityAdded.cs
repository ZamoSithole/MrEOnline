namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DislikeLikeEntityAdded : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.Critics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        videoId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        IsLike = c.Boolean(nullable: false),
                        IsDislike = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Video", t => t.videoId, cascadeDelete: true)
                .Index(t => t.videoId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {

            
            DropForeignKey("dbo.Critics", "videoId", "dbo.Video");
            DropForeignKey("dbo.Critics", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Critics", new[] { "UserId" });
            DropIndex("dbo.Critics", new[] { "videoId" });
            DropTable("dbo.Critics");
           
        }
    }
}
