using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RocketEshop.Core.Enums;
using RocketEshop.Core.Models;

namespace RocketEshop.Infrastructure
{
    public class AppDbInitializer
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using IServiceScope serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            AppDbContext? context = serviceScope.ServiceProvider.GetService<AppDbContext>() ?? throw new Exception("Could not get AppDbContext");
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            context.Database.EnsureCreated();

            // For testing purposes
            // SeedGames(context);
            // SeedGenres(context);
            
            await SeedRoles(context, roleManager);
            await SeedUsers(context, userManager);

            context.SaveChanges();
        }

        private static async Task SeedUsers(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            context.Users.RemoveRange(context.Users);
            var newAdminUser = new ApplicationUser()
            {
                FullName = "Admin User",
                UserName = "admin-user",
                Email = "admin@RocketEshop.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(newAdminUser, "Rocket1234!");
            await userManager.AddToRoleAsync(newAdminUser, UserRole.ADMIN.ToString());

            var newAppUser = new ApplicationUser()
            {
                FullName = "App User",
                UserName = "app-user",
                Email = "user@RocketEshop.com",
                EmailConfirmed = true,
                ShoppingCart = new List<ShoppingCartItem>()
            };
            await userManager.CreateAsync(newAppUser, "User1234!");
            await userManager.AddToRoleAsync(newAppUser, UserRole.USER.ToString());
        }

        private static async Task SeedRoles(AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            context.Roles.RemoveRange(context.Roles);
            await roleManager.CreateAsync(new IdentityRole(UserRole.ADMIN.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserRole.USER.ToString()));
        }

        private static void SeedGenres(AppDbContext context)
        {
            context.Genres.RemoveRange(context.Genres);
            context.Genres.AddRange(new List<Genre>()
            {
                new Genre()
                {
                    Name = "Action"
                },
                new Genre()
                {
                    Name = "First Person Shooter"
                },
                new Genre()
                {
                    Name = "Open World"
                },
                new Genre()
                {
                    Name = "Racing"
                }
            });
        }

        private static void SeedGames(AppDbContext context)
        {
            context.Games.RemoveRange(context.Games);
            context.Games.AddRange(new List<Game>()
            {
                new Game()
                {
                    Title = "RISE OF THE TOMB RAIDER 20 YEARS CELEBRATION",
                    Price = 20,
                    ImageUrl = "https://www.e-shop.gr/images/PS4/PS4.00408.jpg",
                    Quantity = 15,
                    Rating = Rating.Excellent,
                    ReleaseDate = new DateTime(2016, 10, 11),
                    Description =
                        "Rise of the Tomb Raider is a 2015 action-adventure video game developed by Crystal Dynamics and published by Microsoft Studios and Square Enix's European subsidiary. The game is the eleventh main entry in the Tomb Raider series, the sequel to the 2013's Tomb Raider, and is the second instalment in the Survivor trilogy."
                },
                new Game()
                {
                    Title = "JUST CAUSE 4",
                    Price = 25,
                    ImageUrl = "https://www.e-shop.gr/images/PS4/PS4.00824.jpg",
                    Quantity = 10,
                    Rating = Rating.Good,
                    ReleaseDate = new DateTime(2018, 12, 4),
                    Description =
                        "Just Cause 4 is a 2018 action-adventure game developed by Avalanche Studios and published by Square Enix. It is the fourth game in the Just Cause series and the sequel to 2015's Just Cause 3 and was released for Microsoft Windows, PlayStation 4, and Xbox One on 4 December 2018."
                },
                new Game()
                {
                    Title = "KILLZONE: SHADOW FALL",
                    Price = 20,
                    ImageUrl = "https://www.e-shop.gr/images/PS4/PS4.00008.jpg",
                    Quantity = 3,
                    Rating = Rating.Average,
                    ReleaseDate = new DateTime(2013, 11, 15),
                    Description =
                        "Killzone Shadow Fall is a 2013 first-person shooter video game developed by Guerrilla Games and published by Sony Computer Entertainment for the PlayStation 4. It is the sixth game of the Killzone series and the fourth game of the series for home consoles."
                },
                new Game()
                {
                    Title = "NIOH",
                    Price = 40,
                    ImageUrl = "https://www.e-shop.gr/images/PS4/PS4.00841.jpg",
                    Quantity = 0,
                    Rating = Rating.Average,
                    ReleaseDate = new DateTime(2017, 2, 7),
                    Description =
                        "Nioh is an action role-playing video game developed by Team Ninja. It was released for PlayStation 4 in February 2017, and was published by Sony Interactive Entertainment internationally, and by Koei Tecmo in Japan."
                },
                new Game()
                {
                    Title = "WASTELAND 3 - DAY ONE EDITION",
                    Price = 55,
                    ImageUrl = "https://www.e-shop.gr/images/PS4/PS4.01647.jpg",
                    Quantity = 0,
                    Rating = Rating.Bad,
                    ReleaseDate = new DateTime(2020, 8, 28),
                    Description =
                        "Wasteland 3 is a role-playing video game developed by inXile Entertainment and published by Deep Silver. It is a sequel to Wasteland 2 (2014) and was released for Microsoft Windows, PlayStation 4 and Xbox One on August 28, 2020. It was ported to Linux and macOS on December 17, 2020."
                }
            });
        }
    }
}
