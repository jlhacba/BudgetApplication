namespace BudgetApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catsingular : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Expenses", name: "Categories_CategoryID", newName: "Category_CategoryID");
            RenameIndex(table: "dbo.Expenses", name: "IX_Categories_CategoryID", newName: "IX_Category_CategoryID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Expenses", name: "IX_Category_CategoryID", newName: "IX_Categories_CategoryID");
            RenameColumn(table: "dbo.Expenses", name: "Category_CategoryID", newName: "Categories_CategoryID");
        }
    }
}
