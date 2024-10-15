using BuberBreakfast.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuberBreakfast.Persistence.Configurations
{
    public class BuberBreakfastConfiguration : IEntityTypeConfiguration<Breakfast>
    {
        public void Configure(EntityTypeBuilder<Breakfast> builder)
        {
            builder.HasKey(b => b.Id);

            //Don't generate any breakfast id, let me generate them myself .
            builder.Property(b => b.Id)
                .ValueGeneratedNever();

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(Breakfast.MaxNameLength);

            builder.Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(Breakfast.MaxDescriptionLength);

            builder.Property(b => b.StartDateTime)
                .IsRequired();

            builder.Property(b => b.EndDateTime)
                .IsRequired();

            builder.Property(b => b.LastModifiedDateTime)
                .IsRequired();
            
            builder.Property(b => b.Savory)
                .HasConversion(
                    //Convert to
                    v => string.Join(',', v),
                    //Convert from
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                .IsRequired();

            builder.Property(b => b.Sweet)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                .IsRequired();
        }
    }
}
