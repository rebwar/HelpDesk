using HelpDesk.Domain.Core.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.Domain.Core.Articles
{
    public class ArticleTag:BaseEntity
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
