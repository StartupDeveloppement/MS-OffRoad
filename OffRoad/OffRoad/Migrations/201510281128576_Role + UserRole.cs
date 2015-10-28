namespace OffRoad.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleUserRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUser_Id = c.Int(),
                        Roles_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.IdUser_Id)
                .ForeignKey("dbo.Roles", t => t.Roles_Id)
                .Index(t => t.IdUser_Id)
                .Index(t => t.Roles_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Roles_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "IdUser_Id", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "Roles_Id" });
            DropIndex("dbo.UserRoles", new[] { "IdUser_Id" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
        }
    }
}
