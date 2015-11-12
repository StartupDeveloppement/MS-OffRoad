namespace OffRoad.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcategoryEventEventUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Place = c.String(nullable: false),
                        Owner_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id, cascadeDelete: true)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.EventUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Event_Id = c.Int(),
                        Participant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .ForeignKey("dbo.Users", t => t.Participant_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Participant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventUsers", "Participant_Id", "dbo.Users");
            DropForeignKey("dbo.EventUsers", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Events", "Owner_Id", "dbo.Users");
            DropIndex("dbo.EventUsers", new[] { "Participant_Id" });
            DropIndex("dbo.EventUsers", new[] { "Event_Id" });
            DropIndex("dbo.Events", new[] { "Owner_Id" });
            DropTable("dbo.EventUsers");
            DropTable("dbo.Events");
        }
    }
}
