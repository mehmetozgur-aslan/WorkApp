using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web
{
    public static class IdentityInitiazer
    {
        public static async Task SeedData(UserManager<Entities.Concrete.AppUser> userManager, RoleManager<Entities.Concrete.AppRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                await roleManager.CreateAsync(new Entities.Concrete.AppRole { Name = "Admin" });
            }

            var memberRole = await roleManager.FindByNameAsync("Member");
            if (memberRole == null)
            {
                await roleManager.CreateAsync(new Entities.Concrete.AppRole { Name = "Member" });
            }

            var adminUser = await userManager.FindByNameAsync("ozgur");
            if (adminUser == null)
            {
                Entities.Concrete.AppUser appUser = new Entities.Concrete.AppUser
                {
                    Name = "Mehmet Özgür",
                    Surname = "Aslan",
                    UserName = "mehmetozguraslan",
                    Email = "mehmetozguraslan@gmail.com",
                };
                await userManager.CreateAsync(appUser, "123");
                await userManager.AddToRoleAsync(appUser, "Admin");
            }

        }
    }
}
