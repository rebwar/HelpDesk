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
        List<Article> GetNewArticles(int ArticleNumbers);
        List<Article> GetTopLikeArticles(int ArticleNumbers);
        List<Article> GetTopViewedArticles(int ArticleNumbers);
        List<Article> SearchArticle(string search);
        int GetVisitCount();
        int GetVisitCountByAuthor(int UserId);

        List<Article> GetArticlesDesc();
        int GetArticleStaticsByAuthor(int UserId);

        List<Article> GetArticleByAuthor(int userId);

    }
}
