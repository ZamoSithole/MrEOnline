namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRentalModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VideoId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                        isCheckedOut = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Member", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.Video", t => t.VideoId, cascadeDelete: true)
                .Index(t => t.VideoId)
                .Index(t => t.MemberId);            
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Rentals", "VideoId", "dbo.Video");
            DropForeignKey("dbo.Rentals", "MemberId", "dbo.Member");
            DropIndex("dbo.Rentals", new[] { "MemberId" });
            DropIndex("dbo.Rentals", new[] { "VideoId" });
            DropTable("dbo.Rentals");            
        }
    }
}
