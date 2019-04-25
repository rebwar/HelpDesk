using HelpDesk.Domain.Core.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.Domain.Core.Articles
{
    public class ArticleComment:BaseEntity
    {
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }

    }
}
