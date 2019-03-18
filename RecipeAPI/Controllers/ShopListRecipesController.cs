using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopListRecipesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<ShopListRecipesController> _logger;
        private readonly IMapper _mapper;
        private readonly IShopListRecipeService _shopListRecipeService;
        private string errorMessage;

        public ShopListRecipesController(IRepository repository, ILogger<ShopListRecipesController> logger, IMapper mapper, IShopListRecipeService shopListRecipeService)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _shopListRecipeService = shopListRecipeService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<ShopListRecipeModel>> GetShopListRecipes(int shopListId = 0, int recipeId = 0)
        {
            try
            {
                var result = _repository.GetAllShopListRecipes(shopListId, recipeId);

                return _mapper.Map<List<ShopListRecipeModel>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetShopListRecipes Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
            

        }

        // GET api/<controller>/5
        [HttpGet("{id:int}")]
        public ActionResult<ShopListRecipeModel> GetShopListRecipe(int id)
        {

            try
            {
                var result = _repository.GetShopListRecipeById(id);
                if (result == null) return NotFound($"Unable to find ShopListRecipe with id: {id}");

                return _mapper.Map<ShopListRecipeModel>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetShopListRecipe Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public ActionResult<ShopListRecipeModel> AddShopListRecipe(ShopListRecipeModel model)
        {

            try
            {
                if (model == null) return BadRequest("ShopListRecipeModel is incorrect");

                var shopListRecipe = _mapper.Map<ShopListRecipe>(model);

                if (!_shopListRecipeService.ValidateAddShopListRecipe(shopListRecipe, model.ShopListId, model.RecipeId, out errorMessage))
                {
                    return BadRequest(errorMessage);
                }

                if (_repository.SaveChanges())
                {
                    return Created($"api/shoplistrecipes/{model.ShopListRecipeId}", _mapper.Map<ShopListRecipeModel>(shopListRecipe));
                }

                return BadRequest("Failed to add ShopListRecipe in database");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddShopListRecipe Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<ShopListRecipeModel> UpdateShopListRecipe(int id, ShopListRecipeModel model)
        {

            try
            {
                if (model == null) return BadRequest("ShopListReipeModel is incorrect");

                var result = _repository.GetShopListRecipeById(id);

                if (result == null) return NotFound($"Unable to find ShopListRecipe with id: {id}");

                if (!_shopListRecipeService.ValidateUpdateShopListRecipe(model.ShopListId, model.RecipeId, out errorMessage))
                {
                    return BadRequest(errorMessage);
                }

                _mapper.Map(model, result);

                if (_repository.SaveChanges())
                {
                    return _mapper.Map<ShopListRecipeModel>(result);
                }

                return BadRequest("Failed to update ShopListRecipe in database");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateShopListRecipe Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
          
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeletShopListRecipee(int id)
        {

            try
            {
                var result = _repository.GetShopListRecipeById(id);

                if (result == null) return NotFound($"Unable to find ShopListRecipe with id: {id}");

                _repository.Delete(result);

                if (_repository.SaveChanges())
                {
                    return Ok();
                }

                return BadRequest("Failed to delete ShopListRecipe in database");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteShopListRecipe Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
            
        }
    }
}
