using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeAPI.Models
{
    public class ShopListRecipeModel
    {
        public int ShopListRecipeId { get; set; }

        public DateTime AddedToListDate { get; set; }

        public int ShopListId { get; set; }
        

        public int RecipeId { get; set; }
        
    }
}
