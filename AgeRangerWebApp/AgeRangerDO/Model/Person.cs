using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerDO.Model
{
    public class Person
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? Age { get; set; }
    }
}
