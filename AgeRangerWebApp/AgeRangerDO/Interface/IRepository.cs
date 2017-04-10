using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerDO.Interface
{
    public interface IRepository<TEntity> where TEntity : new()
    {
        long Insert(TEntity model);
        long Update(TEntity model);
        long Delete(TEntity model);
        TEntity Select(long pk);
        IEnumerable<TEntity> SelectAll();
        long GetLastInsertId();
    }
}
