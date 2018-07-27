namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserAsFkeyToRental : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rentals", "MemberId", "dbo.Member");
            DropIndex("dbo.Rentals", new[] { "MemberId" });
            AddColumn("dbo.Rentals", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Rentals", "UserId");
            AddForeignKey("dbo.Rentals", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Rentals", "MemberId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rentals", "MemberId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Rentals", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Rentals", new[] { "UserId" });
            DropColumn("dbo.Rentals", "UserId");
            CreateIndex("dbo.Rentals", "MemberId");
            AddForeignKey("dbo.Rentals", "MemberId", "dbo.Member", "Id", cascadeDelete: true);
        }
    }
}
