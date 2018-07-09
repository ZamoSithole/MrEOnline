namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedVideoEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Video", "GenreID", "dbo.Genres");
            DropIndex("dbo.Video", new[] { "GenreID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Video", "GenreID");
            AddForeignKey("dbo.Video", "GenreID", "dbo.Genres", "Id", cascadeDelete: true);
        }
    }
}
