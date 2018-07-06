namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameInStatusToRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Statuses", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Statuses", "Name", c => c.String());
        }
    }
}
