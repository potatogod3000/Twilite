using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Twilite.Models;

namespace Twilite.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<PostInfoModel> Posts {get; set; }


    protected override void OnModelCreating(ModelBuilder builder) {

        base.OnModelCreating(builder);

        //Generate SERIAL Column in PostgreSQL Db Table (which auto-increments)
        builder.Entity<PostInfoModel>().Property(p => p.PostId).ValueGeneratedOnAdd();
    }
}
