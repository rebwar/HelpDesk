using HelpDesk.Domain.Contracts.Common;
using HelpDesk.Domain.Core.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.Domain.Contracts.Categories
{
   public interface ICategoryRepository:IBaseRepository<Category>
    {
        List<Category> SearchCategory(string search);
    }
}
