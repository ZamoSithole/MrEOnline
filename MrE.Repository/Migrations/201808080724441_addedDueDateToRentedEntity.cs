namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDueDateToRentedEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "DueDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rentals", "DueDate");
        }
    }
}
