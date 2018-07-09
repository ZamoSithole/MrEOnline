namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGenreEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genre",
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
            
            AddColumn("dbo.Video", "GenreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Video", "GenreId");
            AddForeignKey("dbo.Video", "GenreId", "dbo.Genre", "Id", cascadeDelete: true);
            DropColumn("dbo.Video", "Genre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Video", "Genre", c => c.String(nullable: false));
            DropForeignKey("dbo.Video", "GenreId", "dbo.Genre");
            DropIndex("dbo.Video", new[] { "GenreId" });
            DropColumn("dbo.Video", "GenreId");
            DropTable("dbo.Genre");
        }
    }
}
