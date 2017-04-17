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

        public override bool Equals(object obj)
        {
            if ((obj == null) || GetType() != obj.GetType())
                return false;

            Person p = (Person)obj;
            return (Id == p.Id) && (FirstName == p.FirstName) && (LastName == p.LastName) && (Age == p.Age);
        }

        public override int GetHashCode()
        {
            return (Id == null ? 0 : Id.GetHashCode()) ^ (FirstName == null ? 0 : FirstName.GetHashCode())
                ^ (LastName == null ? 0 : LastName.GetHashCode()) ^ (Age == null ? 0 : Age.GetHashCode());
        }
    }
}
