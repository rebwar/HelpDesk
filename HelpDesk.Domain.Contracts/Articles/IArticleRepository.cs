using HelpDesk.Domain.Contracts.Common;
using HelpDesk.Domain.Core.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.Domain.Contracts.Articles
{
   public interface IArticleRepository: IBaseRepository<Article>
    {
        int GetArticlesCountInCategory(int id);

    }
}
