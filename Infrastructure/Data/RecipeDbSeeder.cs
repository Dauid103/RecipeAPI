using ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RecipeDbSeeder
    {

        public static async Task SeedAsync(RecipeContext context, ILoggerFactory logger)
        {
            try
            {
                if (!context.ShopLists.Any())
                {
                    context.ShopLists.AddRange(GetPreconfiguredShopLists());
                    await context.SaveChangesAsync();
                }

                if (!context.Recipes.Any())
                {
                    context.Recipes.AddRange(GetPreconfiguredRecipes());
                    await context.SaveChangesAsync();
                }

                if (!context.ShopListRecipes.Any())
                {

                    context.ShopListRecipes.AddRange(GetPreconfiguredShopListRecipes());
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var log = logger.CreateLogger<RecipeDbSeeder>();
                log.LogError(ex.Message);
            }
        }

        private static IEnumerable<ShopList> GetPreconfiguredShopLists()
        {
            return new List<ShopList>
            {
                new ShopList {Name = "ShopList1", CreatedDate = new DateTime(2019,03,10)},
                new ShopList {Name = "ShopList2", CreatedDate = new DateTime(2019,03,11)}
            };
        }

        private static IEnumerable<Recipe> GetPreconfiguredRecipes()
        {
            return new List<Recipe>
            {
                new Recipe {Name = "Recipe1", Instruction = "This is Recipe1", Category = "Meat"},
                new Recipe {Name = "Recipe2", Instruction = "This is Recipe2", Category = "Fish"},
                new Recipe {Name = "Recipe3", Instruction = "This is Recipe3", Category = "Vegeterian"}
            };
        }

        private static IEnumerable<ShopListRecipe> GetPreconfiguredShopListRecipes()
        {
            return new List<ShopListRecipe>
            {
                new ShopListRecipe {AddedToListDate = new DateTime(2019,03,10), ShopListId = 1, RecipeId = 1},
                new ShopListRecipe {AddedToListDate = new DateTime(2019,03,10), ShopListId = 1, RecipeId = 2},
                new ShopListRecipe {AddedToListDate = new DateTime(2019,03,10), ShopListId = 1, RecipeId = 3},
                new ShopListRecipe {AddedToListDate = new DateTime(2019,03,11), ShopListId = 2, RecipeId = 1}
            };
        }

    }
}
