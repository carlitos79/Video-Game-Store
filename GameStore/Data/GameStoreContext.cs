using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;

namespace GameStore.Models
{
    public class GameStoreContext : DbContext
    {
        public GameStoreContext (DbContextOptions<GameStoreContext> options)
            : base(options)
        {
        }

        public DbSet<GameStore.Models.Game> Game { get; set; }

        public DbSet<GameStore.Models.Cart> Cart { get; set; }

        public DbSet<GameStore.Models.Order> Order { get; set; }
    }
}
