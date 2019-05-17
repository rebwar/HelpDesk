using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelpDesk.Domain.Core.Articles
{
   public enum ArticleStatus
    {
        [Display(Name = "منتظر تائید")]
        PrePublish,
        [Display(Name = "منتشر شده")]

        Publish,
        [Display(Name = " پیش نویش")]

        Draft,
        [Display(Name = "بایگانی")]

        Delete
    }
}
