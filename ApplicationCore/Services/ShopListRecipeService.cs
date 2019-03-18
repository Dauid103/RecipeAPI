using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Services
{
    public class ShopListRecipeService : IShopListRecipeService
    {
        private readonly IRepository _repository;

        public ShopListRecipeService(IRepository repository)
        {
            _repository = repository;
        }

        public bool ValidateAddShopListRecipe(ShopListRecipe shopListRecipe, int shopListId, int recipeId, out string errorMessage)
        {

            var result = _repository.GetAllShopListRecipes().Where(slr => slr.ShopListId == shopListId && slr.RecipeId == recipeId);

            if (result.Any())
            {
                errorMessage = $"There is already an existing ShopListRecipe with shopListId {shopListId} and recipeId {recipeId}";
                return false;
            }
                
            _repository.Add(shopListRecipe);

            errorMessage = string.Empty;
            return true;
        }

        public bool ValidateUpdateShopListRecipe(int shopListId, int recipeId, out string errorMessage)
        {
            var shopList = _repository.GetShopListById(shopListId);
            if (shopList == null)
            {
                errorMessage = $"No existing ShopList with id {shopListId}";
                return false;
            }

            var recipe = _repository.GetRecipeById(recipeId);
            if (recipe == null)
            {
                errorMessage = $"No existing Recipe with id {recipeId}";
                return false;
            }
            
            errorMessage = string.Empty;
            return true;
        }
    }
}
