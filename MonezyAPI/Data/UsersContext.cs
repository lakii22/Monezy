using Microsoft.EntityFrameworkCore;

namespace MonezyAPI.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options)
        :base(options) 
        { 
            
        }

        public DbSet<MonezyAPI.Models.Users> Users { get; set; } = default!;
    }
}
