using HelpDesk.Domain.Contracts.Common;
using HelpDesk.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDesk.InfraStructures.DataAccess.Common
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
         where TEntity : BaseEntity, new()
    {
        private readonly HelpDeskContext _dbcontext;
        public BaseRepository(HelpDeskContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public TEntity Add(TEntity entity)
        {
            _dbcontext.Add(entity);
            _dbcontext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            TEntity entity = new TEntity
            {
                Id = id
            };
            _dbcontext.Remove(id);
            _dbcontext.SaveChanges();
        }

        public TEntity Get(int id)
        {
            return _dbcontext.Set<TEntity>().Find(id);
        }
        //public IQueryable<TEntity> Search(TEntity entity)
        //{
        //    return _dbcontext.Set<TEntity>().AsQueryable();
        //}

        public  IQueryable<TEntity> GetAll()
        {
            return _dbcontext.Set<TEntity>().AsQueryable();
        }
        
    }
}
