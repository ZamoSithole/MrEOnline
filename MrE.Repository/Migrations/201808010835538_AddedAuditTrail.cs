namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAuditTrail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditTrail",
                c => new
                    {
                        AuditEntryId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        TransactionId = c.Guid(nullable: false),
                        TableName = c.String(),
                        Operation = c.String(),
                        NewData = c.String(),
                        Column = c.String(),
                        OldData = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AuditEntryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditTrail");
        }
    }
}
