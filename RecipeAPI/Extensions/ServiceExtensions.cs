using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static void ConfigureAuthentication_Jwt(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = config["Token:Issuer"],
                        ValidAudience = config["Token:Auddience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"]))
                    };
                });
        }

    }
}
