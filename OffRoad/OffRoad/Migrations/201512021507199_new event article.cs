namespace OffRoad.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class neweventarticle : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "Author_Id", "dbo.Users");
            DropForeignKey("dbo.Articles", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Events", "Owner_Id", "dbo.Users");
            DropIndex("dbo.Articles", new[] { "Author_Id" });
            DropIndex("dbo.Articles", new[] { "Category_Id" });
            DropIndex("dbo.Events", new[] { "Owner_Id" });
            AddColumn("dbo.Articles", "EditDate", c => c.DateTime());
            AddColumn("dbo.Events", "EditDate", c => c.DateTime());
            AlterColumn("dbo.Articles", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.Articles", "PicturePath", c => c.String(nullable: false));
            AlterColumn("dbo.Articles", "Author_Id", c => c.Int());
            AlterColumn("dbo.Articles", "Category_Id", c => c.Int());
            AlterColumn("dbo.Events", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.Events", "Owner_Id", c => c.Int());
            CreateIndex("dbo.Articles", "Author_Id");
            CreateIndex("dbo.Articles", "Category_Id");
            CreateIndex("dbo.Events", "Owner_Id");
            AddForeignKey("dbo.Articles", "Author_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Articles", "Category_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.Events", "Owner_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.Articles", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Articles", "Author_Id", "dbo.Users");
            DropIndex("dbo.Events", new[] { "Owner_Id" });
            DropIndex("dbo.Articles", new[] { "Category_Id" });
            DropIndex("dbo.Articles", new[] { "Author_Id" });
            AlterColumn("dbo.Events", "Owner_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Articles", "Category_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Articles", "Author_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Articles", "PicturePath", c => c.String());
            AlterColumn("dbo.Articles", "CreateDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "EditDate");
            DropColumn("dbo.Articles", "EditDate");
            CreateIndex("dbo.Events", "Owner_Id");
            CreateIndex("dbo.Articles", "Category_Id");
            CreateIndex("dbo.Articles", "Author_Id");
            AddForeignKey("dbo.Events", "Owner_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Articles", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Articles", "Author_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
