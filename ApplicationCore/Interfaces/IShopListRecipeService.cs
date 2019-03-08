using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IShopListRecipeService
    {
        bool VerifyAddShopListRecipe(ShopListRecipe shopListRecipe, int shopListId, int recipeId, out string errorMessage);
        bool VerifyUpdateShopListRecipe(int id, int shopListId, int recipeId, out string errorMessage);
        
    }
}
