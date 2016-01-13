namespace OffRoad.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArticleComments", "User_Id", "dbo.Users");
            DropIndex("dbo.ArticleComments", new[] { "User_Id" });
            CreateTable(
                "dbo.EventComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Event_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Event_Id)
                .Index(t => t.User_Id);
            
            AlterColumn("dbo.ArticleComments", "User_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ArticleComments", "User_Id");
            AddForeignKey("dbo.ArticleComments", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticleComments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.EventComments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.EventComments", "Event_Id", "dbo.Events");
            DropIndex("dbo.EventComments", new[] { "User_Id" });
            DropIndex("dbo.EventComments", new[] { "Event_Id" });
            DropIndex("dbo.ArticleComments", new[] { "User_Id" });
            AlterColumn("dbo.ArticleComments", "User_Id", c => c.Int());
            DropTable("dbo.EventComments");
            CreateIndex("dbo.ArticleComments", "User_Id");
            AddForeignKey("dbo.ArticleComments", "User_Id", "dbo.Users", "Id");
        }
    }
}
