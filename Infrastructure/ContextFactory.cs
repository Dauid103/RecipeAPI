using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    class ContextFactory : IDesignTimeDbContextFactory<RecipeContext>
    {
        public RecipeContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RecipeContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RecipeApiDb;Integrated Security=True;");

            return new RecipeContext(optionsBuilder.Options);
        }
    }
}
