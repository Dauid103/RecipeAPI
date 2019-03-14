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
    public class RecipesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<RecipesController> _logger;
        private readonly IMapper _mapper;

        public RecipesController(IRepository repository, ILogger<RecipesController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<List<RecipeModel>> GetRecipes(string category = null)
        {
            try
            {
                var result = _repository.GetAllRecipes(category);

                return _mapper.Map<List<RecipeModel>>(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetRecipes Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
            
        }
        
        [HttpGet("{id}")]
        public ActionResult<RecipeModel> GetRecipe(int id)
        {
            try
            {
                var result = _repository.GetRecipeById(id);

                if (result == null) return NotFound($"Unable to find Recipe with id: {id}");

                return _mapper.Map<RecipeModel>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetRecipe Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
            
        }
        

        [HttpGet("{id}/shoplists")]
        public ActionResult<List<ShopListModel>> GetRecipeShopLists(int id)
        {

            try
            {
                List<ShopList> shopLists = new List<ShopList>();

                var result = _repository.GetAllShopListRecipes(0, id);

                foreach (var shopListItem in result)
                {
                    shopLists.Add(_repository.GetShopListById(shopListItem.ShopListId));
                }

                return _mapper.Map<List<ShopListModel>>(shopLists);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetRecipeShopLists Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
            
        }

        [HttpPost]
        [Authorize]
        public ActionResult<RecipeModel> AddRecipe(RecipeModel model)
        {

            try
            {
                if (model == null) return BadRequest("RecipeModel is incorrect");

                var result = _mapper.Map<Recipe>(model);


                _repository.Add(result);

                if (_repository.SaveChanges())
                {
                    return Created($"api/recipes/{model.RecipeId}", _mapper.Map<RecipeModel>(result));
                }

                return BadRequest("Failed to add Recipe in database");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddRecipe Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<RecipeModel> UpdateRecipe(int id, RecipeModel model)
        {
            try
            {
                if (model == null) return BadRequest("RecipeModel is incorrect");

                var result = _repository.GetRecipeById(id);
                if (result == null) return NotFound($"Unable to find Recipe with id: {id}");

                _mapper.Map(model, result);

                if (_repository.SaveChanges())
                {
                    return _mapper.Map<RecipeModel>(result);
                }

                return BadRequest("Failed to update Recipe in database");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateRecipe Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
            

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteRecipe(int id)
        {
            try
            {
                var result = _repository.GetRecipeById(id);
                if (result == null) return NotFound($"Unable to find Recipe with id: {id}");

                _repository.Delete(result);

                if (_repository.SaveChanges())
                {
                    return Ok();
                }

                return BadRequest("Failed to delete ShopListRecipe in database");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteRecipe Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }

        }
    }
}
