namespace BudgetApp.Migrations
{
    using BudgetApp.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BudgetApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BudgetApp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var newBudget1 = new Budget
            {
                Name = "James' Budget",
                StartDate = new DateTime(2016, 1, 1),
                EndDate = new DateTime (2016, 12, 31),
                

            };

            var newCat1 = new Category
            {
                Type = "Rent",
                BudgetCost = 1055,
                Occurance = "Monthly",
                Budget = newBudget1
            };

            var newCat2 = new Category
            {
                Type = "Utilities",
                BudgetCost = 200,
                Occurance = "Monthly",
                Budget = newBudget1
            };

            var newCat3 = new Category
            {
                Type = "Restaurants",
                BudgetCost = 475,
                Occurance = "Monthly",
                Budget = newBudget1
            };

            var newCat4 = new Category
            {
                Type = "Auto Insurance",
                BudgetCost = 1047,
                Occurance = "Annual",
                Budget = newBudget1
            };

            context.Budgets.AddOrUpdate(newBudget1);
            context.Categories.AddOrUpdate(newCat1, newCat2, newCat3, newCat4);

            context.SaveChanges();

            context.Configuration.LazyLoadingEnabled = true;
            
        }
    }
}
