namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DowngradedTheSchemaByRemovingChangeTracking : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Addresses", new[] { "CreatedById" });
            DropIndex("dbo.Addresses", new[] { "DeletedById" });
            DropIndex("dbo.Addresses", new[] { "UpdatedById" });
            DropIndex("dbo.Contacts", new[] { "CreatedById" });
            DropIndex("dbo.Contacts", new[] { "DeletedById" });
            DropIndex("dbo.Contacts", new[] { "UpdatedById" });
            DropIndex("dbo.Statuses", new[] { "CreatedById" });
            DropIndex("dbo.Statuses", new[] { "UpdatedById" });
            DropIndex("dbo.Statuses", new[] { "DeletedById" });
            RenameColumn(table: "dbo.Addresses", name: "CreatedById", newName: "User_Id");
            RenameColumn(table: "dbo.Addresses", name: "DeletedById", newName: "User_Id1");
            RenameColumn(table: "dbo.Addresses", name: "UpdatedById", newName: "User_Id2");
            RenameColumn(table: "dbo.Contacts", name: "CreatedById", newName: "User_Id");
            RenameColumn(table: "dbo.Contacts", name: "DeletedById", newName: "User_Id1");
            RenameColumn(table: "dbo.Contacts", name: "UpdatedById", newName: "User_Id2");
            RenameColumn(table: "dbo.Member", name: "CreatedById", newName: "User_Id");
            RenameColumn(table: "dbo.Member", name: "DeletedById", newName: "User_Id1");
            RenameColumn(table: "dbo.Member", name: "UpdatedById", newName: "User_Id2");
            RenameColumn(table: "dbo.Statuses", name: "CreatedById", newName: "User_Id");
            RenameColumn(table: "dbo.Statuses", name: "DeletedById", newName: "User_Id1");
            RenameColumn(table: "dbo.Statuses", name: "UpdatedById", newName: "User_Id2");
            RenameIndex(table: "dbo.Member", name: "IX_CreatedById", newName: "IX_User_Id");
            RenameIndex(table: "dbo.Member", name: "IX_DeletedById", newName: "IX_User_Id1");
            RenameIndex(table: "dbo.Member", name: "IX_UpdatedById", newName: "IX_User_Id2");
            AlterColumn("dbo.Addresses", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Addresses", "User_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.Addresses", "User_Id2", c => c.String(maxLength: 128));
            AlterColumn("dbo.Contacts", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Contacts", "User_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.Contacts", "User_Id2", c => c.String(maxLength: 128));
            AlterColumn("dbo.Statuses", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Statuses", "User_Id2", c => c.String(maxLength: 128));
            AlterColumn("dbo.Statuses", "User_Id1", c => c.String(maxLength: 128));
            CreateIndex("dbo.Addresses", "User_Id");
            CreateIndex("dbo.Addresses", "User_Id1");
            CreateIndex("dbo.Addresses", "User_Id2");
            CreateIndex("dbo.Contacts", "User_Id");
            CreateIndex("dbo.Contacts", "User_Id1");
            CreateIndex("dbo.Contacts", "User_Id2");
            CreateIndex("dbo.Statuses", "User_Id");
            CreateIndex("dbo.Statuses", "User_Id1");
            CreateIndex("dbo.Statuses", "User_Id2");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Statuses", new[] { "User_Id2" });
            DropIndex("dbo.Statuses", new[] { "User_Id1" });
            DropIndex("dbo.Statuses", new[] { "User_Id" });
            DropIndex("dbo.Contacts", new[] { "User_Id2" });
            DropIndex("dbo.Contacts", new[] { "User_Id1" });
            DropIndex("dbo.Contacts", new[] { "User_Id" });
            DropIndex("dbo.Addresses", new[] { "User_Id2" });
            DropIndex("dbo.Addresses", new[] { "User_Id1" });
            DropIndex("dbo.Addresses", new[] { "User_Id" });
            AlterColumn("dbo.Statuses", "User_Id1", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Statuses", "User_Id2", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Statuses", "User_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Contacts", "User_Id2", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Contacts", "User_Id1", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Contacts", "User_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Addresses", "User_Id2", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Addresses", "User_Id1", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Addresses", "User_Id", c => c.String(nullable: false, maxLength: 128));
            RenameIndex(table: "dbo.Member", name: "IX_User_Id2", newName: "IX_UpdatedById");
            RenameIndex(table: "dbo.Member", name: "IX_User_Id1", newName: "IX_DeletedById");
            RenameIndex(table: "dbo.Member", name: "IX_User_Id", newName: "IX_CreatedById");
            RenameColumn(table: "dbo.Statuses", name: "User_Id2", newName: "UpdatedById");
            RenameColumn(table: "dbo.Statuses", name: "User_Id1", newName: "DeletedById");
            RenameColumn(table: "dbo.Statuses", name: "User_Id", newName: "CreatedById");
            RenameColumn(table: "dbo.Member", name: "User_Id2", newName: "UpdatedById");
            RenameColumn(table: "dbo.Member", name: "User_Id1", newName: "DeletedById");
            RenameColumn(table: "dbo.Member", name: "User_Id", newName: "CreatedById");
            RenameColumn(table: "dbo.Contacts", name: "User_Id2", newName: "UpdatedById");
            RenameColumn(table: "dbo.Contacts", name: "User_Id1", newName: "DeletedById");
            RenameColumn(table: "dbo.Contacts", name: "User_Id", newName: "CreatedById");
            RenameColumn(table: "dbo.Addresses", name: "User_Id2", newName: "UpdatedById");
            RenameColumn(table: "dbo.Addresses", name: "User_Id1", newName: "DeletedById");
            RenameColumn(table: "dbo.Addresses", name: "User_Id", newName: "CreatedById");
            CreateIndex("dbo.Statuses", "DeletedById");
            CreateIndex("dbo.Statuses", "UpdatedById");
            CreateIndex("dbo.Statuses", "CreatedById");
            CreateIndex("dbo.Contacts", "UpdatedById");
            CreateIndex("dbo.Contacts", "DeletedById");
            CreateIndex("dbo.Contacts", "CreatedById");
            CreateIndex("dbo.Addresses", "UpdatedById");
            CreateIndex("dbo.Addresses", "DeletedById");
            CreateIndex("dbo.Addresses", "CreatedById");
        }
    }
}
