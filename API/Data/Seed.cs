using System.Text.Json;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task ClearConnections(DataContext context)
        {
            context.Connections.RemoveRange(context.Connections);
            await context.SaveChangesAsync();
        }

        public static async Task SeedUsers(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if (users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Moderator"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.Photos.First().IsApproved = true;
                user.UserName = user.UserName.ToLower();

                await userManager.CreateAsync(user, "Password1!");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser
            {
                UserName = "admin",
                KnownAs = "Admin"
            };

            await userManager.CreateAsync(admin, "Password1!");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
        }

        public static async Task SeedCharacters(DataContext context)
        {
            if (await context.Characters.AnyAsync()) return;

            var characterData = await System.IO.File.ReadAllTextAsync("Data/CharacterSeedData.json");
            var characters = JsonSerializer.Deserialize<List<Character>>(characterData);

            if (characters == null) return;

            foreach (var character in characters)
            {
                context.Characters.Add(character);
            }

            await context.SaveChangesAsync();
        }
    }
}