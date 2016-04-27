namespace BudgetApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catcolortoTpe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Type", c => c.String());
            DropColumn("dbo.Categories", "CatColor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "CatColor", c => c.String());
            DropColumn("dbo.Categories", "Type");
        }
    }
}
