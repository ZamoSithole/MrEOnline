
namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    public class addAddressEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                {
                    AddressId = c.Int(nullable: false, identity: true),
                    StreetNumber = c.Int(nullable: false),
                    StreetName = c.String(),
                    SuburbTown = c.String(),
                    AreaCode = c.String(),
                    DateCreated = c.DateTime(nullable: false),
                    DateUpdated = c.DateTime(nullable: false),
                    DateDeleted = c.DateTime(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    Area_AreaId = c.Int(),
                })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Areas", t => t.Area_AreaId)
                .Index(t => t.Area_AreaId);

            CreateTable(
                "dbo.Contacts",
                c => new
                {
                    ContactId = c.Int(nullable: false, identity: true),
                    AddressId = c.Int(nullable: false),
                    Email = c.String(),
                    ContactNumber = c.String(),
                    DateCreated = c.DateTime(nullable: false),
                    DateUpdated = c.DateTime(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    DateDeleted = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);

            AddColumn("dbo.Users", "Contact_ContactId", c => c.Int());
            CreateIndex("dbo.Users", "Contact_ContactId");
            AddForeignKey("dbo.StakehoUserslders", "Contact_ContactId", "dbo.Contacts", "ContactId");
            DropColumn("dbo.Users", "Contact_ContactDetailId");
            DropColumn("dbo.Users", "Contact_StreetNumber");
        }
    }
}
