namespace BudgetApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkeyID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Budget_BudgetID", "dbo.Budgets");
            DropForeignKey("dbo.Expenses", "Category_CategoryID", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "Budget_BudgetID" });
            DropIndex("dbo.Expenses", new[] { "Category_CategoryID" });
            RenameColumn(table: "dbo.Categories", name: "Budget_BudgetID", newName: "BudgetID");
            RenameColumn(table: "dbo.Expenses", name: "Category_CategoryID", newName: "CategoryID");
            AlterColumn("dbo.Categories", "BudgetID", c => c.Int(nullable: false));
            AlterColumn("dbo.Expenses", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Categories", "BudgetID");
            CreateIndex("dbo.Expenses", "CategoryID");
            AddForeignKey("dbo.Categories", "BudgetID", "dbo.Budgets", "BudgetID", cascadeDelete: true);
            AddForeignKey("dbo.Expenses", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "BudgetID", "dbo.Budgets");
            DropIndex("dbo.Expenses", new[] { "CategoryID" });
            DropIndex("dbo.Categories", new[] { "BudgetID" });
            AlterColumn("dbo.Expenses", "CategoryID", c => c.Int());
            AlterColumn("dbo.Categories", "BudgetID", c => c.Int());
            RenameColumn(table: "dbo.Expenses", name: "CategoryID", newName: "Category_CategoryID");
            RenameColumn(table: "dbo.Categories", name: "BudgetID", newName: "Budget_BudgetID");
            CreateIndex("dbo.Expenses", "Category_CategoryID");
            CreateIndex("dbo.Categories", "Budget_BudgetID");
            AddForeignKey("dbo.Expenses", "Category_CategoryID", "dbo.Categories", "CategoryID");
            AddForeignKey("dbo.Categories", "Budget_BudgetID", "dbo.Budgets", "BudgetID");
        }
    }
}
