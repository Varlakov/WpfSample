using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WpfSample.Data.Model;

namespace WpfSample.Data.EntityConfigurations
{
    internal abstract class ConfigurationBase<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(20);
            builder.Property(x => x.LastModifiedOn);
        }
    }
}
