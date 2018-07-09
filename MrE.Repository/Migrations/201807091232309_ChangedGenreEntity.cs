namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedGenreEntity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Video");
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
            
            AddColumn("dbo.Video", "Genre_Id", c => c.Int());
            AlterColumn("dbo.Video", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Video", "Id");
            CreateIndex("dbo.Video", "Genre_Id");
            AddForeignKey("dbo.Video", "Genre_Id", "dbo.Genres", "Id");
            DropColumn("dbo.Video", "Genre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Video", "Genre", c => c.String(nullable: false));
            DropForeignKey("dbo.Video", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Video", new[] { "Genre_Id" });
            DropPrimaryKey("dbo.Video");
            AlterColumn("dbo.Video", "ID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Video", "Genre_Id");
            DropTable("dbo.Genres");
            AddPrimaryKey("dbo.Video", "Id");
        }
    }
}
