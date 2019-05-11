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

        public List<Article> GetNewArticles(int ArticleNumbers)
        {
            return context.Articles.OrderByDescending(c => c.Id).Take(ArticleNumbers).ToList();
        }

        public List<Article> GetTopLikeArticles(int ArticleNumbers)
        {
            return context.Articles.OrderByDescending(c => c.Likes).Take(ArticleNumbers).ToList();

        }

        public List<Article> GetTopViewedArticles(int ArticleNumbers)
        {
            return context.Articles.OrderByDescending(c => c.ViewCount).Take(ArticleNumbers).ToList();
        }

        public List<Article> SearchArticle(string search)
        {
            return context.Articles.Where(c => c.Title.Contains(search)).ToList();

        }
    }
}
