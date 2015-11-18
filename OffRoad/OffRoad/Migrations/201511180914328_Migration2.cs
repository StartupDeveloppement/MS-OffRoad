namespace OffRoad.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Events", "Text");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Text", c => c.String(nullable: false));
            DropColumn("dbo.Events", "Title");
        }
    }
}
