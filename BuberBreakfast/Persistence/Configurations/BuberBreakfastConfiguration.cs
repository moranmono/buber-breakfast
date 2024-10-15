using BuberBreakfast.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
            
            
            var converter = new ValueConverter<List<string>, string>(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            //Setting value comparer to avoid migration warnings.
            var comparer = new ValueComparer<List<string>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());

            builder.Property(b => b.Savory)
                .HasConversion(converter)
                .IsRequired()
                .Metadata.SetValueComparer(comparer);

            builder.Property(b => b.Sweet)
                .HasConversion(converter)
                .IsRequired()
                .Metadata.SetValueComparer(comparer);
        }
    }
}
