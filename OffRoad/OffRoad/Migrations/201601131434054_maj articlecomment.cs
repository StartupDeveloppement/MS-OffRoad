namespace OffRoad.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class majarticlecomment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArticleComments", "User_Id", "dbo.Users");
            DropIndex("dbo.ArticleComments", new[] { "User_Id" });
            AlterColumn("dbo.ArticleComments", "User_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ArticleComments", "User_Id");
            AddForeignKey("dbo.ArticleComments", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticleComments", "User_Id", "dbo.Users");
            DropIndex("dbo.ArticleComments", new[] { "User_Id" });
            AlterColumn("dbo.ArticleComments", "User_Id", c => c.Int());
            CreateIndex("dbo.ArticleComments", "User_Id");
            AddForeignKey("dbo.ArticleComments", "User_Id", "dbo.Users", "Id");
        }
    }
}
