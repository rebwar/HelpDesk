using HelpDesk.Domain.Contracts.Articles;
using HelpDesk.Domain.Core.Articles;
using HelpDesk.InfraStructures.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDesk.InfraStructures.DataAccess.Articles
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        private readonly HelpDeskContext context;
        public ArticleRepository(HelpDeskContext dbcontext) : base(dbcontext)
        {
            this.context = dbcontext;
        }
        public int GetArticlesCountInCategory(int id)
        {
            return context.Articles.Where(c => c.CategoryId == id).Count();
        }

    }
}
