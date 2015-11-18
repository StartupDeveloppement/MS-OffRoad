namespace OffRoad.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Article_Id = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Article_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Title = c.String(nullable: false),
                        PicturePath = c.String(),
                        Author_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Author_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        NickName = c.String(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        City = c.String(),
                        Gender = c.Int(nullable: false),
                        Avatar_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Avatars", t => t.Avatar_Id)
                .Index(t => t.Avatar_Id);
            
            CreateTable(
                "dbo.Avatars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.EventUsers", "Participant_Id", "dbo.Users");
            DropForeignKey("dbo.EventUsers", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Events", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.ArticleComments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ArticleComments", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.Articles", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Articles", "Author_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Avatar_Id", "dbo.Avatars");
            DropIndex("dbo.UserRoles", new[] { "Roles_Id" });
            DropIndex("dbo.UserRoles", new[] { "IdUser_Id" });
            DropIndex("dbo.EventUsers", new[] { "Participant_Id" });
            DropIndex("dbo.EventUsers", new[] { "Event_Id" });
            DropIndex("dbo.Events", new[] { "Owner_Id" });
            DropIndex("dbo.Users", new[] { "Avatar_Id" });
            DropIndex("dbo.Articles", new[] { "Category_Id" });
            DropIndex("dbo.Articles", new[] { "Author_Id" });
            DropIndex("dbo.ArticleComments", new[] { "User_Id" });
            DropIndex("dbo.ArticleComments", new[] { "Article_Id" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.EventUsers");
            DropTable("dbo.Events");
            DropTable("dbo.Categories");
            DropTable("dbo.Avatars");
            DropTable("dbo.Users");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticleComments");
        }
    }
}
