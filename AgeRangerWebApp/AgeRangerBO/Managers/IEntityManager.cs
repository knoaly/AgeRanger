using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerBO.Managers
{
    public interface IEntityManager<Entity>
    {
        Task<IEnumerable<Entity>> GetAll();
        Task<Entity> GetSingle(long id);
        Task<Entity> Insert(Entity entity);
        Task<string> Update(Entity entity);
        Task<string> Delete(long id);
    }
}
