using AgeRangerDO.Interface;
using AgeRangerDO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerDO.Implementation
{
    public class AgeGroupRepository : SQLiteRepository<AgeGroup>, IAgeGroupRepository
    {
    }
}
