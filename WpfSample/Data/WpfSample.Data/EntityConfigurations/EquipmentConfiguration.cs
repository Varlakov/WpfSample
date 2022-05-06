using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WpfSample.Data.Model;

namespace WpfSample.Data.EntityConfigurations
{
    internal class EquipmentConfiguration : ConfigurationBase<Equipment>
    {
        public override void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(300);

            builder.Property(x => x.Quantity);
            builder.Property(x => x.Type).HasMaxLength(100);
        }
    }
}
