using AgeRangerBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerWebApi.Interface
{
    public interface IPersonService : IEntityService<Person>
    {
        IEnumerable<Person> GetByName(string name);
    }
}
