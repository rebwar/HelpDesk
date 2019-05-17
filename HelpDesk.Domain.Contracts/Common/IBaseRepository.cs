using HelpDesk.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDesk.Domain.Contracts.Common
{
    public interface IBaseRepository<TEntity> where TEntity:BaseEntity,new ()
    {
        TEntity Get(int id);
        IQueryable<TEntity> GetAll();
        void Delete(TEntity entity);
        TEntity Add(TEntity entity); 
        void Update(TEntity entity);
        IQueryable<TEntity> Search(string term);
        int GetStatistic();

    }
}
