using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IShopListRecipeService
    {
        bool ValidateAddShopListRecipe(ShopListRecipe shopListRecipe, int shopListId, int recipeId, out string errorMessage);
        bool ValidateUpdateShopListRecipe(int shopListId, int recipeId, out string errorMessage);
        
    }
}
