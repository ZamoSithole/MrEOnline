namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevertVideoEntity : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Video", "GenreID");
            AddForeignKey("dbo.Video", "GenreID", "dbo.Genres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Video", "GenreID", "dbo.Genres");
            DropIndex("dbo.Video", new[] { "GenreID" });
        }
    }
}
