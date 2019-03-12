using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeAPI.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<RecipeContext>(options =>
                options.UseSqlServer(config.GetConnectionString("RecipeConnection")));
        }

        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<RecipeIdentityContext>();

            services.AddDbContext<RecipeIdentityContext>(options =>
                options.UseSqlServer(config.GetConnectionString("IdentityConnection")));
        }

        public static void ConfigureCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IShopListRecipeService, ShopListRecipeService>();

        }

    }
}
