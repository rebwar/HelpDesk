using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.Domain.Core.Articles
{
    public class Article:BaseEntity
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public string PublishDate { get; set; }
        public List<ArticleTag> Tags { get; set; }
        public List<ArticleComment> Comments { get; set; }
        public ArticleStatus Status { get; set; }
        public int CategoryId { get; set; }
        public string PDF { get; set; }
        public string AspNetUsersId { get; set; }

    }
}
