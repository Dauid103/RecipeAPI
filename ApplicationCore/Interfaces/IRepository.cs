using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IRepository
    {
        
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        //ShopLists
        List<ShopList> GetAllShopLists();
        ShopList GetShopListById(int id);
        List<ShopList> GetShopListsByCreatedDate(DateTime dateTime);

        //Recipes
        List<Recipe> GetAllRecipes(string category = null);
        Recipe GetRecipeById(int id);

        //ShopListRecipes
        List<ShopListRecipe> GetAllShopListRecipes(int shopListId = 0, int recipeId = 0);
        ShopListRecipe GetShopListRecipeById(int id);



    }
}
