using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerDO.Model
{
    public class AgeGroup
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public long? Id { get; set; }
        public long? MinAge { get; set; }
        public long? MaxAge { get; set; }
        public string Description { get; set; }
    }
}
