using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeAPI.Models
{
    public class ShopListRecipeModel
    {
        public int ShopListRecipeId { get; set; }

        [Required]
        public DateTime AddedToListDate { get; set; }

        [Required]
        public int ShopListId { get; set; }

        [Required]
        public int RecipeId { get; set; }
        
    }
}
