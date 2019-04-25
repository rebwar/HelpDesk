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
        public ArticleRepository(HelpDeskContext dbcontext) : base(dbcontext)
        {
        }
    }
}
