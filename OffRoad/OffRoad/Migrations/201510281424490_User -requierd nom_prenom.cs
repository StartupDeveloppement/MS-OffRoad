namespace OffRoad.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Userrequierdnom_prenom : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            AlterColumn("dbo.Users", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false));
        }
    }
}
