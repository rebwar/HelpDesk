using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.Domain.Core.Images
{
    public class Image:BaseEntity
    {
        public int ArticleId { get; set; }
        public string Name { get; set; }
    }
}
