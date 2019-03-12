using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class RecipeIdentitySeeder
    {
        
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = await userManager.FindByEmailAsync("daviande@kth.se");
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    FirstName = "David",
                    LastName = "Andersson",
                    UserName = "daviande@kth.se",
                    Email = "daviande@kth.se"
                };

                var result = await userManager.CreateAsync(user, "Welc0me!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user in Seeding");
                }
            }

        }
    }
}
