using BankAppModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAppAuthAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
