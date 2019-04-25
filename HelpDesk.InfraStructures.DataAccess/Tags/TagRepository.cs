using HelpDesk.Domain.Contracts.Tags;
using HelpDesk.Domain.Core.Tags;
using HelpDesk.InfraStructures.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDesk.InfraStructures.DataAccess.Tags
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository 
    {
        public TagRepository(HelpDeskContext dbcontext) : base(dbcontext)
        {
        }
    }
}
