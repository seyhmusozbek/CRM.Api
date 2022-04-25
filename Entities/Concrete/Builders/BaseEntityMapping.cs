using Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Concrete.Builders
{
    public abstract class BaseEntityMapping<TEntity>:IEntityTypeConfiguration<TEntity>
    where TEntity:BaseEntity,IEntity,new()
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.TimeStamp).IsRowVersion();
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);
            UpConfigure(builder);
            // builder.HasData(SeedData.Seder.GetSeedData<TEntity>());
        }
        
        public abstract void UpConfigure(EntityTypeBuilder<TEntity> builder);
    }
}