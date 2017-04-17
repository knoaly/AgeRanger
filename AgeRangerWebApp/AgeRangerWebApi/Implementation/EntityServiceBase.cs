using AgeRangerDO.Interface;
using AgeRangerWebApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgeRangerWebApi.Implementation
{
    public abstract class EntityServiceBase<TBO, TDO> : IEntityService<TBO> where TBO : new()
                                                                            where TDO : new()
    {
        protected readonly IRepository<TDO> _repo;

        public EntityServiceBase() { }

        public EntityServiceBase(IRepository<TDO> repo)
        {
            _repo = repo;
        }

        public virtual IEnumerable<TBO> GetAll()
        {
            IEnumerable<TDO> entities = _repo.SelectAll() as IEnumerable<TDO>;
            IEnumerable<TBO> retEntities = entities.Select(EntityRepoToBO);
            return retEntities;
        }

        protected abstract TBO EntityRepoToBO(TDO dataEntity);

        protected abstract TDO EntityBOToRepo(TBO bussEntity);

        public virtual TBO GetSingle(long id)
        {
            TDO dataEntity = _repo.Select(id);
            TBO bussEntity = EntityRepoToBO(dataEntity);
            return bussEntity;
        }

        public abstract TBO Insert(TBO dataEntity);

        public virtual long Update(TBO bussEntity)
        {
            long iRes = _repo.Update(EntityBOToRepo(bussEntity));
            return iRes;
        }

        public abstract long Delete(long id);
    }
}