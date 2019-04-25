using HelpDesk.Domain.Core.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.InfraStructures.DataAccess.Comments
{
    internal class CommentConfig : IEntityTypeConfiguration<Domain.Core.Comments.Comment>
    {
        public void Configure(EntityTypeBuilder<Domain.Core.Comments.Comment> builder)
        {
            builder.Property(c => c.AddedDate).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Body).HasMaxLength(500).IsRequired();
            builder.Property(c => c.IpSender).HasMaxLength(15).IsRequired();
        }
    }
}
