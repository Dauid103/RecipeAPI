using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopListRecipesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ShopListRecipesController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<ShopListRecipeModel>> Get(int shopListId = 0, int recipeId = 0)
        {

            var result = _repository.GetAllShopListRecipes(shopListId, recipeId);

            return _mapper.Map<List<ShopListRecipeModel>>(result);

        }

        // GET api/<controller>/5
        [HttpGet("{id:int}")]
        public ActionResult<ShopListRecipeModel> Get(int id)
        {
            var result = _repository.GetShopListRecipeById(id);
            if (result == null) return NotFound();

            return _mapper.Map<ShopListRecipeModel>(result);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<ShopListRecipeModel> Post(ShopListRecipeModel model)
        {
            if (model == null) return BadRequest();

            var result = _repository.GetAllShopListRecipes().Where(slr => slr.ShopListId == model.ShopListId && slr.RecipeId == model.RecipeId);

            if (result.Any()) return BadRequest($"There is already an exisiting ShopListRecipe with ShopListId {model.ShopListId} and RecipeId {model.RecipeId}");


            var shopListRecipe = _mapper.Map<ShopListRecipe>(model);

            _repository.Add(shopListRecipe);

            if (_repository.SaveChanges())
            {
                return Created($"api/shoplistrecipes/{model.ShopListRecipeId}", _mapper.Map<ShopListRecipeModel>(shopListRecipe));
            }

            return BadRequest();

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<ShopListRecipeModel> Edit(int id, ShopListRecipeModel model)
        {
            if (model == null) return BadRequest();

            
            var shopList = _repository.GetShopListById(model.ShopListId);
            if (shopList == null) return BadRequest($"No exisiting ShopList with id {model.ShopListId}");

            var recipe = _repository.GetRecipeById(model.RecipeId);
            if (shopList == null) return BadRequest($"No exisiting Recipe with id {model.RecipeId}");

            var existingShopListRecipe = _repository.GetAllShopListRecipes().Where(slr => slr.ShopListId == model.ShopListId && slr.RecipeId == model.RecipeId);

            if (existingShopListRecipe.Any()) return BadRequest($"There is already an exisiting ShopListRecipe with ShopListId {model.ShopListId} and RecipeId {model.RecipeId}");

            var result = _repository.GetShopListRecipeById(id);

            if (result == null) return NotFound();

            _mapper.Map(model, result);

            if (_repository.SaveChanges())
            {
                return _mapper.Map<ShopListRecipeModel>(result);
            }

            return BadRequest();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _repository.GetShopListRecipeById(id);

            if (result == null) return NotFound();

            _repository.Delete(result);

            if (_repository.SaveChanges())
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
