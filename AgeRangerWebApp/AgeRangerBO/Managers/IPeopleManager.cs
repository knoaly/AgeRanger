using AgeRangerBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerBO.Managers
{
    public interface IPeopleManager : IEntityManager<Person>
    {
        Task<IEnumerable<Person>> GetByName(string name);
    }
}
