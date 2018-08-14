namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDisplayNamesRatingEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Reviews", "Comment", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Comment", c => c.String());
            AlterColumn("dbo.Reviews", "Title", c => c.String());
        }
    }
}
