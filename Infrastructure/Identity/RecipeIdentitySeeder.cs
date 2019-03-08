using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class RecipeIdentitySeeder
    {
        private readonly RecipeIdentityContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipeIdentitySeeder(RecipeIdentityContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            ApplicationUser user = await _userManager.FindByEmailAsync("daviande@kth.se");
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    FirstName = "David",
                    LastName = "Andersson",
                    UserName = "daviande@kth.se",
                    Email = "daviande@kth.se"
                };

                var result = await _userManager.CreateAsync(user, "Welc0me!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user in Seeding");
                }
            }

            _context.SaveChanges();
        }
    }
}
