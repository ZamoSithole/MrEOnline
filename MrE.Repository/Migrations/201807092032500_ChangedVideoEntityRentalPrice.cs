namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedVideoEntityRentalPrice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Video", "RentalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Video", "RentalPrice", c => c.String(nullable: false));
        }
    }
}
