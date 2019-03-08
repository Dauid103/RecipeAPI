using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public string Category { get; set; }
        
        ICollection<ShopListRecipe> ShopListRecipes { get; set; }
        

    }
}
