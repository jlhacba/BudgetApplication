namespace BudgetApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class occurprop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Occurance", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Occurance");
        }
    }
}
