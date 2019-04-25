using HelpDesk.Domain.Core.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.InfraStructures.DataAccess.Articles
{
    internal class ArticleConfig : IEntityTypeConfiguration<Domain.Core.Articles.Article>
    {
        public void Configure(EntityTypeBuilder<Domain.Core.Articles.Article> builder)
        {
            builder.Property(c => c.Title).HasMaxLength(500).IsRequired();
            builder.Property(c => c.Abstract).HasMaxLength(1000);
            builder.Property(c => c.Body).IsRequired();
            builder.Property(c => c.Image).IsUnicode(false).IsRequired();
            builder.Property(c => c.PublishDate).HasMaxLength(60).IsRequired();
        }
    }
}
