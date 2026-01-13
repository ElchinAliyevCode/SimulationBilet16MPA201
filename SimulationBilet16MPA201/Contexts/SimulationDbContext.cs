using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimulationBilet16MPA201.Models;

namespace SimulationBilet16MPA201.Contexts;

public class SimulationDbContext : IdentityDbContext<AppUser>
{
    public SimulationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Category> Categories { get; set; }

}
