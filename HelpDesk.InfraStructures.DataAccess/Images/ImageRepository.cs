using HelpDesk.Domain.Contracts.Images;
using HelpDesk.Domain.Core.Images;
using HelpDesk.InfraStructures.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.InfraStructures.DataAccess.Images
{
    public class ImageRepository : BaseRepository<Image>, IimageRepository
    {
        private readonly HelpDeskContext _dbContext;
        public ImageRepository(HelpDeskContext dbcontext) : base(dbcontext)
        {
            this._dbContext = dbcontext;
        }
    }
}
