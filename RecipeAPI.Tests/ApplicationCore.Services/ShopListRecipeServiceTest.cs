using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Moq;
using Xunit;

namespace RecipeAPI.Tests.ApplicationCore.Services
{
    public class ShopListRecipeServiceTest
    {

        private readonly Mock<IRepository> _mockRepository;
        private readonly ShopListRecipeService _shopListRecipeService;

        public ShopListRecipeServiceTest()
        {
            _mockRepository = new Mock<IRepository>();
            _shopListRecipeService = new ShopListRecipeService(_mockRepository.Object);
        }



        [Theory]
        [InlineData(1,1)]
        public void ReturnFalse_ValidateAddShopListRecipe(int shopListId, int recipeId)
        {
            ShopListRecipe shopListRecipe = new ShopListRecipe
            {
                ShopListId = shopListId,
                RecipeId = recipeId
            };

            _mockRepository.Setup(x => x.GetAllShopListRecipes(0,0)).Returns(new List<ShopListRecipe> { new ShopListRecipe {ShopListId = 1, RecipeId = 1 } });
                
            bool result = _shopListRecipeService.ValidateAddShopListRecipe(shopListRecipe, shopListId, recipeId, out string errorMessage);

            _mockRepository.Verify(x => x.Add(shopListRecipe), Times.Never);
            Assert.True(errorMessage != string.Empty);
            Assert.False(result);
            
        }

        [Theory]
        [InlineData(1, 2)]
        public void ReturnTrue_ValidateAddShopListRecipe(int shopListId, int recipeId)
        {
            ShopListRecipe shopListRecipe = new ShopListRecipe
            {
                ShopListId = shopListId,
                RecipeId = recipeId
            };

            _mockRepository.Setup(x => x.GetAllShopListRecipes(0,0)).Returns(new List<ShopListRecipe> { new ShopListRecipe { ShopListId = 1, RecipeId = 1 } });
            
            bool result = _shopListRecipeService.ValidateAddShopListRecipe(shopListRecipe, shopListId, recipeId, out string errorMessage);

            _mockRepository.Verify(x => x.Add(shopListRecipe), Times.Once);
            Assert.True(errorMessage == string.Empty);
            Assert.True(result);
            
        }

        [Theory]
        [InlineData(2, 1)]
        public void ReturnFalse_NoExistingShopList_ValidateUpdateShopListRecipe(int shopListId, int recipeId)
        {
            ShopList shopList = null;

            _mockRepository.Setup(x => x.GetShopListById(shopListId)).Returns(shopList);

            bool result = _shopListRecipeService.ValidateUpdateShopListRecipe(shopListId, recipeId, out string errorMessage);
            
            Assert.True(errorMessage != string.Empty);
            Assert.False(result);

        }

        [Theory]
        [InlineData(1, 2)]
        public void ReturnFalse_NoExistingRecipe_ValidateUpdateShopListRecipe(int shopListId, int recipeId)
        {
            Recipe recipe = null;

            _mockRepository.Setup(x => x.GetShopListById(shopListId)).Returns(new ShopList {ShopListId = 1 });
            _mockRepository.Setup(x => x.GetRecipeById(recipeId)).Returns(recipe);


            bool result = _shopListRecipeService.ValidateUpdateShopListRecipe(shopListId, recipeId, out string errorMessage);

            Assert.True(errorMessage != string.Empty);
            Assert.False(result);

        }

        [Theory]
        [InlineData(1, 1)]
        public void ReturnTrue_ValidateUpdateShopListRecipe(int shopListId, int recipeId)
        {

            _mockRepository.Setup(x => x.GetShopListById(shopListId)).Returns(new ShopList { ShopListId = 1 });
            _mockRepository.Setup(x => x.GetRecipeById(recipeId)).Returns(new Recipe { RecipeId = 1 });


            bool result = _shopListRecipeService.ValidateUpdateShopListRecipe(shopListId, recipeId, out string errorMessage);

            Assert.True(errorMessage == string.Empty);
            Assert.True(result);

        }

    }
}
