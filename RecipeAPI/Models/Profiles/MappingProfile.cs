using ApplicationCore.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeAPI.Models.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<ShopList, ShopListModel>()
                .ReverseMap();

            this.CreateMap<Recipe, RecipeModel>()
                .ReverseMap();

            this.CreateMap<ShopListRecipe, ShopListRecipeModel>()
               .ReverseMap();
        }
    }
}
