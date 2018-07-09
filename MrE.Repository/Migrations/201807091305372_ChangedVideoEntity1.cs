namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedVideoEntity1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Video", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Video", new[] { "Genre_Id" });
            RenameColumn(table: "dbo.Video", name: "Genre_Id", newName: "GenreID");
            AlterColumn("dbo.Video", "GenreID", c => c.Int(nullable: false));
            CreateIndex("dbo.Video", "GenreID");
            AddForeignKey("dbo.Video", "GenreID", "dbo.Genres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Video", "GenreID", "dbo.Genres");
            DropIndex("dbo.Video", new[] { "GenreID" });
            AlterColumn("dbo.Video", "GenreID", c => c.Int());
            RenameColumn(table: "dbo.Video", name: "GenreID", newName: "Genre_Id");
            CreateIndex("dbo.Video", "Genre_Id");
            AddForeignKey("dbo.Video", "Genre_Id", "dbo.Genres", "Id");
        }
    }
}
