using HelpDesk.Domain.Contracts.Categories;
using HelpDesk.Domain.Core.Categories;
using HelpDesk.InfraStructures.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDesk.InfraStructures.DataAccess.Categories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly HelpDeskContext _dbcontext;

        public CategoryRepository(HelpDeskContext dbcontext) : base(dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public List<Category> SearchCategory(string search)
        {
            return _dbcontext.Categories.Where(c => c.Name.Contains(search)).ToList();
        }
    }
}
