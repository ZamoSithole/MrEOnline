using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrE.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addAddressEntity1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Contacts", "DateCreated");
            DropColumn("dbo.Contacts", "DateUpdated");
            DropColumn("dbo.Contacts", "IsDeleted");
            DropColumn("dbo.Contacts", "DateDeleted");
        }

        public override void Down()
        {
            AddColumn("dbo.Contacts", "DateDeleted", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contacts", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Contacts", "DateUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contacts", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
