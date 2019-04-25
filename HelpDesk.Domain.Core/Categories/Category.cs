using HelpDesk.Domain.Core.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.Domain.Core.Categories
{
   public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Article> Articles { get; set; }
    }
}
