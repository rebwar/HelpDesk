using HelpDesk.Domain.Core.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.InfraStructures.DataAccess.Tags
{
    internal class TagConfig : IEntityTypeConfiguration<Domain.Core.Tags.Tag>
    {
        public void Configure(EntityTypeBuilder<Domain.Core.Tags.Tag> builder)
        {
            builder.Property(c => c.Title).HasMaxLength(50).IsRequired();
            
        }
    }
}
