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
    public class ShopListsController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ShopListsController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<ShopListModel>> Get()
        {
            var result = _repository.GetAllShopLists();

            return _mapper.Map<List<ShopListModel>>(result);
            
        }

        [HttpGet("{id:int}")]
        public ActionResult<ShopListModel> Get(int id)
        {
            var result =_repository.GetShopListById(id);

            if (result == null) return NotFound();

            return _mapper.Map<ShopListModel>(result);
        }

        [HttpGet("{id:int}/recipes")]
        public ActionResult<List<RecipeModel>> GetShopListRecipes(int id)
        {
            List<Recipe> recipes = new List<Recipe>();

            var result = _repository.GetAllShopListRecipes(id);

            foreach(var shopListItem in result)
            {
                recipes.Add(_repository.GetRecipeById(shopListItem.RecipeId));
            }

            return _mapper.Map<List<RecipeModel>>(recipes);

        }

        [HttpGet("{search}")]
        public ActionResult<List<ShopListModel>> SearchByDate(DateTime dateTime)
        {
            var result = _repository.GetShopListsByCreatedDate(dateTime);

            if (result == null) return NotFound();

            return _mapper.Map<List<ShopListModel>>(result);
        }
        
        [HttpPost]
        public ActionResult<ShopListModel> Post(ShopListModel model)
        {
            if(model == null)
            {
                return BadRequest();
            }

            var result = _mapper.Map<ShopList>(model);

            _repository.Add(result);

            if (_repository.SaveChanges())
            {
                return Created($"api/´shoplists/{model.ShopListId}", _mapper.Map<ShopListModel>(result));
            }

            return BadRequest();

        }

        [HttpPut("{id:int}")]
        public ActionResult<ShopListModel> Edit(int id, ShopListModel model)
        {
            if(model == null) return BadRequest();

            var result = _repository.GetShopListById(id);

            if (result == null) return NotFound();

            _mapper.Map(model, result);

            if (_repository.SaveChanges())
            {
                return _mapper.Map<ShopListModel>(result);
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _repository.GetShopListById(id);

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
