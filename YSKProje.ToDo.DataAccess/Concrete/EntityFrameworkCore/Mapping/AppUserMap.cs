using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    //Fluent Api
    public class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(i => i.Name).HasMaxLength(100);
            builder.Property(i => i.Surname).HasMaxLength(100);

            builder.HasMany(i => i.Tasks).WithOne(i => i.AppUser).HasForeignKey(i => i.AppUserId).OnDelete(DeleteBehavior.SetNull);

        }
    }
}
