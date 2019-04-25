using HelpDesk.Domain.Core.Articles;
using HelpDesk.Domain.Core.Categories;
using HelpDesk.Domain.Core.Comments;
using HelpDesk.Domain.Core.Tags;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.Infrastructures.DataAccess.Common
{
    public class HelpDeskContext : DbContext
    {
        public DbSet<Category> MyProperty { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public HelpDeskContext(DbContextOptions<HelpDeskContext> option)
            : base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
