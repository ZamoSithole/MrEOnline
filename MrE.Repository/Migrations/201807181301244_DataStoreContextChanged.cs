namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataStoreContextChanged : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rented",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VideoId = c.Int(nullable: false),
                        MemberID = c.Int(nullable: false),
                        IsCheckedOut = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Member", t => t.MemberID, cascadeDelete: true)
                .ForeignKey("dbo.Video", t => t.VideoId, cascadeDelete: true)
                .Index(t => t.VideoId)
                .Index(t => t.MemberID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rented", "VideoId", "dbo.Video");
            DropForeignKey("dbo.Rented", "MemberID", "dbo.Member");
            DropIndex("dbo.Rented", new[] { "MemberID" });
            DropIndex("dbo.Rented", new[] { "VideoId" });
            DropTable("dbo.Rented");
        }
    }
}
