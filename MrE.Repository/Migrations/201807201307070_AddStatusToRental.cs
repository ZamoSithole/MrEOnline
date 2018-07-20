namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToRental : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rentals", "StatusId");
            AddForeignKey("dbo.Rentals", "StatusId", "dbo.Statuses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "StatusId", "dbo.Statuses");
            DropIndex("dbo.Rentals", new[] { "StatusId" });
            DropColumn("dbo.Rentals", "StatusId");
        }
    }
}
