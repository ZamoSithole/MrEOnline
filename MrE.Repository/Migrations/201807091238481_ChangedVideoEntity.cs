namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedVideoEntity : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Video", new[] { "Genre_Id" });
            DropPrimaryKey("dbo.Video");
            AlterColumn("dbo.Video", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Video", "GENRE_ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Video", "Id");
            CreateIndex("dbo.Video", "Genre_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Video", new[] { "Genre_Id" });
            DropPrimaryKey("dbo.Video");
            AlterColumn("dbo.Video", "GENRE_ID", c => c.Int());
            AlterColumn("dbo.Video", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Video", "Id");
            CreateIndex("dbo.Video", "Genre_Id");
        }
    }
}
