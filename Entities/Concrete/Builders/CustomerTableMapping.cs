using Entities.Concrete.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Concrete.Builders
{
    public class CustomerTableMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Customers");
            builder.Property(x => x.FirstName);
            builder.Property(x => x.LastName);
        }
    }
}