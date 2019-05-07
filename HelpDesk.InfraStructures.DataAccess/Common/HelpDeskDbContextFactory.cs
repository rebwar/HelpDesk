using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.InfraStructures.DataAccess.Common
{
    public class HelpDeskDbContextFactory : IDesignTimeDbContextFactory<HelpDeskContext>
    {
        public HelpDeskContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<HelpDeskContext>();
            builder.UseSqlServer(@"Server= .;Initial Catalog=HelpDesk;Integrated security=true");
            return new HelpDeskContext(builder.Options);
        }
    }
}
