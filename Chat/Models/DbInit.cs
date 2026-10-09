using Microsoft.AspNetCore.Identity;

namespace Chat.Models
{
    public class DbInit
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (await roleManager.FindByNameAsync("Editor") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Editor"));
            }
            if (await roleManager.FindByNameAsync("Client") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Client"));
            }

            string adminEmail = "admin@gmail.com";
            string adminPassword = "qwerty1";
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    PhoneNumber = "380123456789",
                    Year = 1990,
                    City = "Odessa",
                    Address = "Kanatna 47"
                };
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

                User alex = new User
                {
                    Email = "elvira@gmail.com",
                    UserName = "elvira@gmail.com",
                    PhoneNumber = "380987654321",
                    Year = 2008,
                    City = "Odessa",
                    Address = "Sadova 2a"
                };
                result = await userManager.CreateAsync(alex, "qwerty");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(alex, "Editor");
                }

                User tom = new User
                {
                    Email = "sasha@gmail.com",
                    UserName = "sasha@gmail.com",
                    PhoneNumber = "380123654879",
                    Year = 2007,
                    City = "Odessa",
                    Address = "Topolya 3"
                };
                result = await userManager.CreateAsync(tom, "1234567AS");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(tom, "Client");
                }
            }
        }
    }
}