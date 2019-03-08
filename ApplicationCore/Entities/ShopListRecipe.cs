using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class ShopListRecipe
    {
        
        public int ShopListRecipeId { get; set; }

        public DateTime AddedToListDate { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int ShopListId { get; set; }
        public ShopList ShopList { get; set; }

    }
}
