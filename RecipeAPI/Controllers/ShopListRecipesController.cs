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
        private readonly IShopListRecipeService _shopListRecipeService;
        private string errorMessage;

        public ShopListRecipesController(IRepository repository, IMapper mapper, IShopListRecipeService shopListRecipeService)
        {
            _repository = repository;
            _mapper = mapper;
            _shopListRecipeService = shopListRecipeService;
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

            var shopListRecipe = _mapper.Map<ShopListRecipe>(model);

            if(!_shopListRecipeService.VerifyAddShopListRecipe(shopListRecipe, model.ShopListId, model.RecipeId, out errorMessage))
            {
                return BadRequest(errorMessage);
            }

            if (_repository.SaveChanges())
            {
                return Created($"api/shoplistrecipes/{model.ShopListRecipeId}", _mapper.Map<ShopListRecipeModel>(shopListRecipe));
            }

            return BadRequest("Could not add ShopListRecipe in database");

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<ShopListRecipeModel> Edit(int id, ShopListRecipeModel model)
        {
            if (model == null) return BadRequest();

            var result = _repository.GetShopListRecipeById(id);

            if (result == null) return NotFound();

            if(!_shopListRecipeService.VerifyUpdateShopListRecipe(id, model.ShopListId, model.RecipeId, out errorMessage))
            {
                return BadRequest(errorMessage);
            }

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
