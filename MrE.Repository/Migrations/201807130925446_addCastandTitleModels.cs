namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCastandTitleModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cast",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        TitleId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        DateDeleted = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Titles", t => t.TitleId, cascadeDelete: true)
                .Index(t => t.TitleId);
            
            CreateTable(
                "dbo.Titles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            AddColumn("dbo.Video", "AgeRestriction", c => c.String(nullable: false));
            AddColumn("dbo.Video", "CastId", c => c.Int(nullable: false));
            AddColumn("dbo.Video", "MoviePhoto", c => c.Binary());
            AlterColumn("dbo.Video", "RentalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Video", "GenreID");
            CreateIndex("dbo.Video", "CastId");
            AddForeignKey("dbo.Video", "CastId", "dbo.Cast", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Video", "GenreID", "dbo.Genres", "Id", cascadeDelete: true);
            DropColumn("dbo.Video", "Genre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Video", "Genre", c => c.String(nullable: false));
            DropForeignKey("dbo.Video", "GenreID", "dbo.Genres");
            DropForeignKey("dbo.Video", "CastId", "dbo.Cast");
            DropForeignKey("dbo.Cast", "TitleId", "dbo.Titles");
            DropIndex("dbo.Video", new[] { "CastId" });
            DropIndex("dbo.Video", new[] { "GenreID" });
            DropIndex("dbo.Cast", new[] { "TitleId" });
            AlterColumn("dbo.Video", "RentalPrice", c => c.String(nullable: false));
            DropColumn("dbo.Video", "MoviePhoto");
            DropColumn("dbo.Video", "CastId");
            DropColumn("dbo.Video", "AgeRestriction");
            DropColumn("dbo.Video", "GenreID");
            DropTable("dbo.Genres");
            DropTable("dbo.Titles");
            DropTable("dbo.Cast");
        }
    }
}
