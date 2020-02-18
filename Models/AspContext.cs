using Microsoft.EntityFrameworkCore;

namespace asp.Models
{
    public class AspContext : DbContext
    {
        public AspContext(DbContextOptions<AspContext> options) :base (options)
        {
            
        }
        public DbSet<User> Users {get; set;}
        public DbSet<Player> Players {get; set;}
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        
    }
}