namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectedDatesInVideoModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Video", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Video", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Video", "DateUpdated", c => c.DateTime());
            AddColumn("dbo.Video", "DateDeleted", c => c.DateTime());
            AlterColumn("dbo.Video", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Video", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Video", "Genre", c => c.String(nullable: false));
            AlterColumn("dbo.Video", "RentalPrice", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Video", "RentalPrice", c => c.String());
            AlterColumn("dbo.Video", "Genre", c => c.String());
            AlterColumn("dbo.Video", "Description", c => c.String());
            AlterColumn("dbo.Video", "Title", c => c.String());
            DropColumn("dbo.Video", "DateDeleted");
            DropColumn("dbo.Video", "DateUpdated");
            DropColumn("dbo.Video", "DateCreated");
            DropColumn("dbo.Video", "IsDeleted");
        }
    }
}
