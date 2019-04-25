using HelpDesk.Domain.Contracts.Comments;
using HelpDesk.Domain.Core.Comments;
using HelpDesk.InfraStructures.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDesk.InfraStructures.DataAccess.Comments
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(HelpDeskContext dbcontext) : base(dbcontext)
        {
        }
    }
}
