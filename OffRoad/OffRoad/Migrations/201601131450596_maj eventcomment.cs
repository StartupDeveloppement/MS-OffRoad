namespace OffRoad.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class majeventcomment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventComments", "User_Id", "dbo.Users");
            DropIndex("dbo.EventComments", new[] { "User_Id" });
            AlterColumn("dbo.EventComments", "User_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.EventComments", "User_Id");
            AddForeignKey("dbo.EventComments", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventComments", "User_Id", "dbo.Users");
            DropIndex("dbo.EventComments", new[] { "User_Id" });
            AlterColumn("dbo.EventComments", "User_Id", c => c.Int());
            CreateIndex("dbo.EventComments", "User_Id");
            AddForeignKey("dbo.EventComments", "User_Id", "dbo.Users", "Id");
        }
    }
}
