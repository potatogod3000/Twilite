using Microsoft.EntityFrameworkCore;
using Twilite.Models;

namespace Twilite.Data {

    public class ApplicationDbContext : DbContext {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<UserInfoModel> Users { get; set; }
    }
}