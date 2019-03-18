using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    public class ShopListsController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<ShopListsController> _logger;
        private readonly IMapper _mapper;

        public ShopListsController(IRepository repository, ILogger<ShopListsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<ShopListModel>> GetShopLists()
        {
            try
            {
                var result = _repository.GetAllShopLists();

                return _mapper.Map<List<ShopListModel>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetShopLists Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
            
            
        }

        [HttpGet("{id:int}")]
        public ActionResult<ShopListModel> GetShopList(int id)
        {
            try
            {
                var result = _repository.GetShopListById(id);

                if (result == null) return NotFound();

                return _mapper.Map<ShopListModel>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetShopList Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
        }

        [HttpGet("{id:int}/recipes")]
        public ActionResult<List<RecipeModel>> GetShopListRecipes(int id)
        {

            try
            {
                List<Recipe> recipes = new List<Recipe>();

                var result = _repository.GetAllShopListRecipes(id);

                foreach (var shopListItem in result)
                {
                    recipes.Add(_repository.GetRecipeById(shopListItem.RecipeId));
                }

                return _mapper.Map<List<RecipeModel>>(recipes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetShopListRecipes Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }

        }

        [HttpGet("{search}")]
        public ActionResult<List<ShopListModel>> SearcShopListhByDate(DateTime dateTime)
        {

            try
            {
                var result = _repository.GetShopListsByCreatedDate(dateTime);

                if (result == null) return NotFound();

                return _mapper.Map<List<ShopListModel>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside SearchShopListByDate Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
            
        }
        
        [HttpPost]
        [Authorize]
        public ActionResult<ShopListModel> AddShopList(ShopListModel model)
        {

            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                var result = _mapper.Map<ShopList>(model);

                _repository.Add(result);

                if (_repository.SaveChanges())
                {
                    return Created($"api/´shoplists/{model.ShopListId}", _mapper.Map<ShopListModel>(result));
                }

                return BadRequest("Failed to add ShopList in database");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddShopList Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }
           

        }

        [HttpPut("{id:int}")]
        [Authorize]
        public ActionResult<ShopListModel> UpdateShopList(int id, ShopListModel model)
        {

            try
            {
                if (model == null) return BadRequest();

                var result = _repository.GetShopListById(id);

                if (result == null) return NotFound();

                _mapper.Map(model, result);

                if (_repository.SaveChanges())
                {
                    return _mapper.Map<ShopListModel>(result);
                }

                return BadRequest("Failed to update ShopList in database");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateShopList Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }

        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public IActionResult DeleteShopList(int id)
        {
            try
            {
                var result = _repository.GetShopListById(id);

                if (result == null) return NotFound();

                _repository.Delete(result);

                if (_repository.SaveChanges())
                {
                    return Ok();
                }

                return BadRequest("Failed to delete ShopList in database");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteShopList Action: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server error");
            }

        }
        
    }
}
