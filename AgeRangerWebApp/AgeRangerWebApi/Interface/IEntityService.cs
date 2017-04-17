using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerWebApi.Interface
{
    public interface IEntityService<TBO>
    {
        IEnumerable<TBO> GetAll();
        TBO GetSingle(long id);
        TBO Insert(TBO bussEntity);
        long Update(TBO bussEntity);
        long Delete(long id);
    }
}
