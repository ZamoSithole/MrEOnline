namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDateRentedToRentedEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "DateRented", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rentals", "DateRented");
        }
    }
}
