using AgeRangerDO.Interface;
using AgeRangerDO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerDO.Implementation
{
    public class RepoExtensions<T> where T : new()
    {
        private readonly IRepository<T> _repo;

        public RepoExtensions(IRepository<T> repo)
        {
            _repo = repo;
        }

        public AgeGroup GetAgeGroupByAge(long age)
        {
            foreach (AgeGroup ag in _repo.SelectAll() as IEnumerable<AgeGroup>)
            {
                if (ag.MinAge == null)
                {
                    if (age < ag.MaxAge) return ag;
                }
                else if (age >= ag.MinAge)
                {
                    if (ag.MaxAge == null) return ag;
                    else if (age < ag.MaxAge) return ag;
                }
            }
            //something not right with the AgeRange table
            throw new Exception("Age could not be found in Age Range.");
        }

    }
}
