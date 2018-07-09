namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedGenreEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Video", "GenreId", "dbo.Genres");
            DropPrimaryKey("dbo.Genres");
            AddColumn("dbo.Genres", "GenreId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Genres", "GenreId");
            AddForeignKey("dbo.Video", "GenreId", "dbo.Genres", "GenreId", cascadeDelete: true);
            DropColumn("dbo.Genres", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Genres", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Video", "GenreId", "dbo.Genres");
            DropPrimaryKey("dbo.Genres");
            DropColumn("dbo.Genres", "GenreId");
            AddPrimaryKey("dbo.Genres", "Id");
            AddForeignKey("dbo.Video", "GenreId", "dbo.Genres", "Id", cascadeDelete: true);
        }
    }
}
