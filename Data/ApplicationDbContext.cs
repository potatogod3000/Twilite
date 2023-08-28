using Microsoft.EntityFrameworkCore;
using Twilite.Models;

namespace Twilite.Data;

public class ApplicationDbContext : DbContext {
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<UserInfoModel> Users { get; set; }

    public DbSet<PostInfoModel> Posts {get; set; }


    protected override void OnModelCreating(ModelBuilder builder) {
        builder.Entity<UserInfoModel>().HasData(
            new UserInfoModel {UserId = 1, Email="admin@twilite.com", UserName="Twilite-Admin", Password="admin@Twilite1234", Country="SimulationLand"}
        );
        builder.Entity<PostInfoModel>().HasData(
            new PostInfoModel {PostId = 1, UserName = "Twilite-Admin", PostContent = "Welcome to twilite! This is a test Post. Enjoy your blogging experience :)"}
        );
    }
}