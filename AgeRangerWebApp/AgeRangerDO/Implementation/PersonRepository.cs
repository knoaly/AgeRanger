using AgeRangerDO.Interface;
using AgeRangerDO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerDO.Implementation
{
    public class PersonRepository : SQLiteRepository<Person>, IPersonRepository
    {
        public IEnumerable<Person> SelectByName(string name)
        {
            var map = Context.GetMapping(typeof(Person));
            var nameWildCard = string.Format("%{0}%", name);
            return Context.Query<Person>("select * from Person where (FirstName like ?) or (LastName like ?)",
                nameWildCard, nameWildCard);
        }
    }
}
