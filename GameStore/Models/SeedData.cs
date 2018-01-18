using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace GameStore.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GameStoreContext(
                serviceProvider.GetRequiredService<DbContextOptions<GameStoreContext>>()))
            {
                if (context.Game.Any())
                {
                    return;
                }

                context.Game.AddRange(

                    new Game
                    {
                        GameImage = "/images/farCry4.png",
                        Title = "Far Cry 4",
                        Genre = "FPS",
                        Price = 350,
                        Description = "First - person shooter(FPS) is a video game genre which is centered on gun and projectile weapon - based combat through a first - person perspective.",
                        UnitsInStock = 5
                    },

                     new Game
                     {
                         GameImage = "/images/gearsOfWar4.png",
                         Title = "Gears of War 4",
                         Genre = "TPS",
                         Price = 350,
                         Description = "Third-person shooter (TPS) is a subgenre of 3D shooter games in which the player character is visible on-screen.",
                         UnitsInStock = 5
                     },

                      new Game
                      {
                          GameImage = "/images/pes2015.png",
                          Title = "PES 2015",
                          Genre = "Sports",
                          Price = 350,
                          Description = "A sports game is a video game that simulates the practice of sports.",
                          UnitsInStock = 5
                      },

                       new Game
                       {
                           GameImage = "/images/plantsVsZombies.png",
                           Title = "Plants vs Zombies 2",
                           Genre = "Adventure",
                           Price = 350,
                           Description = "An adventure game is a video game in which the player assumes the role of protagonist in an interactive story driven by exploration and puzzle-solving.",
                           UnitsInStock = 5
                       },

                        new Game
                        {
                            GameImage = "/images/StreetFighterVsTekken.png",
                            Title = "Street Fighter vs Tekken",
                            Genre = "Fighting",
                            Price = 350,
                            Description = "A fighting game is a video game genre in which the player controls an on-screen character and engages in close combat with an opponent.",
                            UnitsInStock = 5
                        },

                        new Game
                        {
                            GameImage = "/images/CallOfDutyIII.jpg",
                            Title = "Call Of Duty III",
                            Genre = "FPS",
                            Price = 350,
                            Description = "First - person shooter(FPS) is a video game genre which is centered on gun and projectile weapon - based combat through a first - person perspective.",
                            UnitsInStock = 5
                        },

                     new Game
                     {
                         GameImage = "/images/ArmyOfTwo.jpeg",
                         Title = "Army Of Two - The Devil's Cartel",
                         Genre = "TPS",
                         Price = 350,
                         Description = "Third-person shooter (TPS) is a subgenre of 3D shooter games in which the player character is visible on-screen.",
                         UnitsInStock = 5
                     },

                      new Game
                      {
                          GameImage = "/images/pes2018.jpg",
                          Title = "PES 2018",
                          Genre = "Sports",
                          Price = 350,
                          Description = "A sports game is a video game that simulates the practice of sports.",
                          UnitsInStock = 5
                      },

                       new Game
                       {
                           GameImage = "/images/Assasins.jpg",
                           Title = "Assassin's Creed Origins",
                           Genre = "Adventure",
                           Price = 350,
                           Description = "An adventure game is a video game in which the player assumes the role of protagonist in an interactive story driven by exploration and puzzle-solving.",
                           UnitsInStock = 5
                       },

                        new Game
                        {
                            GameImage = "/images/StreetFighterVsCapcom.jpg",
                            Title = "Street Fighter vs Capcom",
                            Genre = "Fighting",
                            Price = 350,
                            Description = "A fighting game is a video game genre in which the player controls an on-screen character and engages in close combat with an opponent.",
                            UnitsInStock = 5
                        }

                );
                context.SaveChanges();
            }
        }
    }
}
