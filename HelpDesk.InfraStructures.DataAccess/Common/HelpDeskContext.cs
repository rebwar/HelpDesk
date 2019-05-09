using HelpDesk.Domain.Core.Articles;
using HelpDesk.Domain.Core.Categories;
using HelpDesk.Domain.Core.Comments;
using HelpDesk.Domain.Core.Tags;
using HelpDesk.InfraStructures.DataAccess.Articles;
using HelpDesk.InfraStructures.DataAccess.Categories;
using HelpDesk.InfraStructures.DataAccess.Comments;
using HelpDesk.InfraStructures.DataAccess.Tags;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.InfraStructures.DataAccess.Common
{
    public class HelpDeskContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<HelpDesk.Domain.Core.Images.Image> Images { get; set; }
        public HelpDeskContext(DbContextOptions<HelpDeskContext> options)
            :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CommentConfig());
            modelBuilder.ApplyConfiguration(new ArticleConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new TagConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
