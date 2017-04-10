using AgeRangerBO.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerBO.ViewModels
{
    public abstract class ViewModelDetailBase<Entity>
    {
        public string EventCommand { get; set; }
        public string EventArgument { get; set; }

        protected readonly IEntityManager<Entity> _entityManager;

        public IEnumerable<Entity> EntityList { get; set; }
        public Entity EntitySearch { get; set; }
        public Entity EntityDetail { get; set; }

        public ViewModelDetailBase() { }

        public ViewModelDetailBase(IEntityManager<Entity> entityManager)
        {
            _entityManager = entityManager;
        }

        public virtual async Task GetEntityList()
        {
            IEnumerable<Entity> entities = await _entityManager.GetAll();
            EntityList = entities;
        }

        public async Task SetupDetailForEdit(long id)
        {
            Entity anEntity = await _entityManager.GetSingle(id);
            EntityDetail = anEntity;
        }

        protected virtual async Task<Entity> CreateNewEntity()
        {
            Entity result = await _entityManager.Insert(EntityDetail);
            return result;
        }

        protected virtual async Task<bool> UpdateEntity()
        {
            string result = await _entityManager.Update(EntityDetail);
            return result.Contains("success");
        }

        public virtual async Task<bool> DeleteEntity(long id)
        {
            string result = await _entityManager.Delete(id);
            return result.Contains("success");
        }
    }
}
