using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace RecipeAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<RecipeIdentityContext>();

            // Add Recipe DbContext
            services.AddDbContext<RecipeContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("RecipeConnection")));

            // Add Identity DbContext
            services.AddDbContext<RecipeIdentityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddScoped<IRepository, Repository>();

            services.AddTransient<RecipeIdentitySeeder>();

            services.AddAutoMapper();

            services.AddMvc().AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
