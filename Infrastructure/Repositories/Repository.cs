using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        
        private readonly RecipeContext _context;
        private readonly ILogger<Repository> _logger;

        public Repository(RecipeContext context, ILogger<Repository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>().AsEnumerable();
        }

        public T GetById<T>(int id) where T : class
        {
            return _context.Set<T>().Find(id);
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");
            return _context.SaveChanges() > 0;
        }


        public List<ShopList> GetAllShopLists()
        {
            
            return _context.ShopLists.ToList();
        }

        public ShopList GetShopListById(int id)
        {
            var result = _context.ShopLists.Where(sl => sl.ShopListId == id);
            
            return result.FirstOrDefault();

        }

        public List<ShopList> GetShopListsByCreatedDate(DateTime dateTime)
        {
            IEnumerable<ShopList> result = _context.ShopLists.Where(sl => sl.CreatedDate == dateTime);
            
            return result.ToList();

        }

        public List<Recipe> GetAllRecipes(string category = null)
        {
            if(category != null)
            {
                return _context.Recipes.Where(r => r.Category == category).ToList();
            }

            return _context.Recipes.ToList();
        }

        public Recipe GetRecipeById(int id)
        {
            var result = _context.Recipes.Where(r => r.RecipeId == id);

            return result.FirstOrDefault();
        }


        public List<ShopListRecipe> GetAllShopListRecipes(int shopListId = 0, int recipeId = 0)
        {
            if(shopListId > 0)
            {
                return _context.ShopListRecipes
                    .Include(slr => slr.Recipe)
                    .Where(slr => slr.ShopListId == shopListId).ToList();
            }
            
            if(recipeId > 0)
            {
                return _context.ShopListRecipes
                    .Include(slr => slr.ShopList)
                    .Where(slr => slr.RecipeId == recipeId).ToList();
            }
            
            return _context.ShopListRecipes.ToList();
        }

        public ShopListRecipe GetShopListRecipeById(int id)
        {
            var result = _context.ShopListRecipes.Where(r => r.ShopListRecipeId == id);

            return result.FirstOrDefault();
        }


    }
}
