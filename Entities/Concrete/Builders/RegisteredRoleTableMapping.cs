using Entities.Concrete.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Concrete.Builders
{
    public class RegisteredRoleTableMapping:IEntityTypeConfiguration<RegisteredRole>
    {
        public void Configure(EntityTypeBuilder<RegisteredRole> builder)
        {
            builder.ToTable("RegisteredRoles");
        }
    }
}