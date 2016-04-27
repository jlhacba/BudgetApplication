namespace BudgetApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catcolor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CatColor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "CatColor");
        }
    }
}
