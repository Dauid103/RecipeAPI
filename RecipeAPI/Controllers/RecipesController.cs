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
    public class RecipesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public RecipesController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<List<RecipeModel>> Get(string category = null)
        {
            
            var result = _repository.GetAllRecipes(category);

            return _mapper.Map<List<RecipeModel>>(result);
        }
        
        [HttpGet("{id}")]
        public ActionResult<RecipeModel> Get(int id)
        {
            var result = _repository.GetRecipeById(id);

            if (result == null) return NotFound();

            return _mapper.Map<RecipeModel>(result);
        }
        

        [HttpGet("{id}/shoplists")]
        public ActionResult<List<ShopListModel>> GetRecipeShopLists(int id)
        {
            List<ShopList> shopLists = new List<ShopList>();

            var result = _repository.GetAllShopListRecipes(0,id);

            foreach (var shopListItem in result)
            {
                shopLists.Add(_repository.GetShopListById(shopListItem.ShopListId));
            }

            return _mapper.Map<List<ShopListModel>>(shopLists);
        }

        [HttpPost]
        public ActionResult<RecipeModel> Post(RecipeModel model)
        {
            if (model == null) return BadRequest();

            var result = _mapper.Map<Recipe>(model);

           
            _repository.Add(result);

            if (_repository.SaveChanges())
            {
                return Created($"api/recipes/{model.RecipeId}", _mapper.Map<RecipeModel>(result));
            }

            return BadRequest();

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<RecipeModel> Edit(int id, RecipeModel model)
        {
            if (model == null) return BadRequest();

            var result = _repository.GetRecipeById(id);
            if (result == null) return NotFound();

            _mapper.Map(model, result);

            if (_repository.SaveChanges())
            {
                return _mapper.Map<RecipeModel>(result);
            }

            return BadRequest();

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _repository.GetRecipeById(id);
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
