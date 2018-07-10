namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VideoEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        DateDeleted = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Video", "GenreID", c => c.Int(nullable: false));
            AlterColumn("dbo.Video", "RentalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Video", "GenreID");
            AddForeignKey("dbo.Video", "GenreID", "dbo.Genres", "Id", cascadeDelete: true);
            DropColumn("dbo.Video", "Genre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Video", "Genre", c => c.String(nullable: false));
            DropForeignKey("dbo.Video", "GenreID", "dbo.Genres");
            DropIndex("dbo.Video", new[] { "GenreID" });
            AlterColumn("dbo.Video", "RentalPrice", c => c.String(nullable: false));
            DropColumn("dbo.Video", "GenreID");
            DropTable("dbo.Genres");
        }
    }
}
