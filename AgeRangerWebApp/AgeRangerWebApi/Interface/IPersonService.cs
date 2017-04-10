using AgeRangerBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerWebApi.Interface
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAll();
        Person GetSingle(long id);
        IEnumerable<Person> GetByName(string name);
        Person Insert(Person person);
        long Update(Person person);
        long Delete(long person);
    }
}
