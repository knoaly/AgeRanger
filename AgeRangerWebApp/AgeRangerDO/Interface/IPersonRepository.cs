using AgeRangerDO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerDO.Interface
{
    public interface IPersonRepository : IRepository<Person>
    {
        IEnumerable<Person> SelectByName(string name);
    }
}
