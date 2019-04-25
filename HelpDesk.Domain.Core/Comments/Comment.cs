using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.Domain.Core.Comments
{
   public class Comment:BaseEntity
    {
        public string Body { get; set; }
        public string IpSender { get; set; }
        public string Email { get; set; }
        public string AddedDate { get; set; }
        public CommentStatus Status { get; set; }
    }
}
