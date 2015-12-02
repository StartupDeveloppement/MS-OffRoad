namespace OffRoad.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatedeModif : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "EditDate", c => c.DateTime());
            AddColumn("dbo.Events", "EditDate", c => c.DateTime());
            AlterColumn("dbo.Events", "CreateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "CreateDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "EditDate");
            DropColumn("dbo.Articles", "EditDate");
        }
    }
}
