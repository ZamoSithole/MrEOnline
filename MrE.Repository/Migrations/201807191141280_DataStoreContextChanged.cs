namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataStoreContextChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cast", "CastImage", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cast", "CastImage");
        }
    }
}
