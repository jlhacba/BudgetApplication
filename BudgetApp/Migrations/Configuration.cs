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

            context.Budgets.AddOrUpdate(newBudget1);

            context.SaveChanges();

            context.Configuration.LazyLoadingEnabled = true;
            
        }
    }
}
