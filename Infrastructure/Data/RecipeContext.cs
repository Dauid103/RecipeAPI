using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class RecipeContext : DbContext
    {

        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<ShopList> ShopLists { get; set; }
        public DbSet<ShopListRecipe> ShopListRecipes {get; set;}
        
       
    }
}
