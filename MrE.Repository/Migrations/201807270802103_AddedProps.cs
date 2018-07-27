namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: false));
            CreateIndex("dbo.Member", "UserId");
            AddForeignKey("dbo.Member", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Member", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Member", "Password", c => c.String(nullable: false, maxLength: 8));
            DropForeignKey("dbo.Member", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Member", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.Member", "UserId");
        }
    }
}
