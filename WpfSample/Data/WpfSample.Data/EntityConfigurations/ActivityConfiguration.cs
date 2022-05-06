using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WpfSample.Data.Model;

namespace WpfSample.Data.EntityConfigurations
{
    internal class ActivityConfiguration : ConfigurationBase<Activity>
    {
        public override void Configure(EntityTypeBuilder<Activity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired();
        }
    }
}
