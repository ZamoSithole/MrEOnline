namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityRefactoring : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetNumber = c.Int(),
                        StreetName = c.String(),
                        SuburbTown = c.String(),
                        AreaCode = c.String(),
                        CreatedById = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateDeleted = c.DateTime(),
                        DeletedById = c.String(nullable: false, maxLength: 128),
                        DateUpdated = c.DateTime(),
                        UpdatedById = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedById)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.DeletedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressID = c.Int(),
                        Email = c.String(nullable: false),
                        ContactNumber = c.String(nullable: false, maxLength: 10),
                        CreatedById = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateDeleted = c.DateTime(),
                        DeletedById = c.String(nullable: false, maxLength: 128),
                        DateUpdated = c.DateTime(),
                        UpdatedById = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedById)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedById)
                .Index(t => t.AddressID)
                .Index(t => t.CreatedById)
                .Index(t => t.DeletedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 8),
                        ContactID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        DateDeleted = c.DateTime(),
                        DeletedById = c.String(maxLength: 128),
                        DateUpdated = c.DateTime(),
                        UpdatedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedById)
                .ForeignKey("dbo.Statuses", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedById)
                .Index(t => t.ContactID)
                .Index(t => t.StatusID)
                .Index(t => t.CreatedById)
                .Index(t => t.DeletedById)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Statuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedById = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        UpdatedById = c.String(nullable: false, maxLength: 128),
                        DeletedById = c.String(nullable: false, maxLength: 128),
                        DateDeleted = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedById)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.DeletedById);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Addresses", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Addresses", "DeletedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Addresses", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Member", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Member", "StatusID", "dbo.Statuses");
            DropForeignKey("dbo.Statuses", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Statuses", "DeletedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Statuses", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Member", "DeletedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Member", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Member", "ContactID", "dbo.Contacts");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contacts", "UpdatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contacts", "DeletedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contacts", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contacts", "AddressID", "dbo.Addresses");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Statuses", new[] { "DeletedById" });
            DropIndex("dbo.Statuses", new[] { "UpdatedById" });
            DropIndex("dbo.Statuses", new[] { "CreatedById" });
            DropIndex("dbo.Member", new[] { "UpdatedById" });
            DropIndex("dbo.Member", new[] { "DeletedById" });
            DropIndex("dbo.Member", new[] { "CreatedById" });
            DropIndex("dbo.Member", new[] { "StatusID" });
            DropIndex("dbo.Member", new[] { "ContactID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Contacts", new[] { "UpdatedById" });
            DropIndex("dbo.Contacts", new[] { "DeletedById" });
            DropIndex("dbo.Contacts", new[] { "CreatedById" });
            DropIndex("dbo.Contacts", new[] { "AddressID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Addresses", new[] { "UpdatedById" });
            DropIndex("dbo.Addresses", new[] { "DeletedById" });
            DropIndex("dbo.Addresses", new[] { "CreatedById" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Statuses");
            DropTable("dbo.Member");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Contacts");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Addresses");
        }
    }
}
