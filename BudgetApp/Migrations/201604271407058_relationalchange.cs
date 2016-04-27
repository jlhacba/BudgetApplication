namespace BudgetApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationalchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "Categories_CategoryID", c => c.Int());
            CreateIndex("dbo.Expenses", "Categories_CategoryID");
            AddForeignKey("dbo.Expenses", "Categories_CategoryID", "dbo.Categories", "CategoryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "Categories_CategoryID", "dbo.Categories");
            DropIndex("dbo.Expenses", new[] { "Categories_CategoryID" });
            DropColumn("dbo.Expenses", "Categories_CategoryID");
        }
    }
}
