using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimulationBilet16MPA201.Models;

namespace SimulationBilet16MPA201.Configurations;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(256);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(512);
        builder.HasOne(x => x.Category).WithMany(x => x.Trainers).HasForeignKey(x => x.CategoryId).HasPrincipalKey(x => x.Id);
    }
}
