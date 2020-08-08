using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class UrgentMap : IEntityTypeConfiguration<Urgent>
    {
        public void Configure(EntityTypeBuilder<Urgent> builder)
        {
            builder.Property(i => i.Definition).HasMaxLength(250);

            //ForeingKey maplemesi bu sefer TaskMap inde gerçekleşti
        }
    }
}
